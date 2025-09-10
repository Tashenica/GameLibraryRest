using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiGameLibrary.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnAppearing()
        {
            // This method can be overridden in derived classes to handle when the view appears
        }

    }
}
