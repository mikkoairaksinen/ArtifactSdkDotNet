using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ArtifactSdkDotNet.Core;
using Utils;

namespace ArtifactSdkDotNet.CardsetApi
{
    public static class CardsetApi
    {
        public static async Task<Cardset> GetCardset( string cardsetId )
        {
            if (!Config.CardSetApi.SetIds.All.Contains(cardsetId))
            {
                throw new ArgumentException("Unknown cardsetId: {cardsetId}");
            }
            var unixTimeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (!m_cache.ContainsKey(cardsetId) || m_cache[cardsetId].Item2 < unixTimeNow)
            {
                m_cache[cardsetId] = await DownloadCardset(cardsetId);
            }

            return m_cache[cardsetId].Item1;

        }

        private static async Task<(Cardset, long)> DownloadCardset( string cardsetId )
        {
            
            var baseResponse = await m_httpClient.GetAsync($"{Config.CardSetApi.BaseUrl}/{cardsetId}");

            baseResponse.EnsureSuccessStatusCode();

            CardsetApiResponse response = JsonUtils.DeserializeFromArtifactApi<CardsetApiResponse>(await baseResponse.Content.ReadAsStringAsync());

            var url = $"{response.CdnRoot}{WebUtility.UrlDecode(response.Url)}";
            var jsonResponse = await m_httpClient.GetAsync(url);

            string json = await jsonResponse.Content.ReadAsStringAsync();
            CardsetApiJsonResponse cardSetResponse = JsonUtils.DeserializeFromArtifactApi<CardsetApiJsonResponse>(json);
            
            return (cardSetResponse.Cardset, response.ExpireTime);
        }

        private static Dictionary<string, (Cardset, long)> m_cache = new Dictionary<string, (Cardset, long)>();
        private static HttpClient m_httpClient = new HttpClient();
    }
}