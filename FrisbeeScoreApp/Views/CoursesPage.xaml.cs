using FrisbeeScoreApp.Models;
using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class CoursesPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly IServiceProvider _serviceProvider;

    public CoursesPage(DatabaseService databaseService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _databaseService = databaseService;
        _serviceProvider = serviceProvider;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCoursesAsync();
    }

    // Hakee radat ja näyttää ne listassa
    private async Task LoadCoursesAsync()
    {
        var courses = await _databaseService.GetCoursesAsync();
        CoursesList.ItemsSource = courses.OrderBy(c => c.Name).ToList();
    }

    // Uuden radan lisäys
    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        var page = _serviceProvider.GetRequiredService<CourseEditPage>();
        await Navigation.PushAsync(page);
    }

    // Radan nimen muokkaus
    private async void OnEditCourseClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Course selectedCourse)
        {
            var page = _serviceProvider.GetRequiredService<CourseEditPage>();
            page.LoadCourse(selectedCourse);
            await Navigation.PushAsync(page);
        }
    }

    // Väyläeditori
    private async void OnEditHolesClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Course selectedCourse)
        {
            var page = _serviceProvider.GetRequiredService<HoleEditorPage>();
            await page.LoadCourseAsync(selectedCourse);
            await Navigation.PushAsync(page);
        }
    }

    // Radan poisto varmistuksella
    private async void OnDeleteCourseClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Course selectedCourse)
        {
            bool confirm = await DisplayAlert(
                "Vahvista poisto",
                $"Haluatko varmasti poistaa radan \"{selectedCourse.Name}\"?",
                "Kyllä",
                "Peruuta");

            if (!confirm)
                return;

            await _databaseService.DeleteCourseAsync(selectedCourse);
            await LoadCoursesAsync();

            await DisplayAlert("OK", "Rata poistettu.", "OK");
        }
    }
}