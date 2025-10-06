# MVVM Demo App (.NET 8, WPF)

A simple, client-ready MVVM demo built with two projects:
- Ui.Desktop: WPF UI (no UI logic in code-behind).
- Logic.Ui: ViewModels and Commands.

This structure keeps the UI thin and all presentation logic in ViewModels.

## Prerequisites
- Windows 10/11
- .NET SDK 8.0+
- Visual Studio 2022 (17.8+) with “.NET desktop development” workload

## Solution layout
- Ui.Desktop (WPF, `net8.0-windows`, `UseWPF=true`)
  - `App.xaml`
  - `MainWindow.xaml` (+ `.xaml.cs`, only calls `InitializeComponent`)
- Logic.Ui (`net8.0`)
  - `ViewModels/`
    - `BaseViewModel.cs` (INotifyPropertyChanged)
    - `MainViewModel.cs` (app state, commands)
  - `Commands/`
    - `RelayCommand.cs` (sync)
    - `AsyncRelayCommand.cs` (async)

## Build & Run (Visual Studio)
1. Ensure project reference exists:
   - Right-click `Ui.Desktop` → __Add > Project Reference__ → check `Logic.Ui` → OK
2. Set startup project:
   - Right-click `Ui.Desktop` → __Set as Startup Project__
3. Build and run:
   - __Build > Rebuild Solution__ → F5

## Build & Run (CLI)
