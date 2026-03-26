using FrisbeeScoreApp.Models;
using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class ScorecardPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private Course? _course;
    private List<ScoreEntry> _scoreEntries = new();

    public ScorecardPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;

        // Valinnainen s‰‰lista
        WeatherPicker.ItemsSource = new List<string>
        {
            "",
            "Aurinkoinen",
            "Puolipilvinen",
            "Pilvinen",
            "Sade",
            "Rankka sade",
            "Tuulinen",
            "Kuuma",
            "Kylm‰",
            "Lumisade"
        };
    }

    public async Task LoadCourseAsync(Course course)
    {
        _course = course;

        // N‰ytet‰‰n radan nimi
        CourseNameLabel.Text = course.Name;

        // Haetaan radan v‰yl‰t
        var holes = await _databaseService.GetHolesByCourseIdAsync(course.Id);

        // Muutetaan v‰yl‰t kierroksen syˆttˆriveiksi
        _scoreEntries = holes.Select(h => new ScoreEntry
        {
            HoleNumber = h.HoleNumber,
            Par = h.Par,
            Throws = 0
        }).ToList();

        ScoreList.ItemsSource = _scoreEntries;
        WeatherPicker.SelectedIndex = 0; // oletus = ei valintaa

        UpdateSummary();
    }

    private async void OnCalculateClicked(object sender, EventArgs e)
    {
        UpdateSummary();
        await DisplayAlert("OK", "Tulos p‰ivitetty.", "OK");
    }

    private async void OnFinishClicked(object sender, EventArgs e)
    {
        if (_course == null)
            return;

        int totalThrows = _scoreEntries.Sum(s => s.Throws);
        int totalPar = _scoreEntries.Sum(s => s.Par);
        int vsPar = totalThrows - totalPar;

        var round = new Round
        {
            CourseId = _course.Id,
            DatePlayed = DateTime.Now,
            TotalThrows = totalThrows,
            TotalVsPar = vsPar,
            Weather = WeatherPicker.SelectedItem?.ToString() ?? string.Empty
        };

        await _databaseService.SaveRoundAsync(round, _scoreEntries);

        await DisplayAlert("OK", "Kierros tallennettu!", "OK");
        await Navigation.PopAsync();
    }

    private void UpdateSummary()
    {
        int totalThrows = _scoreEntries.Sum(s => s.Throws);
        int totalPar = _scoreEntries.Sum(s => s.Par);
        int vsPar = totalThrows - totalPar;

        SummaryLabel.Text = $"Yhteens‰ heitot: {totalThrows} | Tulos pariin: {vsPar:+#;-#;0}";
    }
}