using FrisbeeScoreApp.Services;
using FrisbeeScoreApp.Views;   // CoursesPage löytyy täältä
using Microsoft.Extensions.Logging;

namespace FrisbeeScoreApp
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
                    // Sovelluksen fontit
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Palvelut
            builder.Services.AddSingleton<DatabaseService>();

            // Sivut
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CoursesPage>();
            builder.Services.AddSingleton<CourseEditPage>();
            builder.Services.AddSingleton<HoleEditorPage>();
            builder.Services.AddSingleton<NewRoundPage>();
            builder.Services.AddSingleton<ScorecardPage>();
            builder.Services.AddSingleton<ResultsPage>();
            builder.Services.AddSingleton<HistoryPage>();

#if DEBUG
            // Debug-lokit kehitysvaiheeseen
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}