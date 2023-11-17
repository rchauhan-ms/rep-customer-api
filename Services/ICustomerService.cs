public interface ICustomerService
{
    Task<IEnumerable<Response<Customer>>> CreateAsync(
                                    IEnumerable<Customer> customers, 
                                    OmsarasDbContext dbContext);
}