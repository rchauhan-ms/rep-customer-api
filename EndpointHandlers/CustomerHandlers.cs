using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using AutoMapper;

public static class CustomerHandlers
{
    public static async Task<Ok<List<Customer>>> GetCustomersAsync(
                OmsarasDbContext dbContext,
                ClaimsPrincipal claimsPrincipal)
        {
                var customers = await dbContext.Customers.ToListAsync();
                return TypedResults.Ok(customers);
        }

        public static async Task<Results<Ok<Customer>, NotFound>> GetCustomerByIdAsync(int customerId, OmsarasDbContext dbContext)
        {
                var customer = await dbContext.Customers.FindAsync(customerId);
                return customer is not null? 
                                TypedResults.Ok(customer) 
                                : TypedResults.NotFound();
        }

        public static async Task<Created<Customer>> AddCustomerAsync(Customer customer, OmsarasDbContext dbContext)
        {
                dbContext.Customers.Add(customer);
                await dbContext.SaveChangesAsync();
                return TypedResults.Created($"/customers/{customer.CustomerId}", customer);
        } 

        public static async Task<IEnumerable<Response<Customer>>> AddBatchCustomersAsync(
                Customer[] customers, 
                OmsarasDbContext dbContext,
                ICustomerService customerService)
        {
                var customersAdded = await customerService.CreateAsync(customers, dbContext);
                return customersAdded.Select(x=> x);
        }

        public static async Task<Results<NotFound, NoContent>> UpdateCustomerAsync(
                        [FromQuery]int customerId, [FromBody]CustomerUpdateDto customer, 
                        OmsarasDbContext dbContext, IMapper mapper)
        {
                var cust = dbContext.Customers.Find(customerId);
                if(cust == null) return TypedResults.NotFound();

                mapper.Map(customer, cust);

                await dbContext.SaveChangesAsync();
                return TypedResults.NoContent();
        }
        
        public static async Task<Results<NotFound, NoContent>> DeleteCustomerAsync(int customerId, OmsarasDbContext dbContext)
        {
                var customer = dbContext.Customers.Find(customerId);
                        if(customer == null) return TypedResults.NotFound();

                        dbContext.Customers.Remove(customer);

                        await dbContext.SaveChangesAsync();
                        return TypedResults.NoContent();
        }   
}



