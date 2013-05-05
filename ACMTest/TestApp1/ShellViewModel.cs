using Caliburn.Micro;

namespace TestApp1 {
    using System.ComponentModel.Composition;

    [Export(typeof(IShell))]
    public sealed class ShellViewModel : Screen, IShell
    {
      #region Fields
      private IDockAwareWindowManager _windowManager;
      #endregion

      /// <summary>
      /// Creates an instance of the screen.
      /// </summary>
      public ShellViewModel()
      {
        DisplayName = "Actipro Caliburn Micro Test Case";
      }

      protected override void OnActivate()
      {
        base.OnActivate();

        var view = GetView() as ShellView;
        _windowManager = new DockAwareWindowManager(view.dockSite);
      }
      public void AddDocument()
      {
        _windowManager.ShowDocumentWindow(new DocumentViewModel
                                            {
                                              Title = "Document Window",
                                              DisplayName = "MyDocument.txt",
                                              SomeDisplayData = "Hello World from a Docked Document!"
                                            }, null, true);
      }
      public void AddTool()
      {
        _windowManager.ShowToolWindow(new DocumentViewModel
        {
          Title = "Tool Window",
          DisplayName = "MyToolWindow",
          SomeDisplayData = "Hello World from a Docked Tool!"
        }, null, true);
      }
    }
}