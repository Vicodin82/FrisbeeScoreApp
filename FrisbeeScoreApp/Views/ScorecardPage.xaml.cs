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
    }

    public async Task LoadCourseAsync(Course course)
    {
        _course = course;

        // Näytetään radan nimi
        CourseNameLabel.Text = course.Name;

        // Haetaan radan väylät
        var holes = await _databaseService.GetHolesByCourseIdAsync(course.Id);

        // Muutetaan väylät kierroksen syöttöriveiksi
        _scoreEntries = holes.Select(h => new ScoreEntry
        {
            HoleNumber = h.HoleNumber,
            Par = h.Par,
            Throws = 0
        }).ToList();

        // Näytetään listassa
        ScoreList.ItemsSource = _scoreEntries;

        // Päivitetään yhteenveto
        UpdateSummary();
    }

    private async void OnCalculateClicked(object sender, EventArgs e)
    {
        UpdateSummary();
        await DisplayAlert("OK", "Tulos päivitetty.", "OK");
    }

    private async void OnFinishClicked(object sender, EventArgs e)
    {
        if (_course == null)
            return;

        // Lasketaan tulokset
        int totalThrows = _scoreEntries.Sum(s => s.Throws);
        int totalPar = _scoreEntries.Sum(s => s.Par);
        int vsPar = totalThrows - totalPar;

        // Luodaan kierros
        var round = new Round
        {
            CourseId = _course.Id,
            DatePlayed = DateTime.Now,
            TotalThrows = totalThrows,
            TotalVsPar = vsPar
        };

        // Tallennetaan
        await _databaseService.SaveRoundAsync(round, _scoreEntries);

        await DisplayAlert("OK", "Kierros tallennettu!", "OK");

        await Navigation.PopAsync();
    }

    private void UpdateSummary()
    {
        // Lasketaan kokonaistulos
        int totalThrows = _scoreEntries.Sum(s => s.Throws);
        int totalPar = _scoreEntries.Sum(s => s.Par);
        int vsPar = totalThrows - totalPar;

        SummaryLabel.Text = $"Yhteensä heitot: {totalThrows} | Tulos pariin: {vsPar:+#;-#;0}";
    }
}