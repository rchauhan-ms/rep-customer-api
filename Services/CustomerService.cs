public class CustomerService : ICustomerService
{
    public async Task<IEnumerable<Response<Customer>>> CreateAsync(IEnumerable<Customer> customers, 
                                                                    OmsarasDbContext dbContext)
    {
        List<Response<Customer>> responses = new();
        foreach(var customer in customers)
        {
            try
            {
                dbContext.Customers.Add(customer);
                await dbContext.SaveChangesAsync();
                responses.Add(new Response<Customer>(customer, true, "201", "Data has been created successfully."));
            }
            catch(Exception ex)
            {
                responses.Add(new Response<Customer>(customer, false, "500", ex.Message));
            }
        }
        return responses;
    }
}