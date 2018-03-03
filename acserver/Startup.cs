using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace acserver
{
    public class Startup
    {
        private SerialPortDriver _portDriver;
        private AcManager _acManager;
        private Ac _crAc;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            string comPort = Configuration["comPort"];
            string acModel = Configuration["acModel"];
            _acManager = new AcManager(null);
            services.AddTransient(_acManager);
            if(comPort!=null)
            {
                _portDriver = new SerialPortDriver(comPort);
                _portDriver.Connect();
                _acManager.SetDriver(_portDriver);
                _crAc = _acManager.CreateAc(acModel);
                services.AddTransient(_crAc);
                services.AddTransient(_portDriver);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
