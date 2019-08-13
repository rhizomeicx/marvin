using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.bots
{
    static class PriceFrom
    {
        private static HttpClient _client = new HttpClient();

        private static string Get(string uri)
        {
            var response = _client.GetAsync(uri).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static long Binance(string ticker)
        {
            try
            {
                var json = JsonConvert.DeserializeObject(Get("https://api.binance.com/api/v1/ticker/24hr?symbol=" + ticker));
                var jObject = (JObject)json;
                var price = (jObject["lastPrice"].Value<double>());
                return Convert.ToInt64(1 / price * Math.Pow(10, 18));
            }
            catch
            {
                return 0;
            }
        }

        public static long Coingecko(string ticker)
        {
            try
            {
                var json = JsonConvert.DeserializeObject(Get("https://api.coingecko.com/api/v3/coins/" + ticker + "?localization=false"));
                var jObject = (JObject)json;
                var price = (jObject["market_data"]["current_price"]["usd"].Value<double>());
                return Convert.ToInt64(1 / price * Math.Pow(10, 18));
            }
            catch
            {
                return 0;
            }

        }

        public static long CoinMarketCap(string ticker)
        {
            try
            {
                var json = JArray.Parse(Get("https://api.coinmarketcap.com/v1/ticker/" + ticker));
                var price = json.First["price_usd"].Value<double>();
                return Convert.ToInt64(1 / price * Math.Pow(10, 18));
            }
            catch
            {
                return 0;
            }
        }

        public static long Velic()
        {
            try
            {
                var json = JArray.Parse(Get("https://api.velic.io/api/v1/public/ticker/"));
                var jObj = json.Where(b => (string)b["base_coin"] == "USDT" && (string)b["match_coin"] == "ICX").ToList();
                var price = jObj.Select(p => p["recent_price"].Value<double>()).First();
                return Convert.ToInt64(1 / price * Math.Pow(10, 18));
            }
            catch
            {
                return 0;
            }

        } 
       
    }
}
