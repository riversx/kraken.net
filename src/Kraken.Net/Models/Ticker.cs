using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kraken.Net.Models
{

    public class Ticker
    {
        /// <summary>
        /// pair_name
        /// ex: "XETHZEUR": { }
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// a = ask array(<price>, <whole lot volume>, <lot volume>),
        /// ex: "a": [ "312.73000", "4", "4.000" ],
        /// </summary>
        public PriceLotVolume Ask { get; set; }

        /// <summary>
        /// b = bid array(<price>, <whole lot volume>, <lot volume>),
        /// ex: "b": [ "312.21000", "14", "14.000" ],
        /// </summary>
        public PriceLotVolume Bid { get; set; }

        /// <summary>
        /// c = last trade closed array(<price>, <lot volume>),
        /// ex "c": [ "312.21000", "0.80000000" ],
        /// </summary>
        public PriceVolume LastClosedTrade { get; set; }

        /// <summary>
        /// v = volume array(<today>, <last 24 hours>),
        /// ex: "v": [ "13052.65894436", "33348.83362695" ],
        /// </summary>
        public TodayLast24h<decimal> Volume { get; set; }

        /// <summary>
        /// p = volume weighted average price array(<today>, <last 24 hours>),
        /// ex: "p": [ "311.48548", "310.74913" ],
        /// </summary>
        public TodayLast24h<double> VWAP { get; set; }

        /// <summary>
        /// t = number of trades array(<today>, <last 24 hours>),
        /// ex:  "t": [ 4105, 11549 ],
        /// </summary>
        public TodayLast24h<int> Tradesnumber { get; set; }

        /// <summary>
        /// l = low array(<today>, <last 24 hours>),
        /// ex: "l": [ "305.59000", "305.00000" ],
        /// </summary>
        public TodayLast24h<double> Low { get; set; }

        /// <summary>
        /// h = high array(<today>, <last 24 hours>),
        /// ex: "h": [ "313.11000", "313.11000" ],
        /// </summary>
        public TodayLast24h<double> High { get; set; }

        /// <summary>
        /// o = today's opening price
        /// ex:  "o": "306.01000"
        /// </summary>
        public double TodayOpeningPrice { get; set; }
    }

    public class PriceLotVolume
    {
        public double Price { get; set; }
        public double WholeLotVolume { get; set; }
        public double LotVolume { get; set; }
    }

    public class PriceVolume
    {
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
    }

    public class TodayLast24h<T> where T: struct
    {
        public T Today { get; set; }
        public T Last24h { get; set; }
    }

}
 