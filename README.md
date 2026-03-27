

# 📱 FrisbeeScoreApp

**FrisbeeScoreApp** on mobiilisovellus frisbeegolfin tulosten kirjaamiseen ja seurantaan.
Sovellus on toteutettu **.NET MAUI -teknologialla** ja toimii täysin paikallisesti ilman verkkoyhteyttä.

---

# 🎯 Projektin tarkoitus

Sovelluksen tavoitteena on tarjota yksinkertainen ja käytännöllinen työkalu:

* ratojen hallintaan
* kierrosten tulosten kirjaamiseen
* tulosten analysointiin
* oman kehityksen seuraamiseen

Sovellus on suunniteltu erityisesti yksittäiselle käyttäjälle (ei moninpeliä tai pilvipalvelua).

---

# ⚙️ Teknologiat

* **.NET MAUI**
* **C#**
* **XAML**
* **SQLite (SQLiteAsyncConnection)**

---

# 🧱 Sovelluksen rakenne

## 📁 Models

* `Course` – rata
* `Hole` – väylä
* `Round` – kierros
* `RoundScore` – väyläkohtaiset tulokset
* `ScoreEntry` – UI-malli kierroksen syöttöön
* `RoundDisplayItem` – näkymää varten muokattu data
* `HistorySummary` – historian yhteenveto

## 📁 Services

* `DatabaseService`

  * vastaa tietokannan käsittelystä
  * sisältää CRUD-toiminnot ja hakulogiikan

## 📁 Views

* `MainPage` – etusivu
* `CoursesPage` – ratojen hallinta
* `CourseEditPage` – radan luonti/muokkaus
* `HoleEditorPage` – väyläkohtainen editori
* `NewRoundPage` – kierroksen aloitus
* `ScorecardPage` – tuloslaskuri
* `ResultsPage` – tulosnäkymä
* `HistoryPage` – historiatiedot

---

# 🗺️ Pääominaisuudet

## 🗺️ Ratojen hallinta

* ➕ Luo uusia ratoja
* ✏️ Muokkaa radan nimeä
* ❌ Poista rata (varmistusdialogilla)
* 🔢 Määritä väylien määrä
* 🎯 Muokkaa väyläkohtaiset par-arvot

👉 Radan poistaminen poistaa myös:

* kaikki siihen liittyvät kierrokset
* väylät
* tulokset

---

## 🥏 Kierroksen kirjaaminen

Käyttäjä voi:

1. valita radan
2. syöttää jokaisen väylän tuloksen
3. nähdä reaaliaikaisesti:

   * kokonaisheitot
   * tuloksen suhteessa pariin

### 🌦️ Säämerkintä (valinnainen)

Kierrokselle voi lisätä säätilan, esim:

* Aurinkoinen
* Puolipilvinen
* Pilvinen
* Sade
* Rankka sade
* Tuulinen
* Kuuma
* Kylmä
* Lumisade

---

## 🏆 Tulokset

Tulokset-näkymä näyttää valitun radan:

### 🔝 Top 5 kierrosta

* parhaat tulokset järjestettynä
* huomioi:

  * par-tulos
  * heitot
  * ajankohta

### 🕒 Viimeisin kierros

* viimeisin pelattu kierros valitulla radalla
* sisältää:

  * päivämäärän
  * heitot
  * tuloksen
  * sään

---

## 📊 Historia

Erillinen historia-näkymä:

### 🔍 Suodatus:

* vuosi
* kuukausi (tai kaikki kuukaudet)

### 📋 Näyttää:

* radan nimi
* päivämäärä
* heitot
* tulos pariin
* sää

### 📈 Yhteenveto:

* kierrosten määrä
* paras tulos
* keskiarvo

---

# 🎨 Käyttöliittymä

* mobiiliystävällinen layout
* korttipohjainen esitystapa
* selkeä navigointi
* värikoodatut tulokset:

  * 🟢 negatiivinen (hyvä tulos)
  * 🔴 positiivinen (huonompi tulos)
  * ⚪ par

---

# 🔤 Lajittelu

* radat näytetään **aakkosjärjestyksessä**
* lajittelu on **case-insensitive**
* tukee myös suomen kielen merkkejä (ä, ö, å)

---

# 💾 Tietokanta

Kaikki data tallennetaan paikallisesti SQLiteen.

## Taulut:

* `Course`
* `Hole`
* `Round`
* `RoundScore`

👉 Sovellus toimii täysin offline.

---

# 🚀 Käyttö

## 1. Luo rata

* anna nimi ja väylien määrä
* muokkaa väylien par-arvot

## 2. Pelaa kierros

* valitse rata
* syötä tulokset
* valitse halutessa sää
* tallenna

## 3. Tarkastele tuloksia

* Top 5
* viimeisin kierros

## 4. Tarkastele historiaa

* suodata kuukausittain
* seuraa kehitystä

---

# ⚠️ Huomioita

* ei käyttäjätilejä
* ei pilvitallennusta
* kaikki data on laitteen muistissa
* tietokanta alustetaan automaattisesti

---

# 🔧 Jatkokehitysideoita

* 📊 graafinen tilastointi
* 🧠 väyläkohtainen analyysi
* 👥 monen pelaajan tuki
* ☁️ pilvitallennus
* 🎯 yksittäisen kierroksen tarkempi näkymä
* 🎨 lisä UI-viimeistely

---

# 👨‍💻 Tekijä

Meikäläinen ja Tekoäly


