using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinalProjectRedone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                   Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>()
                          .UseDefaultServiceProvider(           // add this
                               options => options.ValidateScopes = false);// you need to add this if you want to minipulate scopes using the scopefactory for startup you must make validate scopes FALSE ! otherwise it wont work . 
                           //try to look into what options.validate scopes does so that you can understand why its not working . 
                       });
    }
}

