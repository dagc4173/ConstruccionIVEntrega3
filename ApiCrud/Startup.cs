


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiCrud.Services;
using ApiCrud.Models;

namespace ApiCrud
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<LibroService, LibroService>();
            services.AddSingleton<AutorService, AutorService>();

            services.AddMvc().AddMvcOptions(options => options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                _ => "El campo es obligatorio."));

            services.AddSingleton<IList<Libro>>();
            services.AddSingleton<IList<Autor>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
