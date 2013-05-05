using ActiproSoftware.Windows.Controls.Docking;
using Caliburn.Micro;

namespace TestApp1
{
  public class DockAwareWindowManager : WindowManager, IDockAwareWindowManager
  {
    #region Fields
    private readonly DockSite _dockSite;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="DockAwareWindowManager"/> class.
    /// </summary>
    /// <param name="dockSite">The dock site or <see langword="null"/> to use the main window dock site.</param>
    public DockAwareWindowManager(DockSite dockSite = null)
    {
      _dockSite = dockSite;
    }
    #endregion

    #region Implementation of IDockAwareWindowManager
    /// <summary>
    ///   Shows a tool window.
    /// </summary>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "context">The context.</param>
    /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
    public void ShowToolWindow(object viewModel, object context, bool selectWhenShown = true)
    {
      var dockableWindow = _dockSite.CreateWindow(viewModel, false, context);

      /*
      var pane = XamDockManagerHelper.FindSplitPaneWithLocationOrCreate(GetDockingManager(_window), dockstate);
      pane.Panes.Add(dockableWindow);
      //If this is a new dockable location (there are no split panes for it)
      //we need to add it to the XamDockManager
      if (pane.Parent == null)
      {
        DockManager.Panes.Add(pane);
      }*/

      //  TODO: Add docking location list. Either dock to named dock host, possibly found by an interface named IHaveDockAffinity with a DockName property.
      //  TODO: or IHaveDockLocation with an Enum defining Left, Right, Top, Bottom

      if (selectWhenShown)
      {
        dockableWindow.Activate();
      }
    }
    /// <summary>
    ///   Shows a floating window.
    /// </summary>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "context">The context.</param>
    /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
    public void ShowFloatingWindow(object viewModel, object context, bool selectWhenShown = true)
    {
      var dockableWindow = _dockSite.CreateWindow(viewModel, true, context);

      /*var pane = new SplitPane();
      XamDockManager.SetInitialLocation(pane, InitialPaneLocation.DockableFloating);

      pane.Panes.Add(dockableWindow);
      DockManager.Panes.Add(pane);*/

      dockableWindow.Float();

      if (selectWhenShown)
      {
        dockableWindow.Activate();
      }
    }
    /// <summary>
    ///   Shows a document window.
    /// </summary>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "context">The context.</param>
    /// <param name="selectWhenShown">If set to <c>true</c> the window will be selected when shown.</param>
    public void ShowDocumentWindow(object viewModel, object context, bool selectWhenShown = true)
    {
      var dockableWindow = _dockSite.CreateWindow(viewModel, true, context);
      /*var host = XamDockManagerHelper.FindTabGroupPane(GetDockingManager());
      
      host.Items.Add(dockableWindow);*/
      if (selectWhenShown)
      {
        dockableWindow.Activate();
      }
    }
    #endregion
  }
}