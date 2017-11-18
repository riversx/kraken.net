using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kraken.Net.Models
{
    public class OhlcResult
    {
        public List<OhlcData> OhlcHistory { get; set; }

        public int Last { get; set; }
    }

}
