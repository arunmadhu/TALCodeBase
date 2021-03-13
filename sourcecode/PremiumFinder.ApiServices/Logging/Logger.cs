using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumFinder.ApiServices.Logging
{
    public static class Logger
    {
        public static ILogger ConfigureLogger(IConfiguration Configuration, IApplicationBuilder app = null)
        {
            var columnOptions = new ColumnOptions();
            columnOptions.Store.Add(StandardColumn.LogEvent);
            // In case of .net Core application
            if (app != null)
            {
                Log.Logger = new LoggerConfiguration()
                             .ReadFrom.Configuration(Configuration)
                             .Enrich.FromLogContext()
                             .WriteTo.MSSqlServer(Configuration["Serilog:ConnectionString"], Configuration["Serilog:TableName"], columnOptions: columnOptions)
                             .CreateLogger();
            }

            //In case of console app/windows service
            else
            {
                Log.Logger = new LoggerConfiguration()
                           .ReadFrom.Configuration(Configuration)
                           .Enrich.FromLogContext()
                           .WriteTo.MSSqlServer(Configuration["Serilog:ConnectionString"], Configuration["Serilog:TableName"], columnOptions: columnOptions)
                           .CreateLogger();

            }

            return Log.Logger;
        }
        /// <summary>
        /// Extention methods to add AddSerilogger to the Webhost Builder...
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="logger"></param>
        /// <param name="dispose"></param>
        /// <returns></returns>
        public static IWebHostBuilder AddSerilogger(this IWebHostBuilder builder, ILogger logger = null, bool dispose = false)
        {
            return Serilog.SerilogWebHostBuilderExtensions.UseSerilog(builder, logger, dispose);
        }
    }
}
