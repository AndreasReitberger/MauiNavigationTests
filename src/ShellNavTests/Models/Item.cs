using CommunityToolkit.Mvvm.ComponentModel;

namespace ShellNavTests.Models
{
    public partial class Item : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        string name;

        [ObservableProperty]
        DateTimeOffset start = DateTimeOffset.Now;

        [ObservableProperty]
        DateTimeOffset end = DateTimeOffset.Now.Add(TimeSpan.FromHours(2));
        #endregion
    }
}
