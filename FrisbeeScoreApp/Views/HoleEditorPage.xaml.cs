using FrisbeeScoreApp.Models;
using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class HoleEditorPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private Course? _course;
    private List<Hole> _holes = new();

    public HoleEditorPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService; // Tietokantapalvelu käyttöön
    }

    public async Task LoadCourseAsync(Course course)
    {
        _course = course;

        // Näytetään radan nimi otsikossa
        CourseNameLabel.Text = $"Väylät – {course.Name}";

        // Haetaan väylät tietokannasta
        _holes = await _databaseService.GetHolesByCourseIdAsync(course.Id);

        // Näytetään väylät listassa
        HolesList.ItemsSource = _holes;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_holes.Count == 0)
        {
            await DisplayAlert("Virhe", "Väyliä ei löytynyt.", "OK");
            return;
        }

        // Tallennetaan kaikki väylät yksi kerrallaan
        foreach (var hole in _holes)
        {
            // Varmistetaan että par on järkevä
            if (hole.Par <= 0)
            {
                await DisplayAlert("Virhe", $"Väylän {hole.HoleNumber} par-arvon pitää olla suurempi kuin 0.", "OK");
                return;
            }

            await _databaseService.SaveHoleAsync(hole);
        }

        await DisplayAlert("OK", "Väylät tallennettu.", "OK");
        await Navigation.PopAsync();
    }
}