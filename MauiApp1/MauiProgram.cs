using System.Reflection;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Analytics;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.Logging;

#if IOS
using Plugin.Firebase.Core.Platforms.iOS;
using UIKit;
#elif ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#endif

namespace MauiApp1
{
    public static class MauiProgram
    {
        static IServiceProvider? _serviceProvider;

        public static TService GetService<TService>()
        where TService : notnull
        => _serviceProvider is not null ? (_serviceProvider.GetRequiredService<TService>()) : throw new Exception("Service provider not registered yet");

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterFirebaseServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddViewModels();

            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((app, launchOptions) => {
                CrossFirebase.Initialize();
                FirebaseCloudMessagingImplementation.Initialize();
                return true;
            }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, _) =>
            {
                CrossFirebase.Initialize(activity);
                //FirebaseAnalyticsImplementation.Initialize(activity);
            }));
#endif
            });

            CrossFirebaseCloudMessaging.Current.NotificationTapped += Current_NotificationTapped;


            return builder;
        }

        private static void Current_NotificationTapped(object? sender, Plugin.Firebase.CloudMessaging.EventArgs.FCMNotificationTappedEventArgs e)
        {
            //var notificationData = e.Notification.Data;
            //if (notificationData.ContainsKey("link"))
            //{
            //    //Do something with the link
            //}
        }
    }
}
