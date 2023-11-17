using AutoMapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerUpdateDto, Customer>();
    }
    
}