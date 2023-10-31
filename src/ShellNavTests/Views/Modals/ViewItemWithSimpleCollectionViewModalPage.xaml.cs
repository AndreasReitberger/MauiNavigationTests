using ShellNavTests.ViewModels.Modals;

namespace ShellNavTests.Views.Modals;

public partial class ViewItemWithSimpleCollectionViewModalPage : ContentPage
{
    public ViewItemWithSimpleCollectionViewModalPage(ViewItemModalPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        Loaded += ((ViewItemModalPageViewModel)BindingContext).Pages_Loaded;
    }
    ~ViewItemWithSimpleCollectionViewModalPage()
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