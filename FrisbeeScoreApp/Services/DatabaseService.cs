using FrisbeeScoreApp.Models;
using SQLite;

namespace FrisbeeScoreApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        // Alustaa tietokannan ja luo taulut tarvittaessa
        public async Task InitAsync()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "frisbeescore.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Course>();
            await _database.CreateTableAsync<Hole>();
            await _database.CreateTableAsync<Round>();
            await _database.CreateTableAsync<RoundScore>();
        }

        // =========================
        // COURSE / RADAT
        // =========================

        // Hakee kaikki radat
        public async Task<List<Course>> GetCoursesAsync()
        {
            await InitAsync();
            return await _database!.Table<Course>().ToListAsync();
        }

        // Lisää uuden radan tai päivittää olemassa olevan
        public async Task<int> SaveCourseAsync(Course course)
        {
            await InitAsync();

            if (course.Id != 0)
                return await _database!.UpdateAsync(course);

            return await _database!.InsertAsync(course);
        }

        // Poistaa radan sekä siihen liittyvät väylät, kierrokset ja kierrostulokset
        public async Task<int> DeleteCourseAsync(Course course)
        {
            await InitAsync();

            // Poistetaan radan väylät
            var holes = await _database!.Table<Hole>()
                .Where(h => h.CourseId == course.Id)
                .ToListAsync();

            foreach (var hole in holes)
            {
                await _database.DeleteAsync(hole);
            }

            // Haetaan radan kierrokset
            var rounds = await _database.Table<Round>()
                .Where(r => r.CourseId == course.Id)
                .ToListAsync();

            foreach (var round in rounds)
            {
                // Poistetaan kierroksen väylätulokset
                var roundScores = await _database.Table<RoundScore>()
                    .Where(rs => rs.RoundId == round.Id)
                    .ToListAsync();

                foreach (var roundScore in roundScores)
                {
                    await _database.DeleteAsync(roundScore);
                }

                // Poistetaan itse kierros
                await _database.DeleteAsync(round);
            }

            // Lopuksi poistetaan itse rata
            return await _database.DeleteAsync(course);
        }

        // =========================
        // HOLE / VÄYLÄT
        // =========================

        // Hakee tietyn radan väylät järjestyksessä
        public async Task<List<Hole>> GetHolesByCourseIdAsync(int courseId)
        {
            await InitAsync();

            return await _database!.Table<Hole>()
                .Where(h => h.CourseId == courseId)
                .OrderBy(h => h.HoleNumber)
                .ToListAsync();
        }

        // Lisää uuden väylän tai päivittää olemassa olevan
        public async Task<int> SaveHoleAsync(Hole hole)
        {
            await InitAsync();

            if (hole.Id != 0)
                return await _database!.UpdateAsync(hole);

            return await _database!.InsertAsync(hole);
        }

        // Luo uudelle radalle oletusväylät (Par 3)
        public async Task CreateDefaultHolesForCourseAsync(int courseId, int holeCount)
        {
            await InitAsync();

            for (int i = 1; i <= holeCount; i++)
            {
                var hole = new Hole
                {
                    CourseId = courseId,
                    HoleNumber = i,
                    Par = 3
                };

                await _database!.InsertAsync(hole);
            }
        }

        // =========================
        // ROUND / KIERROKSET
        // =========================

        // Tallentaa kierroksen ja sen kaikki väylätulokset
        public async Task SaveRoundAsync(Round round, List<ScoreEntry> scores)
        {
            await InitAsync();

            // Tallennetaan kierroksen perustiedot
            await _database!.InsertAsync(round);

            // Tallennetaan jokaisen väylän tulos
            foreach (var score in scores)
            {
                var roundScore = new RoundScore
                {
                    RoundId = round.Id,
                    HoleNumber = score.HoleNumber,
                    Par = score.Par,
                    Throws = score.Throws
                };

                await _database.InsertAsync(roundScore);
            }
        }

        // Hakee kaikki kierrokset
        public async Task<List<Round>> GetAllRoundsAsync()
        {
            await InitAsync();
            return await _database!.Table<Round>().ToListAsync();
        }

        // =========================
        // TULOSNÄKYMÄT
        // =========================

        // Hakee kaikki kierrokset näyttömuotoon, mukana radan nimi
        public async Task<List<RoundDisplayItem>> GetAllRoundDisplayItemsAsync()
        {
            await InitAsync();

            var rounds = await _database!.Table<Round>().ToListAsync();
            var courses = await _database.Table<Course>().ToListAsync();

            return rounds
                .Select(round => new RoundDisplayItem
                {
                    RoundId = round.Id,
                    CourseName = courses.FirstOrDefault(c => c.Id == round.CourseId)?.Name ?? "Tuntematon rata",
                    DatePlayed = round.DatePlayed,
                    TotalThrows = round.TotalThrows,
                    TotalVsPar = round.TotalVsPar
                })
                .OrderByDescending(item => item.DatePlayed)
                .ToList();
        }

        // Hakee Top 5 kierrosta valitulta radalta
        public async Task<List<RoundDisplayItem>> GetTopRoundsByCourseAsync(int courseId, int topCount = 5)
        {
            await InitAsync();

            var course = await _database!.Table<Course>()
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var rounds = await _database.Table<Round>()
                .Where(r => r.CourseId == courseId)
                .ToListAsync();

            return rounds
                .OrderBy(r => r.TotalVsPar)          // Pienempi on parempi
                .ThenBy(r => r.TotalThrows)          // Tasatilanteessa vähemmän heittoja voittaa
                .ThenByDescending(r => r.DatePlayed) // Uudempi edelle, jos muuten sama
                .Take(topCount)
                .Select(round => new RoundDisplayItem
                {
                    RoundId = round.Id,
                    CourseName = course?.Name ?? "Tuntematon rata",
                    DatePlayed = round.DatePlayed,
                    TotalThrows = round.TotalThrows,
                    TotalVsPar = round.TotalVsPar
                })
                .ToList();
        }

        //Hakee viimeisimmän kierroksen valitulta radalta
        public async Task<RoundDisplayItem?> GetLatestRoundByCourseAsync(int courseId)
        {
            await InitAsync();

            var course = await _database!.Table<Course>()
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var latestRound = await _database.Table<Round>()
                .Where(r => r.CourseId == courseId)
                .OrderByDescending(r => r.DatePlayed)
                .FirstOrDefaultAsync();

            if (latestRound == null)
                return null;

            return new RoundDisplayItem
            {
                RoundId = latestRound.Id,
                CourseName = course?.Name ?? "Tuntematon rata",
                DatePlayed = latestRound.DatePlayed,
                TotalThrows = latestRound.TotalThrows,
                TotalVsPar = latestRound.TotalVsPar
            };
        }

        //Hakee historian vuoden ja kuukauden perusteella
        public async Task<List<RoundDisplayItem>> GetRoundHistoryAsync(int year, int? month = null)
        {
            await InitAsync();

            var rounds = await _database!.Table<Round>().ToListAsync();
            var courses = await _database.Table<Course>().ToListAsync();

            var filteredRounds = rounds
                .Where(r => r.DatePlayed.Year == year);

            if (month.HasValue)
            {
                filteredRounds = filteredRounds.Where(r => r.DatePlayed.Month == month.Value);
            }

            return filteredRounds
                .OrderByDescending(r => r.DatePlayed)
                .Select(round => new RoundDisplayItem
                {
                    RoundId = round.Id,
                    CourseName = courses.FirstOrDefault(c => c.Id == round.CourseId)?.Name ?? "Tuntematon rata",
                    DatePlayed = round.DatePlayed,
                    TotalThrows = round.TotalThrows,
                    TotalVsPar = round.TotalVsPar
                })
                .ToList();
        }

        //Historian yhteenveto
        public async Task<HistorySummary> GetHistorySummaryAsync(int year, int? month = null)
        {
            await InitAsync();

            var rounds = await _database!.Table<Round>().ToListAsync();

            var filteredRounds = rounds
                .Where(r => r.DatePlayed.Year == year);

            if (month.HasValue)
            {
                filteredRounds = filteredRounds.Where(r => r.DatePlayed.Month == month.Value);
            }

            var result = filteredRounds.ToList();

            if (result.Count == 0)
            {
                return new HistorySummary
                {
                    RoundCount = 0,
                    BestVsPar = 0,
                    AverageVsPar = 0
                };
            }

            return new HistorySummary
            {
                RoundCount = result.Count,
                BestVsPar = result.Min(r => r.TotalVsPar),
                AverageVsPar = result.Average(r => r.TotalVsPar)
            };
        }
    }
}