using FrisbeeScoreApp.Models;
using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class ResultsPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public ResultsPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCoursesAsync();
    }

    // Lataa radat pickerille
    private async Task LoadCoursesAsync()
    {
        var courses = await _databaseService.GetCoursesAsync();
        CoursePicker.ItemsSource = courses;

        TopResultsList.ItemsSource = null;
        LatestRoundLabel.Text = "Valitse rata nähdäksesi viimeisimmän kierroksen.";
    }

    // Kun rata vaihtuu, haetaan top 5 ja viimeisin kierros
    private async void OnCourseSelectedChanged(object sender, EventArgs e)
    {
        if (CoursePicker.SelectedItem is not Course selectedCourse)
            return;

        var topResults = await _databaseService.GetTopRoundsByCourseAsync(selectedCourse.Id, 5);
        TopResultsList.ItemsSource = topResults;

        var latestRound = await _databaseService.GetLatestRoundByCourseAsync(selectedCourse.Id);

        if (latestRound == null)
        {
            LatestRoundLabel.Text = "Tälle radalle ei ole vielä tallennettu kierroksia.";
            return;
        }

        LatestRoundLabel.Text =
            $"Päivä: {latestRound.DatePlayed:dd.MM.yyyy HH:mm}\n" +
            $"Heitot: {latestRound.TotalThrows}\n" +
            $"Pariin: {latestRound.TotalVsPar:+#;-#;0}";
    }
}