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
    public class ApiErrorsTest
    {
        private readonly Api _api;

        public ApiErrorsTest()
        {
            KrakenApiResponseHandler handler = new KrakenApiResponseHandler();

            _api = new Api(null, null, Api.Url, Api.Version, handler);

            handler.AddResponse(
                new Uri(String.Format("{0}/{1}/public/{2}", Api.Url, Api.Version, "OHLC")),
                String.Format("pair={0}&interval={1}&since={2}", "ETHEURO", "5", "1511034000"),
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(File.ReadAllText("Responses/Errors/UnknownAssetError.json"))
                }
            );

        }

        [Fact(Skip = "Unimplemented error catch")]
        public void TestGetOhlcWithWrongPair()
        {
            const string pair = "ETHEURO";
            const int interval = 5;

            var ohlcResult = _api.GetOhlc(pair, interval, 1511034000);

            Assert.Equal(1511038200, ohlcResult.Last);
        }

    }
}