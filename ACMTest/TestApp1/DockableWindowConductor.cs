using ActiproSoftware.Windows.Controls.Docking;
using Caliburn.Micro;

namespace TestApp1
{
  internal class DockableWindowConductor
  {
    #region Fields
    /// <summary>
    ///   The view.
    /// </summary>
    private readonly DockingWindow _view;

    private readonly DockSite _dockSite;
    /// <summary>
    ///   The view model.
    /// </summary>
    private readonly object _viewModel;

    /// <summary>
    ///   The flag used to identify the view as closing.
    /// </summary>
    private bool _isClosing;

    /// <summary>
    ///   The flag used to determine if the view requested deactivation.
    /// </summary>
    private bool _isDeactivatingFromView;

    /// <summary>
    ///   The flag used to determine if the view model requested deactivation.
    /// </summary>
    private bool _isDeactivatingFromViewModel;
    #endregion

    #region Constructors
    /// <summary>
    ///   Initializes a new instance of the <see cref = "DockableWindowConductor" /> class.
    /// </summary>
    /// <param name="dockSite"> </param>
    /// <param name = "viewModel">The view model.</param>
    /// <param name = "view">The view.</param>
    public DockableWindowConductor(DockSite dockSite, object viewModel, DockingWindow view)
    {
      _dockSite = dockSite;
      _viewModel = viewModel;
      _view = view;

      var activatable = viewModel as IActivate;
      if (activatable != null)
        activatable.Activate();

      var deactivatable = viewModel as IDeactivate;
      if (deactivatable != null)
      {
        _dockSite.WindowClosed += OnClosed;
        //_view.Closed += OnClosed;
        deactivatable.Deactivated += OnDeactivated;
      }

      var guard = viewModel as IGuardClose;
      if (guard != null)
        _dockSite.WindowClosing += OnClosing;
      //view.Closing += OnClosing;
    }
    #endregion

    #region Event Handlers
    /// <summary>
    ///   Called when the view has been closed.
    /// </summary>
    /// <param name = "sender">The sender.</param>
    /// <param name = "e">The <see cref = "DockingWindowEventArgs" /> instance containing the event data.</param>
    private void OnClosed(object sender, DockingWindowEventArgs e)
    {
      if (e.Window != _view)
        return; //  ignore if not our window

      _dockSite.WindowClosed -= OnClosed;
      if (_viewModel is IGuardClose)
        _dockSite.WindowClosing -= OnClosing;

      if (_isDeactivatingFromViewModel)
        return;

      var deactivatable = (IDeactivate)_viewModel;

      _isDeactivatingFromView = true;
      deactivatable.Deactivate(true);
      _isDeactivatingFromView = false;
    }
    /// <summary>
    ///  Called when the view is about to be closed.
    /// </summary>
    /// <param name = "sender">The sender.</param>
    /// <param name = "e">The <see cref = "DockingWindowEventArgs" /> instance containing the event data.</param>
    private void OnClosing(object sender, DockingWindowEventArgs e)
    {
      var guard = (IGuardClose)_viewModel;

      if (_isClosing)
      {
        _isClosing = false;
        return;
      }

      //bool runningAsync = false;
      bool shouldEnd = false;
      bool async = false;

      guard.CanClose(canClose => Execute.OnUIThread(() =>
                                                      {
                                                        if (async && canClose)
                                                        {
                                                          _isClosing = true;

                                                          _view.SafeClose();
                                                        }
                                                        else
                                                          e.Cancel = !canClose;

                                                        shouldEnd = true;
                                                      }));

      if (shouldEnd)
        return;

      //runningAsync = 
      e.Cancel = true;
    }
    /// <summary>
    ///   Called when the view has been deactivated.
    /// </summary>
    /// <param name = "sender">The sender.</param>
    /// <param name = "e">The <see cref = "Caliburn.Micro.DeactivationEventArgs" /> instance containing the event data.</param>
    private void OnDeactivated(object sender, DeactivationEventArgs e)
    {
      ((IDeactivate)_viewModel).Deactivated -= OnDeactivated;

      if (!e.WasClosed || _isDeactivatingFromView)
        return;

      _isDeactivatingFromViewModel = true;
      _isClosing = true;
      _view.SafeClose();//.ExecuteCommand(ContentPaneCommands.Close);
      _isClosing = false;
      _isDeactivatingFromViewModel = false;
    }
    #endregion
  }
}