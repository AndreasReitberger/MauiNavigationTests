using ShellNavTests.ViewModels.Modals;

namespace ShellNavTests.Views.Modals;

public partial class ViewItem2ModalPage : ContentPage
{
    public ViewItem2ModalPage(ViewItem2ModalPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        Loaded += ((ViewItem2ModalPageViewModel)BindingContext).Pages_Loaded;
    }
    ~ViewItem2ModalPage()
    {
        Loaded -= ((ViewItem2ModalPageViewModel)BindingContext).Pages_Loaded;
    }

    #region Methods
    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            ((ViewItem2ModalPageViewModel)BindingContext).OnAppearing();
        }
        catch (Exception) { }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        try
        {
            ((ViewItem2ModalPageViewModel)BindingContext).OnDisappearing();
        }
        catch (Exception) { }
    }

    // Disable Android Back Button
    protected override bool OnBackButtonPressed() => false;
    #endregion
}