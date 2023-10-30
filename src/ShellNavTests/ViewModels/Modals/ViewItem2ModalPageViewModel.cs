using AndreasReitberger.Shared.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShellNavTests.Models;
using System.Diagnostics;
using System.Reflection;

namespace ShellNavTests.ViewModels.Modals
{
    [QueryProperty(nameof(Item), "item")]
    public partial class ViewItem2ModalPageViewModel : AppViewModel
    {
        #region QueryParameter
        [ObservableProperty]
        Item item;
        #endregion

        #region Constructor, LoadSettings

        public ViewItem2ModalPageViewModel(IDispatcher dispatcher) : base(dispatcher)
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
                    await Task.Delay(10000);
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
        public new void Pages_Loaded(object sender, EventArgs e)
        {
            Stopwatch watch = new();
            string methodName = $"{nameof(ViewItem2ModalPageViewModel)}.{nameof(Pages_Loaded)}";
            StopWatchHelper.Start(Dispatcher, ref watch, methodName);

            Task.Run(async () =>
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
            });

            StopWatchHelper.Stop(Dispatcher, ref watch, methodName);
            Debug.WriteLine($"{methodName}: Done in {watch.Elapsed}");
            watch = null;
        }
        public void OnAppearing()
        {
            try
            {
                Stopwatch watch = new();
                string methodName = $"{nameof(ViewItem2ModalPageViewModel)}.{nameof(OnAppearing)}";
                StopWatchHelper.Start(Dispatcher, ref watch, methodName);

                DispatchManager.Dispatch(Dispatcher, () => IsBusy = true);
                LoadSettings();
                //Force refresh to investigate time spent
                if (AppData.DataChanged || true)
                {
                    Task.Run(Refresh);
                }
                else
                {
                    UpdateFromInstanceData();
                }
                StopWatchHelper.Stop(Dispatcher, ref watch, methodName);
                Debug.WriteLine($"{methodName}: Done in {watch.Elapsed}");
                watch = null;
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
