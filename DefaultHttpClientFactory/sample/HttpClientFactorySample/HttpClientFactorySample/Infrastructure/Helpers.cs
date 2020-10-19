using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HttpClientFactorySample.Infrastructure
{
    public static class Helpers
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long ToTimestamp(this DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }

        public static string ComputeHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string ComputeMarvelAPIHash(string privateKey, string publicKey)
        {
            var ts = DateTime.Now.ToTimestamp();
            var hash = ComputeHashMd5($"{ts}{privateKey}{publicKey}");
            return hash;
        }
    }
}
