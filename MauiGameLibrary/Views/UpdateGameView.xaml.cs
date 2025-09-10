using MauiGameLibrary.ViewModels;

namespace MauiGameLibrary.Views;

public partial class UpdateGameView : BasePage
{
	public UpdateGameView(UpdateGameViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

   
}