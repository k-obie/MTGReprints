using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTGReprints.ScryFall
{
    public class SetDef
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("set_type")]
        public string SetType { get; set; }

        [JsonPropertyName("search_uri")]
        public string SearchUri { get; set; }

        [JsonPropertyName("released_at")]
        public string ReleasedAt { get; set; }

        [JsonPropertyName("scryfall_uri")]
        public string ScryfallUri { get; set; }

        [JsonPropertyName("icon_svg_uri")]
        public string IconSvgUri { get; set; }


    }
}
