
# 📱 FrisbeeScoreApp

Paikallinen mobiilisovellus frisbeegolfin tulosten kirjaamiseen.
Sovellus on toteutettu **.NET MAUI:lla** ja käyttää **SQLite-tietokantaa**, joten kaikki data tallennetaan laitteelle ilman verkkoyhteyttä.

---

# 🎯 Sovelluksen tarkoitus

Sovelluksen tavoitteena on tarjota yksinkertainen ja käytännöllinen työkalu:

* kierrosten tulosten kirjaamiseen
* ratojen hallintaan
* tulosten seurantaan
* oman pelikehityksen tarkasteluun

---

# ⚙️ Teknologiat

* .NET MAUI
* C#
* SQLite (SQLiteAsyncConnection)
* XAML (UI)

---

# 📦 Pääominaisuudet

## 🗺️ Ratojen hallinta

Käyttäjä voi:

* ➕ luoda uusia ratoja
* ✏️ muokata radan nimeä
* ❌ poistaa radan (varmistusdialogilla)
* 🔢 määrittää väylien määrän
* 🎯 muokata jokaisen väylän par-arvon

---

## 🥏 Kierroksen pelaaminen (tuloslaskuri)

Käyttäjä voi:

1. valita radan
2. syöttää jokaisen väylän heittotuloksen
3. nähdä:

   * kokonaisheitot
   * tulos suhteessa pariin

Lisäksi:

* 🌦️ kierrokselle voi valita **säätilan (valinnainen)**
* kierros voidaan tallentaa historiaan

---

## 🏆 Tulokset-näkymä

Tulokset-sivu näyttää valitun radan:

### 🔝 Top 5 kierrosta

* järjestetty parhaasta tuloksesta
* huomioi:

  * par-tulos
  * heittojen määrä
  * ajankohta

### 🕒 Viimeisin kierros

* viimeisin pelattu kierros kyseisellä radalla
* näyttää:

  * päivämäärä
  * heitot
  * tulos pariin
  * sää

---

## 📊 Historia

Erillinen historia-näkymä käyttäjän kierroksille.

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

# 🌦️ Säämerkintä

Käyttäjä voi lisätä kierrokselle säätilan, esim:

* Aurinkoinen
* Puolipilvinen
* Pilvinen
* Sade
* Rankka sade
* Tuulinen
* Kuuma
* Kylmä
* Lumisade

Sää näkyy:

* tuloksissa
* historiassa

---

# 💾 Tietojen tallennus

Kaikki tiedot tallennetaan paikallisesti SQLite-tietokantaan:

## Taulut:

* `Course` – radat
* `Hole` – väylät
* `Round` – kierrokset
* `RoundScore` – väyläkohtaiset tulokset

---

# 🧠 Sovelluksen rakenne

## 📁 Models

* Course
* Hole
* Round
* RoundScore
* ScoreEntry (UI-malli)
* RoundDisplayItem (näyttödata)
* HistorySummary

## 📁 Services

* DatabaseService

  * vastaa kaikesta tietokannan käsittelystä

## 📁 Views

* MainPage
* CoursesPage
* CourseEditPage
* HoleEditorPage
* NewRoundPage
* ScorecardPage
* ResultsPage
* HistoryPage

---

# 🎨 UI / Käyttökokemus

* selkeä mobiili-layout
* korttipohjainen listaus
* erilliset näkymät eri tarkoituksiin
* ei ylikuormitettu yhdellä sivulla

---

# 🚀 Käyttö

## 1. Luo rata

* lisää nimi ja väylien määrä
* muokkaa väylien par-arvot

## 2. Pelaa kierros

* valitse rata
* syötä tulokset väylittäin
* valitse halutessa sää
* tallenna kierros

## 3. Tarkastele tuloksia

* Top 5 per rata
* viimeisin kierros

## 4. Tarkastele historiaa

* suodata kuukausittain
* tarkastele kehitystä

---

# ⚠️ Huomioita

* Sovellus toimii täysin offline
* Tietokanta sijaitsee laitteen muistissa
* Sovellus ei sisällä käyttäjätilejä tai pilvitallennusta

---

# 🔧 Jatkokehitysideoita

Mahdollisia jatkokehityksiä:

* 📈 väyläkohtainen analyysi
* 📊 graafinen tilastointi
* 🏁 kierroksen tarkempi näkymä
* 🎨 värikoodatut tulokset (birdie / bogey)
* ☁️ pilvitallennus
* 👥 monen pelaajan tuki

---

# 👨‍💻 Tekijä

Meikäläinen ja toveri TekoÄly.
