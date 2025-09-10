using MauiGameLibrary.ViewModels;

namespace MauiGameLibrary.Views;

public partial class ListOfGamesView : BasePage
{
	private ListOfGamesViewModel _viewModel;

	public ListOfGamesView(ListOfGamesViewModel vm)
	{
		InitializeComponent();

		_viewModel = vm;
		BindingContext = vm;
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		
		// Explicitly refresh the games list when the view appears
		_viewModel?.RefreshGames();
	}
}