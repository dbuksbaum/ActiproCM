using Caliburn.Micro;

namespace TestApp1
{
  public interface IDockAwareWindowManager : IWindowManager
  {
    /// <summary>
    ///   Shows a tool window.
    /// </summary>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "context">The context.</param>
    /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
    /// <param name = "dockSide">The dock side (Left, Right, Top, Bottom).</param>
    void ShowToolWindow(object viewModel, object context, bool selectWhenShown = true);
    //DockSide dockSide = DockSide.Left);

    /// <summary>
    ///   Shows a floating window.
    /// </summary>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "context">The context.</param>
    /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
    void ShowFloatingWindow(object viewModel, object context, bool selectWhenShown = true);

    /// <summary>
    ///   Shows a document window.
    /// </summary>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "context">The context.</param>
    /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
    void ShowDocumentWindow(object viewModel, object context, bool selectWhenShown = true);
  }
}