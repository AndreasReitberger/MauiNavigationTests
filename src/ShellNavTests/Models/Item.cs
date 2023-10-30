using CommunityToolkit.Mvvm.ComponentModel;

namespace ShellNavTests.Models
{
    public partial class Item : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name;
        #endregion
    }
}
