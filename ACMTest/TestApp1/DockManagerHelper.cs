using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Media;
using Caliburn.Micro;

namespace TestApp1
{
  public static class DockManagerHelper
  {
    #region Fields
    private static readonly ILog Log = LogManager.GetLog(typeof(DockManagerHelper));
    #endregion

    public static DockSite FindDock()
    { //  Modified to use Actipro's helper methods, since I need to be bound to Actipro for
      //  the DockSite I might as well use their helper methods :)
      //  TODO: Fix This
      var rootVisual = VisualTreeHelperExtended.GetRoot(Application.Current.MainWindow);
      return VisualTreeHelperExtended.GetAllDescendants(rootVisual, typeof(DockSite)).FirstOrDefault() as DockSite;
    }
    public static DockingWindow FindWindow(this DockSite dockSite, object viewModel)
    {
      return dockSite.DocumentWindows.OfType<DockingWindow>().Concat(dockSite.ToolWindows).FirstOrDefault(w => w.DataContext == viewModel);
    }
    public static DockingWindow EnsureWindow(this DockSite dockSite, object viewModel, object view, bool isDocumentWindow = true)
    {
      var window = view as DockingWindow;

      if (window == null)
      {
        var dockingViewModel = viewModel as IDockingViewModel;

        if (isDocumentWindow)
        { //  assume a document window
          Log.Info("Create DocumentWindow");
          window = (dockingViewModel != null) ?
                     new DocumentWindow(dockSite, dockingViewModel.Name, dockingViewModel.Title, dockingViewModel.ImageSource, view) { DataContext = viewModel } :
                     new DocumentWindow(dockSite) { DataContext = viewModel, Content = view };
        }
        else
        { //  we have a tool window
          Log.Info("Creating ToolWindow");
          window = (dockingViewModel != null) ?
                     new ToolWindow(dockSite, dockingViewModel.Name, dockingViewModel.Title, dockingViewModel.ImageSource, view) { DataContext = viewModel } :
                     new ToolWindow(dockSite) { DataContext = viewModel, Content = view };
        }

        window.SetValue(View.IsGeneratedProperty, true);
      }

      return window;
    }
    public static DockingWindow CreateWindow(this DockSite dockSite, object viewModel, bool isDocumentWindow = true, object context = null)
    {
      var view = dockSite.EnsureWindow(viewModel, ViewLocator.LocateForModel(viewModel, null, context), isDocumentWindow);
      ViewModelBinder.Bind(viewModel, view, context);

      var haveDisplayName = viewModel as IHaveDisplayName;
      if (haveDisplayName != null && !ConventionManager.HasBinding(view, HeaderedContentControl.HeaderProperty))
      {
        var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
        view.SetBinding(HeaderedContentControl.HeaderProperty, binding);
      }

      new DockableWindowConductor(dockSite, viewModel, view);
      return view;
    }

    public static void SafeClose(this DockingWindow dockingWindow)
    {
      Log.Info("SafeClose");
      if (dockingWindow != null)
        dockingWindow.Close();
    }
  }
}