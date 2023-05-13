using CommunityToolkit.Maui;
using KeyStoreEncryption.Helper;
using KeyStoreEncryption.ViewModels;
using Microsoft.Extensions.Logging;

namespace KeyStoreEncryption;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
#if ANDROID
		builder.Services.AddSingleton<IEncryptDecryptHelper, EncryptDecryptHelper>();
#endif
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
