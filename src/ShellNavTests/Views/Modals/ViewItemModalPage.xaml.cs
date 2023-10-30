using ShellNavTests.ViewModels.Modals;

namespace ShellNavTests.Views.Modals;

public partial class ViewItemModalPage : ContentPage
{
    public ViewItemModalPage(ViewItemModalPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        Loaded += ((ViewItemModalPageViewModel)BindingContext).Pages_Loaded;
    }
    ~ViewItemModalPage()
    {
        Loaded -= ((ViewItemModalPageViewModel)BindingContext).Pages_Loaded;
    }

    #region Methods
    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            ((ViewItemModalPageViewModel)BindingContext).OnAppearing();
        }
        catch (Exception) { }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        try
        {
            ((ViewItemModalPageViewModel)BindingContext).OnDisappearing();
        }
        catch (Exception) { }
    }

    // Disable Android Back Button
    protected override bool OnBackButtonPressed() => false;
    #endregion
}