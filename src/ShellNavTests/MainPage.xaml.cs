using ShellNavTests.ViewModels;

namespace ShellNavTests
{
    public partial class MainPage : ContentPage
    {
        public MainPage(AppViewModel viewModel)
        {
            InitializeComponent(); 
            BindingContext = viewModel;
            Loaded += ((AppViewModel)BindingContext).Pages_Loaded;
        }
        ~MainPage()
        {
            Loaded -= ((AppViewModel)BindingContext).Pages_Loaded;
        }

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                //((AppViewModel)BindingContext).OnAppearing();
            }
            catch (Exception) { }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                ((AppViewModel)BindingContext).OnDisappearing();
            }
            catch (Exception) { }
        }
        #endregion
    }
}