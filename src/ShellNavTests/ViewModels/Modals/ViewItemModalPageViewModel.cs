using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShellNavTests.Models;

namespace ShellNavTests.ViewModels.Modals
{
    [QueryProperty(nameof(Item), "item")]
    public partial class ViewItemModalPageViewModel : AppViewModel
    {
        #region QueryParameter
        [ObservableProperty]
        Item item;
        #endregion

        #region Constructor, LoadSettings

        public ViewItemModalPageViewModel(IDispatcher dispatcher) : base(dispatcher)
        {
            Dispatcher = dispatcher;

            IsLoading = true;
            LoadSettings();
            IsLoading = false;
        }
        new void LoadSettings()
        {

        }
        #endregion

        #region Commands

        #region Save

        [RelayCommand]
        Task Save() => Back();

        #endregion

        #region Refreshing
        bool RefreshCommand_CanExcecute() => !IsRefreshing;
        [RelayCommand(CanExecute = nameof(RefreshCommand_CanExcecute))]
        async Task Refresh()
        {
            try
            {
                //DispatchManager.Dispatch(Dispatcher, () => IsRefreshing = true);
                if (Item is not null)
                {
                    await RefreshCourseDataAsync();
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
            DispatchManager.Dispatch(Dispatcher, () => IsRefreshing = false);
        }

        #endregion

        #endregion

        #region Methods

        async Task RefreshCourseDataAsync()
        {
            try
            {
                if (Item is not null)
                {

                }
            }
            catch (Exception exc)
            {
                // Log error
            }
        }

        #endregion

        #region LifeCycle
        /// <summary>
        /// Runs once when the page is created (gets triggered after the 'OnAppearing' function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new async void Pages_Loaded(object sender, EventArgs e)
        {
            if (AppData.DataChanged)
            {
                //await RefreshDashboardAction();
                await DispatchManager.UpdateInNewTaskAsync(OnLoadDataAsync);
            }
            else
            {
                UpdateFromInstanceData();
                // Load image from the currently selected file
                if (Item is not null)
                {
                    await DispatchManager.UpdateInNewTaskAsync(RefreshCourseDataAsync);
                }
            }
        }
        public void OnAppearing()
        {
            try
            {
                DispatchManager.Dispatch(Dispatcher, () => IsBusy = true);
                LoadSettings();

                if (AppData.DataChanged)
                {
                    Task.Run(Refresh);
                }
                else
                {
                    UpdateFromInstanceData();
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
            DispatchManager.Dispatch(Dispatcher, () => IsBusy = false);
            IsStartUp = false;
        }
        #endregion
    }
}
