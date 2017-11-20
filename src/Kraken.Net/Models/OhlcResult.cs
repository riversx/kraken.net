using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kraken.Net.Models
{
    public class OhlcResult
    {
        public string Pair { get; set; }
        public List<OhlcData> OhlcHistory { get; set; }
        public int Last { get; set; }
    }


    public class OhlcData
    {
        public int Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal VWAP { get; set; }
        public decimal Volume { get; set; }
        public int Count { get; set; }
    }
}
