
namespace MauiGameLibrary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
           Routing.RegisterRoute("updategameroute", typeof(Views.UpdateGameView));
        }

     
    }
}
