using FrisbeeScoreApp.Models;
using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class NewRoundPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly IServiceProvider _serviceProvider;

    public NewRoundPage(DatabaseService databaseService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _databaseService = databaseService;   // Tietokantapalvelu
        _serviceProvider = serviceProvider;   // Sivujen avaus DI:n kautta
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Haetaan radat pickerille
        var courses = await _databaseService.GetCoursesAsync();
        CoursePicker.ItemsSource = courses;
    }

    private async void OnStartRoundClicked(object sender, EventArgs e)
    {
        // Tarkistetaan että rata on valittu
        if (CoursePicker.SelectedItem is not Course selectedCourse)
        {
            await DisplayAlert("Virhe", "Valitse rata ennen aloittamista.", "OK");
            return;
        }

        // Avataan tuloslaskurisivu valitulle radalle
        var page = _serviceProvider.GetRequiredService<ScorecardPage>();
        await page.LoadCourseAsync(selectedCourse);

        await Navigation.PushAsync(page);
    }
}