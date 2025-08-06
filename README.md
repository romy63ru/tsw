# WpfCarSalesApp

Jednoduchá WPF aplikace v jazyce C# pro analýzu prodaných vozů ze souboru XML.

## Popis

Aplikace načítá údaje o prodeji vozidel ze souboru XML, filtruje prodeje uskutečněné během víkendů (sobota a neděle) a vypočítává celkovou cenu bez a s DPH pro každý model.

---

## Uživatelské rozhraní

- Tlačítko **Načíst XML** – výběr a načtení XML souboru
- Tabulka **Auta** – seznam všech záznamů o prodeji
- Tabulka **Souhrn o víkendu** – agregované součty dle modelu za víkendové prodeje

> Po načtení souboru je možné tabulku Auta upravovat, přičemž výsledky se automaticky přepočítají.

---

## Použité technologie

- .NET 6/7/8 nebo .NET Framework
- WPF (XAML)
- C#
- `ObservableCollection<T>`
- `INotifyPropertyChanged`
- LINQ to XML

---

## Struktura XML

Očekávaná struktura vstupního XML:

```xml
<Auta>
  <Auto>
    <NazevModelu>Škoda Fabia</NazevModelu>
    <DatumProdeje>2010-12-05</DatumProdeje>
    <Cena>350000</Cena>
    <DPH>20</DPH>
  </Auto>
  <!-- další položky -->
</Auta>
```

## Spuštění projektu

1.	Otevři řešení WpfCarSalesApp.sln ve Visual Studiu
2.	Spusť projekt pomocí F5 nebo tlačítka Start
3.	Klikni na Načíst XML a vyber vstupní soubor (např. auta.xml)

--- 

## Funkcionalita

- Načtení a zpracování XML souboru s prodeji
- Automatické filtrování a seskupování víkendových prodejů podle modelu
- Zobrazení dat v DataGrid tabulkách
- Dynamické přepočítání souhrnu při změně dat
- Možnost použití [Display(Name = "...")] pro pojmenování sloupců



## Možné rozšíření

- Export výsledků do CSV nebo Excelu
- Filtrování dle rozsahu datumu
- Třídění, vyhledávání, a další UI prvky
- Refaktoring do MVVM architektury