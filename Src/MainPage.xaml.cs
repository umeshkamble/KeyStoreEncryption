using KeyStoreEncryption.ViewModels;

namespace KeyStoreEncryption;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;
	}
}

