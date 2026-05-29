# AssetBrowser

AssetBrowser ist ein bewusst einfach gestartetes Lernprojekt für **C#**, **.NET 10**, **WPF** und **MVVM**.

Ziel ist es, eine kleine, gut verständliche Desktop-Anwendung Schritt für Schritt auszubauen. Die Anwendung stellt digitale Assets wie Bilder, Videos oder Dokumente dar und dient als Übungsprojekt für typische WPF-Konzepte.

## Lernziele

Mit diesem Projekt sollen unter anderem folgende Themen praktisch geübt werden:

- WPF-Grundlagen
- MVVM
- Data Binding
- Commands
- Layout mit WPF-Controls
- editierbare Detailansichten
- Styles und Resource Dictionaries
- Theming
- Animationen
- später Services, Dependency Injection und REST API-Anbindung

## Fachliches Szenario

Ein **Asset** ist ein digitales Objekt, zum Beispiel:

- Bild
- Video
- Dokument
- Grafik

In der aktuellen ersten Version arbeitet die Anwendung ausschließlich mit **lokalen Mockdaten**.

## Technischer Stand von Version 1

Diese erste Version enthält bewusst nur eine kleine und gut verständliche Basis:

- 1 Model: `AssetItem`
- 1 ViewModel: `MainViewModel`
- 1 View: `MainWindow`
- Mockdaten im ViewModel
- Auswahl eines Assets
- editierbare Detailansicht
- einfache Commands zum Hinzufügen, Löschen und Abwählen
- CommunityToolkit.Mvvm für Observable Properties und Commands

## Projektstruktur

```text
AssetBrowser/
├─ Models/
│  └─ AssetItem.cs
├─ ViewModels/
│  └─ MainViewModel.cs
├─ MainWindow.xaml
├─ MainWindow.xaml.cs
├─ App.xaml
├─ App.xaml.cs
└─ README.md
```

## Verwendete Technologien

- .NET 10
- WPF
- MVVM
- CommunityToolkit.Mvvm

## Verwendete CommunityToolkit.Mvvm-Features

In Version 1 werden diese Features verwendet:

- `ObservableObject`
- `ObservableProperty`
- `RelayCommand`
- `NotifyCanExecuteChangedFor`

## Aktuelle Funktionen

Die Anwendung bietet derzeit folgende Funktionen:

- Anzeige einer Asset-Liste auf der linken Seite
- Anzeige der Details des ausgewählten Assets auf der rechten Seite
- Bearbeitung der Detailfelder direkt in der Oberfläche
- Hinzufügen eines neuen Mock-Assets
- Löschen des aktuell ausgewählten Assets
- Aufheben der aktuellen Auswahl

## WPF-Konzepte in Version 1

Diese erste Version enthält bereits wichtige WPF-Bausteine:

- `DataContext`
- `Binding`
- `TwoWay Binding`
- `Grid`
- `StackPanel`
- `Border`
- `TextBlock`
- `TextBox`
- `ComboBox`
- `CheckBox`
- `DatePicker`
- `ListBox`
- `Button`
- einfaches `DataTemplate` für die Asset-Liste

## Erste Architekturentscheidung

Die Architektur ist absichtlich **einfach** gehalten:

- noch **keine Dependency Injection**
- noch **keine REST API**
- noch **keine Datenbank**
- noch **keine Navigation**
- noch **keine Service-Schicht**

Das Projekt soll leicht verständlich bleiben und sich gut für Lehre, Übungen und schrittweise Erweiterungen eignen.

## Entwicklungsschritte

Die App soll in kleinen, didaktisch sinnvollen Schritten weiterentwickelt werden.

### Schritt 1: Basisversion mit MVVM und Mockdaten

Bereits umgesetzt:

- einfaches Datenmodell
- MainViewModel mit Mockdaten
- Liste und Detailansicht
- Commands für Add, Delete und Clear Selection
- editierbare Bindings in der Oberfläche

### Schritt 2: Suche und Filter

Bereits umgesetzt:

- Suchfeld für Titel oder Dateinamen
- Filter nach Asset-Typ
- Filter nach Freigabestatus
- Einführung in `CollectionView` und Filterlogik
- gefilterte Anzeige der Asset-Liste
- automatische Aktualisierung der Filter bei Eingaben
- Rücksetzen der Auswahl, wenn ein Asset nicht mehr zum aktiven Filter passt

## Umgesetzte Entwicklungsschritte

### Ausgeführt: Schritt 1

Ergänzt wurden:

- `AssetItem` als einfaches Model
- `MainViewModel` mit Mockdaten
- Commands für Hinzufügen, Löschen und Auswahl aufheben
- grundlegende Asset-Liste mit Detailansicht
- editierbare Bindings in der Oberfläche

### Ausgeführt: Schritt 2

Ergänzt wurden:

- Suchfeld für `Title` und `FileName`
- Filter nach Asset-Typ
- Filter nach Freigabestatus
- `ICollectionView` für die gefilterte Anzeige
- einfache, didaktisch nachvollziehbare Filterlogik im ViewModel

### Schritt 3: Bessere Darstellung mit DataTemplates

Bereits umgesetzt:

- schönere Listendarstellung je Asset
- visuelle Hervorhebung wichtiger Informationen
- stärkere Trennung zwischen Daten und Darstellung
- Typ-Badge, Statusanzeige und Metadaten in jedem Listeneintrag
- ein zentrales `DataTemplate` für die Asset-Liste
- bessere visuelle Struktur der Listeneinträge
- Anzeige von Asset-Typ, Status, Ersteller und Datum direkt in der Liste
- stärkere Trennung zwischen Daten und Darstellung in der View
- `ThumbnailPath` wird jetzt als Bildvorschau in Liste und Detailbereich dargestellt
- `AssetItem` verwendet jetzt ebenfalls das MVVM CommunityToolkit
- Properties im Model werden über `[ObservableProperty]` erzeugt
- die Benachrichtigungslogik im Model bleibt damit konsistent zum restlichen Projekt

### Schritt 4: Styles

Bereits umgesetzt:

- zentrale Styles für Buttons, TextBoxen und Container
- konsistenteres Erscheinungsbild
- Einführung in wiederverwendbare UI-Gestaltung
- gemeinsame Brushes und Abstände in `Window.Resources`
- Styles für Card-Container, Überschriften, Labels, Eingabefelder und Status-Badges
- weniger direkt verteilte visuelle Werte im XAML

### Schritt 5: Resource Dictionaries

Bereits umgesetzt:

- Auslagern von Styles und Farben
- Aufteilung der UI-Ressourcen in eigene Dateien
- bessere Struktur für größere WPF-Projekte
- `Brushes.xaml` für Farbressourcen
- `Styles.xaml` für wiederverwendbare Styles
- `Templates.xaml` für DataTemplates
- Resource Dictionaries im Ordner `Resources`
- Trennung von Brushes, Styles und Templates in eigene XAML-Dateien
- Einbindung der Dictionaries über `MergedDictionaries` global in `App.xaml` geladen
- `MainWindow.xaml` verwendet die Ressourcen nur noch, statt sie lokal erneut zu importieren

### Schritt 6: Light- und Dark-Theme

Bereits umgesetzt:

- Farbpaletten für helle und dunkle Oberfläche
- Theme-Wechsel zur Laufzeit
- Einstieg in Theming mit WPF-Ressourcen
- `LightTheme.xaml` und `DarkTheme.xaml` als austauschbare Farbpaletten
- Theme-Umschaltung über `App.xaml` und `App.xaml.cs`
- einfache Theme-Auswahl direkt in der Oberfläche
- zwei getrennte Theme-Dictionaries für Light und Dark
- Runtime-Wechsel des aktiven Theme-ResourceDictionary
- zusätzliche Theme-Farben für Fenster, Eingabefelder und Buttons
- Theme-ComboBox in der MainWindow-Oberfläche
- themeabhängige Farbressourcen werden jetzt per `DynamicResource` gebunden
- die Oberfläche aktualisiert sich dadurch sofort beim Wechsel zwischen Light und Dark
- ComboBox-Ressourcen für das Theme-Dropdown wurden ergänzt, damit auch dessen Darstellung mit dem aktiven Theme wechselt
- die Dropdown-Farben werden jetzt über passende Theme-Ressourcen und System-Brush-Keys gesteuert
- die ComboBox verwendet jetzt ein eigenes Theme-Template für geschlossenen Zustand, Popup und Auswahlzustand
- derselbe ComboBox-Stil wird jetzt konsistent auch für die Filter-Dropdowns verwendet

### Schritt 7: Animationen

Bereits umgesetzt:

- dezente Hover-Animationen für Asset-Listeneinträge
- leichte Skalierung beim Überfahren
- weicher Schatten für mehr Tiefenwirkung
- animierter Übergang der Rahmenfarbe

### Schritt 8: Services

Geplante Erweiterung:

- Asset-Daten aus einem einfachen Service statt direkt im ViewModel
- bessere Trennung von UI-Logik und Datenbereitstellung
- Vorbereitung auf testbarere Strukturen

### Schritt 9: Dependency Injection

Geplante Erweiterung:

- Einführung einer einfachen DI-Konfiguration
- ViewModels und Services über DI bereitstellen
- Vorbereitung auf größere Anwendungen

### Schritt 10: REST API-Anbindung an Agravity

Geplante Erweiterung:

- Assets nicht nur aus Mockdaten laden
- Anbindung an eine externe API
- Laden, Anzeigen und später eventuell Bearbeiten echter Daten

## Anwendung starten

Voraussetzungen:

- Visual Studio mit WPF-Unterstützung
- .NET 10 SDK

Start:

1. Lösung in Visual Studio öffnen
2. NuGet-Pakete wiederherstellen
3. Projekt starten

## Didaktischer Hinweis

Dieses Projekt ist **kein Enterprise-Template**. Es ist absichtlich klein gehalten, damit die Grundlagen von WPF und MVVM klar erkennbar bleiben.

Erst wenn die Basis verstanden ist, werden weitere Konzepte schrittweise ergänzt.
