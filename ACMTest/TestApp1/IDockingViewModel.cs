using System.Windows.Media;

namespace TestApp1
{
  public interface IDockingViewModel
  {
    /// <summary>
    /// The XAML Control Name. Same Rules Apply!!!
    /// </summary>
    string Name { get; set; }
    string Title { get; set; }
    ImageSource ImageSource { get; set; }
  }
}