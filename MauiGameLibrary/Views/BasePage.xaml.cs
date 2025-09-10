using MauiGameLibrary.ViewModels;

namespace MauiGameLibrary.Views;

public partial class BasePage : ContentPage
{
	public BasePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BaseViewModel vm = BindingContext as BaseViewModel;

        if (vm != null)
        {
            vm.OnAppearing();
        }

    }
}