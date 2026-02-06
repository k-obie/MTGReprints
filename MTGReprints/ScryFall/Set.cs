using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTGReprints.ScryFall
{
    public class Set
    {

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("total_cards")]
        public double TotalCards { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        [JsonPropertyName("next_page")]
        public string NextPage { get; set; }

        [JsonPropertyName("data")]
        public List<object> Data { get; set; }

    }
}
