

namespace KeyStoreEncryption.Helper;

public interface IEncryptDecryptHelper
{
#if ANDROID
    string GenaratePublicKey();
    string DecryptMessage(string AesKey, string AesMessage);
#endif
}
