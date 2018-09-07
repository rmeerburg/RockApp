using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using rock_app.Models;
using rock_app.Services;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace rock_app
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
            RegisterServices(services);
            SetupBasicAuth(services);

            services.AddOData();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                });

            services.AddDbContext<RockAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseOData(GetEdmModel());

            app.UseAuthentication();
            app.UseMvc(options => {
                // required to support odata queries in controllers
                options.EnableDependencyInjection();
            });
            app.UseStaticFiles();
            await MigrateDatabase(app);
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Artist>("artists");
            builder.EntitySet<Song>("songs");
            return builder.GetEdmModel();
        }

        private void RegisterServices(IServiceCollection services)
        {
            var importType = Configuration["ImportOptions:ImportType"];
            switch (importType)
            {
                case "ExternalServer":
                    services.AddTransient<IDataImportService, ExternalDataImportService>();
                    services.AddTransient<DataImportService>();
                    break;

                case "UserSupplied":
                    services.AddTransient<IDataImportService, DataImportService>();
                    break;

                default:
                    throw new NotSupportedException($"configuration value '{importType}' for ImportOptions:ImportType is not supported");
            }
            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<ISongService, SongService>();
            services.AddTransient<ImportFilterService>();
        }

        private void SetupBasicAuth(IServiceCollection services)
        {
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(options =>
                {
                    options.Realm = "rock_app";
                    options.Events = new BasicAuthenticationEvents
                    {
                        OnValidatePrincipal = context =>
                        {
                            if (context.UserName == "rockstars")
                                context.Principal = new GenericPrincipal(new GenericIdentity(context.UserName), new string[] { });
                            else
                                context.AuthenticationFailMessage = "Authentication failed.";

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private async Task MigrateDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<RockAppContext>();
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
