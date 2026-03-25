using FrisbeeScoreApp.Models;
using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class CourseEditPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private Course? _editingCourse;

    public CourseEditPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    // Lataa olemassa oleva rata muokkausta varten
    public void LoadCourse(Course course)
    {
        _editingCourse = course;

        Title = "Muokkaa rataa";
        NameEntry.Text = course.Name;
        HoleCountEntry.Text = course.HoleCount.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim() ?? string.Empty;
        string holeText = HoleCountEntry.Text?.Trim() ?? string.Empty;

        // Tarkistetaan nimi
        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlert("Virhe", "Anna radan nimi.", "OK");
            return;
        }

        if (name.Length > 50)
        {
            await DisplayAlert("Virhe", "Radan nimi saa olla enint‰‰n 50 merkki‰.", "OK");
            return;
        }

        // Tarkistetaan v‰yl‰m‰‰r‰
        if (!int.TryParse(holeText, out int holeCount) || holeCount <= 0)
        {
            await DisplayAlert("Virhe", "Anna kelvollinen v‰ylien m‰‰r‰.", "OK");
            return;
        }

        // Jos muokataan olemassa olevaa rataa
        if (_editingCourse != null)
        {
            // T‰ss‰ vaiheessa pidet‰‰n homma turvallisena:
            // nime‰ voi muokata, mutta v‰yl‰m‰‰r‰n muuttamista ei sallita viel‰.
            if (_editingCourse.HoleCount != holeCount)
            {
                await DisplayAlert("Huomio", "V‰yl‰m‰‰r‰n muuttamista ei viel‰ tueta. Muokkaa vain radan nime‰.", "OK");
                return;
            }

            _editingCourse.Name = name;

            await _databaseService.SaveCourseAsync(_editingCourse);
            await DisplayAlert("OK", "Rata p‰ivitetty.", "OK");
            await Navigation.PopAsync();
            return;
        }

        // Uusi rata
        var course = new Course
        {
            Name = name,
            HoleCount = holeCount
        };

        await _databaseService.SaveCourseAsync(course);
        await _databaseService.CreateDefaultHolesForCourseAsync(course.Id, holeCount);

        await DisplayAlert("OK", "Rata tallennettu.", "OK");
        await Navigation.PopAsync();
    }
}