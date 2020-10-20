using System;
using System.Security.Cryptography;

public static class CryptoServiceProvider
{
    public static string Create()
    {
        RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
        var byteArray = new byte[32];
        cryptoServiceProvider.GetBytes(byteArray);
        return Encode(byteArray);
    }

    /// <summary>
    /// Encodes the byte array.
    /// </summary>
    /// <param name="byteArray">argument</param>
    /// <returns></returns>
    public static string Encode(byte[] byteArray)
    {
        var base64 = Convert.ToBase64String(byteArray);
        return base64.Split('=')[0].Replace('+', '-').Replace('/', '_');
    }
}