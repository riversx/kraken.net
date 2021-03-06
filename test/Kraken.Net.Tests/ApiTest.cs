using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Kraken.Net.Models;
using Xunit;

namespace Kraken.Net.Tests
{
    public class ApiTest
    {
        private readonly Api _api;

        public ApiTest()
        {
            KrakenApiResponseHandler handler = new KrakenApiResponseHandler();

            _api = new Api(null, null, Api.Url, Api.Version, handler);

            handler.AddResponse(
                new Uri(String.Format("{0}/{1}/public/{2}", Api.Url, Api.Version, "Assets")),
                String.Empty,
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(File.ReadAllText("Responses/Assets.json"))
                }
            );

            handler.AddResponse(
                new Uri(String.Format("{0}/{1}/public/{2}", Api.Url, Api.Version, "Time")),
                String.Empty,
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(File.ReadAllText("Responses/ServerTime.json"))
                }
            );

            handler.AddResponse(
                new Uri(String.Format("{0}/{1}/public/{2}", Api.Url, Api.Version, "OHLC")),
                String.Format("pair={0}&interval={1}&since={2}", "ETHEUR", "5", "1511034000"),
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(File.ReadAllText("Responses/OHLC.json"))
                }
            );

            handler.AddResponse(
                new Uri(String.Format("{0}/{1}/public/{2}", Api.Url, Api.Version, "Ticker")),
                String.Format("pair={0}", "XBTEUR,ETHEUR"),
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(File.ReadAllText("Responses/Ticker.json"))
                }
            );
        }

        [Fact]
        public void TestGetAssets()
        {
            var assets = _api.GetAssets(null);

            Assert.True(assets.Count > 0);

            Assert.Contains(assets, a => a.Name.Equals("EUR"));
        }

        [Fact]
        public void TestGetAssetsCache()
        {
            var assets1 = _api.GetAssets(null);
            var assets2 = _api.GetAssets(null);

            Assert.Same(assets1, assets2);
        }

        //[Fact]
        // TODO: Currently not enabled because there are no json response for asset pairs
        public void TestGetAssetPairsCache()
        {
            var assetPairs1 = _api.GetAssetPairs();
            var assetPairs2 = _api.GetAssetPairs();

            Assert.Same(assetPairs1, assetPairs2);
        }

        [Fact]
        public void TestGetServerTime()
        {
            var time = _api.GetServerTime();
            var utc = time.ToUniversalTime();

            Assert.Equal(12, utc.Day);
            Assert.Equal(3, utc.Month);
            Assert.Equal(2017, utc.Year);
            Assert.Equal(14, utc.Hour);
            Assert.Equal(48, utc.Minute);
            Assert.Equal(43, utc.Second);
        }


        [Fact]
        public void TestGetTicker()
        {
            var pairs = new List<string>() { "XBTEUR", "ETHEUR" };

            var tickers = _api.GetTicker(pairs);

            Assert.NotNull(tickers);
            Assert.Equal(2, tickers.Count);
            Assert.Contains(tickers, t => t.Name == "XXBTZEUR");
            Assert.Contains(tickers, t => t.Name == "XETHZEUR");
            var ticker = tickers.FirstOrDefault(t => t.Name == "XETHZEUR");
            Assert.NotNull(ticker);
            Assert.Equal(312.73000m, ticker.Ask.Price);
            Assert.Equal(312.21000m, ticker.Bid.Price);
            Assert.Equal(312.21000m, ticker.LastClosedTrade.Price);
            Assert.Equal(13052.65894436m, ticker.Volume.Today);
            Assert.Equal(311.48548m, ticker.VWAP.Today);
            Assert.Equal(4105, ticker.TradesNumber.Today);
            Assert.Equal(305.59000m, ticker.Low.Today);
            Assert.Equal(313.11000m, ticker.High.Today);
            Assert.Equal(306.01000m, ticker.TodayOpeningPrice);
        }


        [Fact]
        public void TestGetOhlc()
        {
            const string pair = "ETHEUR";
            const int interval = 5;

            var ohlcResult = _api.GetOhlc(pair, interval, 1511034000);

            Assert.Equal(1511038200, ohlcResult.Last);
            Assert.Equal("XETHZEUR", ohlcResult.Pair);
            Assert.Equal(15, ohlcResult.OhlcHistory.Count);
            var ohlcData = ohlcResult.OhlcHistory[0];
            Assert.Equal(1511034300, ohlcData.Time);
            Assert.Equal(285.00m, ohlcData.Open);
            Assert.Equal(285.00m, ohlcData.High);
            Assert.Equal(284.80m, ohlcData.Low);
            Assert.Equal(284.96m, ohlcData.Close);
            Assert.Equal(284.96m, ohlcData.VWAP);
            Assert.Equal(136.98078487m, ohlcData.Volume);
            Assert.Equal(48, ohlcData.Count);
        }


        [Fact]
        public void TestGeneralError()
        {
            IList<Error> errors = new List<Error>();

            try
            {
                var assets = _api.QueryPublicAsync("Tim", null).Result;
            }
            catch (AggregateException ex)
            {
                Assert.IsType<KrakenException>(ex.InnerException);
                errors = ((KrakenException)ex.InnerException).Errors;
            }

            Assert.Single(errors);
            Error error = errors.FirstOrDefault();
            Assert.NotNull(error);
            Assert.Equal("General", error.Category);
            Assert.Equal(Error.Severity.Error, error.SeverityCode);
            Assert.Equal("Unknown method", error.ErrorType);
            Assert.Null(error.ExtraInfo);
        }
    }
}