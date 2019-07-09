using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace BHHCSampleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<Models.ReasonContext>(opt =>
              //  opt.UseInMemoryDatabase("ReasonList"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=BHHCSampleApi;Trusted_Connection=True;ConnectRetryCount=0";
            var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(
                Configuration.GetConnectionString("BHHCSampleApi"));
            //IConfigurationSection BHHCSampleApiCredentials =
              //  Configuration.GetSection("BHHCSampleApiCredentials");

            //builder.UserID = BHHCSampleApiCredentials["UserId"];
            //builder.Password = BHHCSampleApiCredentials["Password"];
            services.AddDbContext<Models.ReasonContext>(options =>
                    options.UseSqlServer(builder.ConnectionString, providerOptions => providerOptions.EnableRetryOnFailure()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
