using CommunityToolkit.Mvvm.ComponentModel;
using ShellNavTests.Views.Modals;

namespace ShellNavTests.Models
{
    public class ShellNavigator : ObservableObject//, IShellNavigator
    {
        #region Instance
        static ShellNavigator _instance = null;
        static readonly Lock Lock = new();
        public static ShellNavigator Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new ShellNavigator();
                }
                return _instance;
            }

            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }

        }
        #endregion

        #region Properties

        public string CurrentRoute
        {
            get => GetCurrentRoute();
        }

        string _previousRoute = string.Empty;
        public string PreviousRoute
        {
            get => _previousRoute;
            private set
            {
                if (_previousRoute == value) return;
                _previousRoute = value;
                OnPropertyChanged();
            }
        }

        string _rootPage = string.Empty;
        public string RootPage
        {
            get => _rootPage;
            set
            {
                if (_rootPage == value) return;
                _rootPage = value;
                OnPropertyChanged();
            }
        }

        List<string> _availableEntryPages = new()
        {
            nameof(MainPage),
        };
        public List<string> AvailableEntryPages
        {
            get => _availableEntryPages;
            private set
            {
                if (_availableEntryPages == value) return;
                _availableEntryPages = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public ShellNavigator()
        {
            RootPage = nameof(MainPage);
        }
        #endregion

        #region Methods

        public async Task<bool> GoToAsync(IDispatcher dispatcher, string target, Dictionary<string, object> parameters, bool flyoutIsPresented = false, int delay = -1, bool animate = true)
        {
            try
            {
                if (Shell.Current.FlyoutBehavior == FlyoutBehavior.Flyout)
                {
                    Shell.Current.FlyoutIsPresented = flyoutIsPresented;
                }
                if (delay != -1)
                {
                    await Task.Delay(delay);
                }

                await DispatchManager.DispatchAsync(dispatcher, async () =>
                {
                    try
                    {
                        PreviousRoute = GetCurrentRoute();
                        if (parameters == null)
                            await Shell.Current.GoToAsync(state: target, animate: animate);
                        else
                            await Shell.Current.GoToAsync(state: target, parameters: parameters, animate: animate);
                    }
                    catch (Exception exc)
                    {
                    }
                });
                return true;
            }
            catch (Exception exc)
            {
                // Log error
                return false;
            }
        }

        [Obsolete("Use method with IDispatcher instead")]
        public async Task<bool> GoToAsync(string target, Dictionary<string, object> parameters, bool flyoutIsPresented = false, int delay = -1, bool animate = true)
        {
            try
            {
                if (Shell.Current.FlyoutBehavior == FlyoutBehavior.Flyout)
                {
                    Shell.Current.FlyoutIsPresented = flyoutIsPresented;
                }
                if (delay != -1)
                {
                    await Task.Delay(delay);
                }
                // Workaround for #13510 - https://github.com/xamarin/Xamarin.Forms/issues/13510
                else if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    //await Task.Delay(50);
                }

                await DispatchManager.UpdateUIThreadSaveAsync(async () =>
                {
                    try
                    {
                        PreviousRoute = GetCurrentRoute();
                        if (parameters == null)
                            await Shell.Current.GoToAsync(state: target, animate: animate);
                        else
                            await Shell.Current.GoToAsync(state: target, parameters: parameters, animate: animate);
                    }
                    catch (Exception exc)
                    {

                    }
                });
                return true;
            }
            catch (Exception exc)
            {
                // Log error
                return false;
            }
        }

        public Task<bool> GoToAsync(IDispatcher dispatcher, string target, bool flyoutIsPresented = false, int delay = -1, bool animate = true)
        => GoToAsync(dispatcher: dispatcher, target: target, parameters: null, flyoutIsPresented: flyoutIsPresented, delay: delay, animate: animate);

        public Task GoBackAsync(IDispatcher dispatcher, bool flyoutIsPresented = false, int delay = -1, bool animate = true, bool confirm = false)
        => GoBackAsync(dispatcher: dispatcher, null, flyoutIsPresented: flyoutIsPresented, delay: delay, animate: animate, confirm: confirm);

        public async Task GoBackAsync(IDispatcher dispatcher, Dictionary<string, object> parameters, bool flyoutIsPresented = false, int delay = -1, bool animate = true, bool confirm = false)
        {
            if (confirm)
            {
                bool result = await Shell.Current.DisplayAlert(
                        "Strings.DialogConfirmGoBackHeadline",
                        "Strings.DialogConfirmGoBackContent",
                        "Strings.GoBack",
                        "Strings.Close"
                        );

                if (result)
                {
                    _ = await GoToAsync(dispatcher: dispatcher, "..", parameters, flyoutIsPresented, delay, animate);
                }
            }
            else
            {
                _ = await GoToAsync(dispatcher: dispatcher, "..", parameters, flyoutIsPresented, delay, animate);
            }
        }


        public bool IsCurrentPathRoot()
        {
            try
            {
                string[] parts = Shell.Current.CurrentState.Location.OriginalString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                return parts.Length <= 1;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        string GetCurrentRoute()
        {
            try
            {
                ShellNavigationState state = Shell.Current.CurrentState;
                if (state == null) return string.Empty;

                string lastPart = state.Location.ToString().Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).LastOrDefault();
                return lastPart;
            }
            catch (Exception exc)
            {
                return string.Empty;
            }
        }

        #endregion

        #region RegisterRoutes

        public void RegisterRoutes()
        {
            // AppShell MenuItems
            Routing.RegisterRoute(nameof(NewItemModalPage), typeof(NewItemModalPage));
            Routing.RegisterRoute(nameof(ViewItemModalPage), typeof(ViewItemModalPage));
            Routing.RegisterRoute(nameof(ViewItem2ModalPage), typeof(ViewItem2ModalPage));
            Routing.RegisterRoute(nameof(ViewItemWithCollectionViewModalPage), typeof(ViewItemWithCollectionViewModalPage));
            Routing.RegisterRoute(nameof(ViewItemWithSimpleCollectionViewModalPage), typeof(ViewItemWithSimpleCollectionViewModalPage));
        }

        #endregion
    }
}
