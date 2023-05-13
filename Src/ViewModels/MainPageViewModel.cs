using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KeyStoreEncryption.Helper;

namespace KeyStoreEncryption.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    #region Propertise
    private readonly IEncryptDecryptHelper encryptDecryptHelper;
    [ObservableProperty]
    string publicKey;
    [ObservableProperty]
    string aesKey;
    [ObservableProperty]
    string encrypedMessage;
    [ObservableProperty]
    string plainText;
    #endregion

    #region Constructor
    public MainPageViewModel(IEncryptDecryptHelper encryptDecryptHelper)
    {
        this.encryptDecryptHelper = encryptDecryptHelper;
    }
    #endregion

    #region Command Operations
    [RelayCommand]
    void GenaratePublicKey()
    {
        PublicKey = encryptDecryptHelper.GenaratePublicKey();
    }

    [RelayCommand]
    async void DecryptMessage()
    {
        try
        {
            PlainText = encryptDecryptHelper.DecryptMessage(AesKey, EncrypedMessage);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
    #endregion

}
