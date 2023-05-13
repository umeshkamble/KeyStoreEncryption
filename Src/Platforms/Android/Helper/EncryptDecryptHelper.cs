using Android.Security.Keystore;
using Android.Util;
using Java.Security;
using Javax.Crypto.Spec;
using Javax.Crypto;
using System.Text;

namespace KeyStoreEncryption.Helper;

internal partial class EncryptDecryptHelper : IEncryptDecryptHelper
{
    private KeyStore androidKeyStore;
    string keyStoreName = "AndroidKeyStore";
    string keyName = "MAUIAndroid";
    int keySize { get; set; } = 2048;

    public EncryptDecryptHelper()
    {
        LoadKeyStore();
    }
    public string GenaratePublicKey()
    {
        var _publicKey = GetPublicKey();
        return Base64.EncodeToString(_publicKey.GetEncoded(), Base64Flags.NoWrap);
    }
    public string DecryptMessage(string AesKey, string AesMessage)
    {
        var aeskey = DecryptAESKey(Convert.FromBase64String(AesKey));
        return DecryptAESMessage(AesMessage, aeskey);
    }
    private void LoadKeyStore()
    {
        try
        {
            androidKeyStore = KeyStore.GetInstance(keyStoreName);
            androidKeyStore.Load(null);
            if (!KeysExist())
            {
                KeyPairGenerator keyGenerator =
                    KeyPairGenerator.GetInstance(KeyProperties.KeyAlgorithmRsa, keyStoreName);
                var builder = new KeyGenParameterSpec.Builder(keyName, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                            .SetBlockModes(KeyProperties.BlockModeEcb)
                            .SetEncryptionPaddings(KeyProperties.EncryptionPaddingRsaPkcs1)
                            .SetRandomizedEncryptionRequired(false).SetKeySize(keySize);

                keyGenerator.Initialize(builder.Build());
                keyGenerator.GenerateKeyPair();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.StackTrace);
        }
    }
    private bool KeysExist() => androidKeyStore.ContainsAlias(keyName);
    private IKey GetPrivateKey()
    {
        if (!androidKeyStore.ContainsAlias(keyName))
            return null;
        return androidKeyStore.GetKey(keyName, null);
    }

    private IKey GetPublicKey()
    {
        if (!androidKeyStore.ContainsAlias(keyName))
            return null;
        return androidKeyStore.GetCertificate(keyName)?.PublicKey;
    }

    private byte[] DecryptAESKey(byte[] encyrptedData)
    {
        try
        {
            var privateKey = GetPrivateKey();
            var transformation = "RSA/ECB/PKCS1Padding";
            var cipher = Cipher.GetInstance(transformation);
            cipher.Init(CipherMode.DecryptMode, privateKey);
            var decryptedBytes = cipher.DoFinal(encyrptedData);
            return decryptedBytes;
        }
        catch (Exception)
        {
            throw new Exception("Invalid AES key, Please enter valid key");
        }
    }

    private string DecryptAESMessage(string message, byte[] secretKeyBytes)
    {
        try
        {
            ISecretKey secretKey = new SecretKeySpec(secretKeyBytes, 0, secretKeyBytes.Length, "AES");
            Cipher cipher = Cipher.GetInstance("AES/ECB/PKCS5Padding");
            cipher.Init(CipherMode.DecryptMode, secretKey);
            byte[] messageBytes = cipher.DoFinal(Convert.FromBase64String(message));
            return Encoding.UTF8.GetString(messageBytes);
        }
        catch (Exception)
        {
            throw new Exception("Enable to decrypt your message");
        }
    }
}
