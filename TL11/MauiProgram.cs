﻿using Microsoft.Extensions.Logging;
using TL11.Database;

namespace TL11
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<DBClass>();
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Set the service provider in the ServiceProviderLocator
            ServiceProviderLocator.ServiceProvider = serviceProvider;

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
