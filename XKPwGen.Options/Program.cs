using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace XKPwGen.Options
{
    static class Program
    {
        public static IConfiguration Configuration { get; }

        public static ILoggerFactory LoggerFactory { get; }

        static Program()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true);

            Configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddLogging(x =>
            {
                x.AddConfiguration(Configuration.GetSection("Logging"));
                x.AddDebug();
                x.AddEventSourceLogger();
                x.AddJsonConsole();
                x.AddFile(o => o.RootPath = AppContext.BaseDirectory);
            });

            var sp = services.BuildServiceProvider();

            LoggerFactory = sp.GetRequiredService<ILoggerFactory>();
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
