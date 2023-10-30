using System.Collections.ObjectModel;

namespace ShellNavTests.Models
{
    /// <summary>
    /// Holds the data from the database, so that the queries are reduced and data only gets loaded if needed.
    /// </summary>
    public static class AppData
    {
        #region Properties
        public static DateTime? LastRefresh { get; set; }
        public static bool DataChanged { get; set; } = false;

        #endregion

        #region Collections
        public static ObservableCollection<Item> Items { get; set; } = new();

        #endregion
    }
}
