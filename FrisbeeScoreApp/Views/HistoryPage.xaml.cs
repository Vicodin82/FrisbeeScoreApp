using FrisbeeScoreApp.Services;

namespace FrisbeeScoreApp.Views;

public partial class HistoryPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public HistoryPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadFiltersAsync();
        await LoadHistoryAsync();
    }

    // Lataa vuodet ja kuukaudet pickereihin
    private Task LoadFiltersAsync()
    {
        var currentYear = DateTime.Now.Year;

        YearPicker.ItemsSource = Enumerable.Range(currentYear - 5, 6)
            .Reverse()
            .Select(y => y.ToString())
            .ToList();

        MonthPicker.ItemsSource = new List<string>
        {
            "Kaikki",
            "Tammikuu",
            "Helmikuu",
            "Maaliskuu",
            "Huhtikuu",
            "Toukokuu",
            "Kes‰kuu",
            "Hein‰kuu",
            "Elokuu",
            "Syyskuu",
            "Lokakuu",
            "Marraskuu",
            "Joulukuu"
        };

        if (YearPicker.SelectedIndex == -1)
            YearPicker.SelectedItem = currentYear.ToString();

        if (MonthPicker.SelectedIndex == -1)
            MonthPicker.SelectedIndex = DateTime.Now.Month;

        return Task.CompletedTask;
    }

    // P‰ivitt‰‰ historian valitun suodatuksen mukaan
    private async Task LoadHistoryAsync()
    {
        if (YearPicker.SelectedItem == null || MonthPicker.SelectedItem == null)
            return;

        int year = int.Parse(YearPicker.SelectedItem.ToString()!);
        int? month = MonthPicker.SelectedIndex == 0 ? null : MonthPicker.SelectedIndex;

        var historyItems = await _databaseService.GetRoundHistoryAsync(year, month);
        HistoryList.ItemsSource = historyItems;

        var summary = await _databaseService.GetHistorySummaryAsync(year, month);

        if (summary.RoundCount == 0)
        {
            SummaryLabel.Text = "Ei kierroksia valitulla ajanjaksolla.";
            return;
        }

        SummaryLabel.Text =
            $"Kierroksia yhteens‰: {summary.RoundCount}\n" +
            $"Paras tulos: {summary.BestVsPar:+#;-#;0}\n" +
            $"Keskiarvo: {summary.AverageVsPar:+0.0;-0.0;0.0}";
    }

    // Kun vuosi tai kuukausi vaihtuu, p‰ivitet‰‰n historia
    private async void OnFilterChanged(object sender, EventArgs e)
    {
        await LoadHistoryAsync();
    }
}