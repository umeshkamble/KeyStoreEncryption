using Microsoft.Maui.Platform;

namespace KeyStoreEncryption;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        ModifyEntry();
        ModifyEditor();
        MainPage = new AppShell();
    }
    private void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("BoderlessEntry", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });
    }
    private void ModifyEditor()
    {
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("BoderlessEditor", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });
    }
}
