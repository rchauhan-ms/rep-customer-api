using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<OmsarasDbContext>(options=> 
                                    options.UseInMemoryDatabase("OmsarasDb"));
builder.Services.AddProblemDetails();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
                .AddPolicy("RequireAuthorizationFromAustralia", 
                            policy => policy.RequireRole("admin")
                                            .RequireClaim("country", "Australia"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("TokenAuthAus",
        new()
        {
            Name = "Authorization",
            Description = "Token-based authentication and authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header
        });

    options.AddSecurityRequirement(new()
        {
            {
                new()
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "TokenAuthAus"
                    }
                }, 
                new List<string>()
            }
        });
});

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();  

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

//register customer endpoints.
app.RegisterCustomersEndpoints();

app.Run();
