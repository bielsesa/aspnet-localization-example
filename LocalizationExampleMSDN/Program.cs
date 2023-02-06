using LocalizationExampleMSDN;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Resources;

[assembly: RootNamespace("Localization.Example")]
[assembly: NeutralResourcesLanguage("es")]

namespace Localization.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            // gets locale string from arguments (if present)
            if (args is { Length: 1 })
            {
                CultureInfo.CurrentCulture =
                    CultureInfo.CurrentUICulture =
                        CultureInfo.GetCultureInfo(args[0]);
            }

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();
            var services = app.Services;

            #region Logger
            ILogger logger = 
                services.GetRequiredService<ILoggerFactory>()
                    .CreateLogger("Localization.Example");
            #endregion Logger

            #region MessageService
            MessageService messageService = 
                services.GetRequiredService<MessageService>();

            logger.LogWarning(
                "{Msg}",
                messageService.GetGreetingMessage());
            #endregion MessageService

            #region ParameterizedMessageService
            ParameterizedMessageService parameterizedMessageService =
                services.GetRequiredService<ParameterizedMessageService>();

            logger.LogWarning(
                "{Msg}",
                parameterizedMessageService.GetFormattedMessage(
                    DateTime.Today.AddDays(-3), 37.63));
            #endregion ParameterizedMessageService

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // TODO
            // Make the app work with resources in a different path 
            //services.AddLocalization(opts =>
            //{
            //    opts.ResourcesPath = "Resources";
            //});

            services.AddLocalization();
            services.AddTransient<MessageService>();
            services.AddTransient<ParameterizedMessageService>();
            services.AddLogging(opts => opts.SetMinimumLevel(LogLevel.Warning));
        }
    }
}