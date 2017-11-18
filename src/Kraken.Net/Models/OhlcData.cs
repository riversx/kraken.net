using Newtonsoft.Json;
using System;

namespace Kraken.Net.Models
{
    public class OhlcData
    {
        public string Pair { get; internal set; }
        public int Time { get; set; }
        public DateTime DateTime { get { return new DateTime(1970, 1, 1).AddSeconds(Time); } }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal VWAP { get; set; }
        public decimal Volume { get; set; }
        public int Count { get; set; }
    }
}