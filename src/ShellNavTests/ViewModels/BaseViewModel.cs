using AndreasReitberger.Shared.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ShellNavTests.Models;

namespace ShellNavTests.ViewModels
{
    public partial class BaseViewModel : ViewModelBase
    {
        #region App
        [ObservableProperty]
        string version = string.Empty;

        [ObservableProperty]
        string build = string.Empty;

        [ObservableProperty]
        bool isTabletMode = false;

        [ObservableProperty]
        double progress = 0;

        #endregion

        #region Shell
        [ObservableProperty]
        bool keepFlyoutOpen = false;

        /*
        FlyoutBehavior _flyoutBehavior = DeviceInfo.Idiom == DeviceIdiom.Tablet ? FlyoutBehavior.Locked : FlyoutBehavior.Flyout;
        public FlyoutBehavior FlyoutBehavior
        {
            get { return _flyoutBehavior; }
            set { SetProperty(ref _flyoutBehavior, value); }
        }
        */
        public FlyoutBehavior FlyoutBehavior => KeepFlyoutOpen ? FlyoutBehavior.Locked : FlyoutBehavior.Flyout;

        #endregion

        #region Navigation
        [ObservableProperty]
        bool isViewShown = false;

        #endregion

        #region Connection
        [ObservableProperty]
        bool isConnecting = false;

        #endregion

        #region States
        [ObservableProperty]
        bool isListening = false;

        [ObservableProperty]
        bool isListeningToWebSocket = false;

        [ObservableProperty]
        bool closeWebSocketWhenLeaving = false;

        #endregion

        #region Module

        [ObservableProperty]
        string printerName = string.Empty;

        [ObservableProperty]
        bool timerActive = false;

        [ObservableProperty]
        bool wifiOn = false;

        [ObservableProperty]
        bool isAppStartupCompleted = false;

        [ObservableProperty]
        bool hasRestApiError = false;

        [ObservableProperty]
        bool isDemoModeOn;

        [ObservableProperty]
        bool refreshOnAppearing = true;

        [ObservableProperty]
        bool showTitleInBar = false;

        [ObservableProperty]
        bool showWikiUris = true;

        #endregion

        #region Constructor
        public BaseViewModel(IDispatcher dispatcher) : base(dispatcher)
        {
            Dispatcher = dispatcher;
        }


        protected void LoadSettings()
        {
            IsLoading = true;

            IsLoading = false;
        }
        #endregion

        #region Destructor
        ~BaseViewModel()
        {
            // Unregisters all when the ViewModel is GC.
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }
        #endregion

        #region Commands

        #region Navigation

        [RelayCommand]
        protected Task Close() => Back();

        [RelayCommand]
        protected Task Back() => ShellNavigator.Instance.GoBackAsync(Dispatcher, false);

        #endregion

        #endregion

        #region LiveCycle
        /*
        public async Task OnAppearingAsync()
        {

        }
        */

        public void OnDisappearing()
        {

        }
        #endregion
    }
}
