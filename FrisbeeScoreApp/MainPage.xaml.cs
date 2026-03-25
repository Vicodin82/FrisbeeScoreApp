using FrisbeeScoreApp.Services;
using FrisbeeScoreApp.Views;

namespace FrisbeeScoreApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;

        public MainPage(DatabaseService databaseService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        // Navigointi ratoihin
        private async void OnCoursesClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetRequiredService<CoursesPage>();
            await Navigation.PushAsync(page);
        }

        // Navigointi uuden kierroksen aloitukseen
        private async void OnNewRoundClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetRequiredService<NewRoundPage>();
            await Navigation.PushAsync(page);
        }

        // Navigointi tuloksiin
        private async void OnResultsClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetRequiredService<ResultsPage>();
            await Navigation.PushAsync(page);
        }

        private async void OnHistoryClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetRequiredService<HistoryPage>();
            await Navigation.PushAsync(page);
        }
    }
}