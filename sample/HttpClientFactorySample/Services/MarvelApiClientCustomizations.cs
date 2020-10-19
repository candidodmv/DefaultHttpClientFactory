using HttpClientFactorySample.Helpers;
using HttpClientFactorySample.Infrastructure;
using System;
using System.Collections.Specialized;
using System.Web;

namespace HttpClientFactorySample.Services
{
    public partial class MarvelApiClient
    {
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            var ts = DateTime.UtcNow.ToTimestamp().ToString("#");
            var apiKey = Secrets.marvelApiPublicKey;
            var hash = Infrastructure.Helpers.ComputeMarvelAPIHash(ts, Secrets.marvelApiPrivateKey, Secrets.marvelApiPublicKey);

            // does it really necessary to make a copy ?
            //var newOne = new char[urlBuilder.Length];
            //urlBuilder.CopyTo(0, newOne, 0, urlBuilder.Length);
            //var newOne2 = new string(newOne);

            var builder = new UriBuilder(urlBuilder.ToString());
            var query = HttpUtility.ParseQueryString(builder.Query ?? string.Empty);

            query.Add("ts", ts);
            query.Add("apikey", apiKey);
            query.Add("hash", hash);

            ////urlBuilder.Append(System.Uri.EscapeDataString("ts") + "=").Append(System.Uri.EscapeDataString(ConvertToString(ts, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            ////urlBuilder.Append(System.Uri.EscapeDataString("apiKey") + "=").Append(System.Uri.EscapeDataString(ConvertToString(apiKey, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            ////urlBuilder.Append(System.Uri.EscapeDataString("hash") + "=").Append(System.Uri.EscapeDataString(ConvertToString(hash, System.Globalization.CultureInfo.InvariantCulture))).Append("&");

            builder.Query = query.ToString();
            urlBuilder.Clear().Append(builder.ToString());
        }
    }
}
