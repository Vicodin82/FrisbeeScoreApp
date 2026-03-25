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
        await LoadDataAsync();
    }

    // Lataa historia ja ratalista pickerille
    private async Task LoadDataAsync()
    {
        var courses = await _databaseService.GetCoursesAsync();
        CoursePicker.ItemsSource = courses;

        var rounds = await _databaseService.GetAllRoundDisplayItemsAsync();
        ResultsList.ItemsSource = rounds;

        TopResultsList.ItemsSource = null;
    }

    // Kun rata vaihtuu pickerissä, haetaan sen Top 5
    private async void OnCourseSelectedChanged(object sender, EventArgs e)
    {
        if (CoursePicker.SelectedItem is Course selectedCourse)
        {
            var topResults = await _databaseService.GetTopRoundsByCourseAsync(selectedCourse.Id, 5);
            TopResultsList.ItemsSource = topResults;
        }
    }
}