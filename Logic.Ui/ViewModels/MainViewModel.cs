using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Logic.Ui.Commands;

namespace Logic.Ui.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        private string _appTitle = "Fiverr MVVM Demo";
        private string _name = string.Empty;
        private string _greeting = string.Empty;
        private int _count;
        private string _status = "Ready";
        private bool _isBusy;
        private bool _isAboutOpen;
        private string _lastLoadedAt = string.Empty;

        public string AppTitle
        {
            get => _appTitle;
            set => SetProperty(ref _appTitle, value);
        }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    (GreetCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    Status = string.IsNullOrWhiteSpace(_name) ? "Ready" : $"Typing: {_name}";
                }
            }
        }

        public string Greeting
        {
            get => _greeting;
            private set => SetProperty(ref _greeting, value);
        }

        public int Count
        {
            get => _count;
            private set
            {
                if (SetProperty(ref _count, value))
                    Status = $"Count changed to {value}";
            }
        }

        public string Status
        {
            get => _status;
            private set => SetProperty(ref _status, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    (LoadDataCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsAboutOpen
        {
            get => _isAboutOpen;
            set => SetProperty(ref _isAboutOpen, value);
        }

        public string LastLoadedAt
        {
            get => _lastLoadedAt;
            private set => SetProperty(ref _lastLoadedAt, value);
        }

        public ICommand GreetCommand { get; }
        public ICommand IncrementCommand { get; }
        public ICommand DecrementCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand LoadDataCommand { get; }
        public ICommand ToggleAboutCommand { get; }

        public MainViewModel()
        {
            GreetCommand = new RelayCommand(Greet, () => !string.IsNullOrWhiteSpace(Name));
            IncrementCommand = new RelayCommand(() => Count++);
            DecrementCommand = new RelayCommand(() => Count--);
            ResetCommand = new RelayCommand(() =>
            {
                Count = 0;
                Name = string.Empty;
                Greeting = string.Empty;
                Status = "Reset";
            });
            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync, () => !IsBusy);
            ToggleAboutCommand = new RelayCommand(() => IsAboutOpen = !IsAboutOpen);
        }

        private void Greet()
        {
            Greeting = $"Hello, {Name}!";
            Status = $"Greeted at {DateTime.Now:T}";
        }

        private async Task LoadDataAsync()
        {
            IsBusy = true;
            Status = "Loading...";
            await Task.Delay(1000);
            LastLoadedAt = $"Loaded at {DateTime.Now:T}";
            Status = "Loaded";
            IsBusy = false;
        }
    }
}