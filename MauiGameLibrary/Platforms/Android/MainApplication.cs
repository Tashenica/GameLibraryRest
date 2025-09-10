using Android.App;
using Android.Runtime;

namespace MauiGameLibrary
{
    [Application(Theme = "@style/Maui.MainTheme")]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
