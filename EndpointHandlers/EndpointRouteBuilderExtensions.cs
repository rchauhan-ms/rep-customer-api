public static class EndpointRouteBuilderExtensions
{
    public static void RegisterCustomersEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var customerEndpoints= endpointRouteBuilder.MapGroup("/customers").RequireAuthorization();
        var customerWithIdEndpoints = customerEndpoints.MapGroup("/{customerId:int}");
        var customersWithBatchEndpoints = customerEndpoints.MapGroup("/batch");

        customerEndpoints.MapGet("", CustomerHandlers.GetCustomersAsync)
                         .RequireAuthorization("RequireAuthorizationFromAustralia");
                         
        customerWithIdEndpoints.MapGet("", CustomerHandlers.GetCustomerByIdAsync)
                                .WithName("GetCustomerById")
                                .WithOpenApi()
                                .WithSummary("Get customer by ID")
                                .WithDescription(@"Providing customer ID will get the customer 
                                from the database if it exists and handed over to the client.")
                                .Produces(400);

        customerEndpoints.MapPost("", CustomerHandlers.AddCustomerAsync);
        customersWithBatchEndpoints.MapPost("", CustomerHandlers.AddBatchCustomersAsync);

        customerWithIdEndpoints.MapPut("", CustomerHandlers.UpdateCustomerAsync)
                               .WithOpenApi(operation => 
                               { 
                                    operation.Deprecated = true; 
                                    return operation;
                                });

        customerWithIdEndpoints.MapDelete("", CustomerHandlers.DeleteCustomerAsync);
    }
}