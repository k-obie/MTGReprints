using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTGReprints.ScryFall
{
    public class Card
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mana_cost")]
        public string ManaCost { get; set; }

        [JsonPropertyName("colors")]
        public List<string> Colors { get; set; }

        [JsonPropertyName("released_at")]
        public string ReleasedAt { get; set; }

        [JsonPropertyName("reprint")]
        public bool Reprint { get; set; }


        [JsonPropertyName("image_uris")]
        public ImageLink ImageUris { get; set; }
    }
}
