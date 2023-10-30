using AndreasReitberger.Shared.Hosting;
using AndreasReitberger.Shared.Syncfusion.Hosting;
using Microsoft.Extensions.Logging;
using ShellNavTests.Hosting;

namespace ShellNavTests
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureApp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .InitializeSharedMauiStyles()
                .InitializeSharedSyncfusionStyles()
                ;

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}