using AndreasReitberger.Shared.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShellNavTests.Models;
using ShellNavTests.Views.Modals;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShellNavTests.ViewModels
{
    [QueryProperty(nameof(CurrentItem), "item")]
    public partial class AppViewModel : BaseViewModel
    {
        #region QueryParameter
        [ObservableProperty]
        Item currentItem;

        #endregion

        #region Properties

        #region States
        [ObservableProperty]
        bool isLoadingAll = false;

        [ObservableProperty]
        bool dataChanged = false;

        #endregion

        #region ModalPopups

        [ObservableProperty]
        bool tutorialShown = false;

        [ObservableProperty]
        bool changeInfosShown = false;

        #endregion

        #region Misc
        [ObservableProperty]
        string lastMessage;

        [ObservableProperty]
        ObservableCollection<string> messages = new();
        #endregion

        #region Actions
        /// <summary>
        /// This action will be invoked if data has changed.
        /// </summary>
        protected Func<Task> OnDataChanged;
        #endregion

        #endregion

        #region Collections
        [ObservableProperty]
        ObservableCollection<Item> items = new();

        #endregion

        #region Constructor
        public AppViewModel(IDispatcher dispatcher) : base(dispatcher)
        {
            Dispatcher = dispatcher;
            LoadSettings();
        }
        #endregion

        #region Commands

        #region Navigations

        [RelayCommand]
        async Task NavigateTo(object parameter)
        {
            try
            {
                Stopwatch watch = new();
                string methodName = $"{nameof(AppViewModel)}.{nameof(NavigateTo)}";
                StopWatchHelper.Start(Dispatcher, ref watch, methodName);
                var data = new Dictionary<string, object>()
                {
                    {"item", new Item { Name = "Test"} }
                };
                if (parameter is string target)
                {
                    switch (target)
                    {
                        case "blank":
                            await Shell.Current.GoToAsync(nameof(ViewItemModalPage), true, data);
                            break;
                        case "blank_no_animation":
                            await Shell.Current.GoToAsync(nameof(ViewItemModalPage), false, data);
                            break;
                        case "blank_no_async_page_loaded":
                            await Shell.Current.GoToAsync(nameof(ViewItem2ModalPage), false, data);
                            break;
                        case "blank_with_collectionview":
                            await DispatchManager.DispatchAsync(Dispatcher, async () =>
                            {
                                await Shell.Current.GoToAsync(nameof(ViewItemWithCollectionViewModalPage), true, data);
                            });
                            break;
                        case "nav_with_collectionview":
                            await ShellNavigator.Instance.GoToAsync(Dispatcher, nameof(ViewItemWithCollectionViewModalPage), data, false);
                            break;
                        case "blank_with_simple_collectionview":
                            await DispatchManager.DispatchAsync(Dispatcher, async () =>
                            {
                                await Shell.Current.GoToAsync(nameof(ViewItemWithSimpleCollectionViewModalPage), true, data);
                            });
                            break;
                        case "nav_with_simple_collectionview":
                            await ShellNavigator.Instance.GoToAsync(Dispatcher, nameof(ViewItemWithSimpleCollectionViewModalPage), data, false);
                            break;
                        default:
                            await ShellNavigator.Instance.GoToAsync(Dispatcher, nameof(ViewItemModalPage), data, false);
                            break;
                    }
                }

                StopWatchHelper.Stop(Dispatcher, ref watch, methodName);
                LastMessage = $"{methodName}: Done in {watch.Elapsed}";
                Messages.Add(LastMessage);
                Debug.WriteLine(LastMessage);
                watch = null;
            }
            catch(Exception ex)
            {

            }
        }

        #endregion

        #region Firebase
        [RelayCommand]
        protected async Task LoadDataFromFirebase()
        {
            try
            {
                IsBusy = true;
                if (IsLoadingAll)
                    return;

                IsLoadingAll = true;
                if (MainThread.IsMainThread)
                {
                    // Only load in a new task if it's the MainThread.
                    await Task.Run(OnLoadDataAsync);
                }
                else
                {
                    await OnLoadDataAsync();
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
            IsBusy = false;
        }
        #endregion

        #region ShellNavigation

        [RelayCommand]
        protected async Task NewItem(object parameter)
        {
            try
            {
                IsBusy = true;
                await Task.Delay(10);
                if (parameter is string target)
                {
                    /*
                    switch (target.ToLower())
                    {
                        case "depot":
                            _ = await ShellNavigator.Instance.GoToAsync(ShellRoute.NewDepotModalPage);
                            break;
                        case "stock":
                            _ = await ShellNavigator.Instance.GoToAsync(ShellRoute.NewStockToDepotModalPage, new Dictionary<string, object> {
                                { "depot", CurrentDepot },
                            });
                            break;
                        case "transaction":
                            _ = await ShellNavigator.Instance.GoToAsync(ShellRoute.NewTransactionToStockModalPage, new Dictionary<string, object> {
                                { "depot", CurrentDepot },
                                { "stock", CurrentStock },
                            });
                            break;
                        case "dividend":
                            _ = await ShellNavigator.Instance.GoToAsync(ShellRoute.NewDividendToStockModalPage, new Dictionary<string, object> {
                                { "depot", CurrentDepot },
                                { "stock", CurrentStock },
                            });
                            break;
                        default:
                            break;
                    }
                    */
                }
                else
                {
                    throw new NotImplementedException($"The parameter type '{parameter?.GetType()}'  hasn't been setup yet!");
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
            IsBusy = false;
        }

        [RelayCommand]
        protected async Task EditItem(object parameter)
        {
            try
            {
                IsBusy = true;
                if (parameter is Item item)
                {
                    /**/
                    _ = await ShellNavigator.Instance.GoToAsync(nameof(NewItemModalPage), new()
                    {
                        { "item", item },
                    }, false);

                }
                else
                {
                    throw new NotImplementedException($"The parameter type '{parameter?.GetType()}'  hasn't been setup yet!");
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
            IsBusy = false;
        }

        [RelayCommand]
        protected async Task ViewItem(object parameter)
        {
            try
            {
                IsBusy = true;
                if (parameter is Item item)
                {
                    _ = await ShellNavigator.Instance.GoToAsync(nameof(ViewItemModalPage), new()
                    {
                        { "item", item },
                    }, false);
                }
                else
                {
                    throw new NotImplementedException($"The parameter type '{parameter?.GetType()}'  hasn't been setup yet!");
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
            IsBusy = false;
        }

        #endregion

        #endregion

        #region Methods

        #region DataLoading
        /// <summary>
        /// This is the default `Pages_Loaded` event. Nothing happens here. If needed, overwrite it in the 
        /// inherited view model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Pages_Loaded(object sender, EventArgs e)
        {
            //OnDefault, do noting.
            //Task.Run(OnLoadedAsync);
        }

        /// <summary>
        /// Loads the data needed for this input form modal.
        /// Attention! This code runs not on the UI Thread!
        /// </summary>
        /// <returns></returns>
        protected async Task OnLoadDataAsync()
        {
            try
            {
                //AppData.Items = await FirebaseHandler.Instance.GetCoursesAsync();
                AppData.Items = new() {
                    new() { Name = "My Item" },
                    new() { Name = "My second Item" },
                };
                AppData.LastRefresh = DateTime.Now;
                AppData.DataChanged = false;
                try
                {
                    await DispatchManager.DispatchAsync(Dispatcher, () =>
                    {
                        try
                        {
                            UpdateFromInstanceData();
                        }
                        catch (Exception exc)
                        {
                            // Log error
                        }
                        DispatchManager.Dispatch(Dispatcher, () => IsLoadingAll = false);
                    });
                }
                catch (Exception exc)
                {
                    // Log error
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
        }

        /// <summary>
        /// Updates the needed information from the RepetierClient.Instance.
        /// Important: This must run on the UIThread!
        /// </summary>
        protected void UpdateFromInstanceData()
        {
            try
            {
                Items = AppData.Items;
            }
            catch (Exception exc)
            {
                // Log error
            }
        }
        #endregion

        #endregion

        #region Lifecycle
        public async Task OnLoadedAsync()
        {
            try
            {
                if (AppData.DataChanged)
                {
                    await LoadDataFromFirebase();
                }
                else
                {
                    await DispatchManager.DispatchAsync(Dispatcher, () =>
                    {
                        try
                        {
                            UpdateFromInstanceData();
                        }
                        catch (Exception exc)
                        {
                            // Log error
                        }
                    });
                }
            }
            catch (Exception exc)
            {
                // Log error
            }
        }
        #endregion
    }
}
