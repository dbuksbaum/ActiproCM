using System.Windows.Media;
using Caliburn.Micro;

namespace TestApp1
{
  public class DocumentViewModel : PropertyChangedBase, IHaveDisplayName, IDockingViewModel
  {
    private string _someDisplayData;
    public string SomeDisplayData
    {
      get { return _someDisplayData; }
      set { _someDisplayData = value; NotifyOfPropertyChange(() => SomeDisplayData); }
    }

    #region Implementation of IHaveDisplayName
    /// <summary>
    /// Gets or Sets the Display Name
    /// </summary>
    public string DisplayName { get; set; }
    #endregion

    #region Implementation of IDockingViewModel
    public string Name { get; set; }
    public string Title { get; set; }
    public ImageSource ImageSource { get; set; }
    #endregion
  }
}