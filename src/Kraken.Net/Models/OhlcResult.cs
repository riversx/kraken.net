﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kraken.Net.Models
{
    public class OhlcResult
    {
        public List<OhlcData> OhlcHistory { get; set; }
        public int Last { get; set; }
    }


    public class OhlcData
    {
        public string Pair { get; set; }
        public int Interval { get; set; }
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
