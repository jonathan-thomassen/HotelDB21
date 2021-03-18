using Microsoft.Extensions.Configuration;

namespace RazorPageHotelApp.Services
{
    public abstract class Connection
    {
        protected string ConnectionString;
        public IConfiguration Configuration { get; }

        protected Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
    }
}