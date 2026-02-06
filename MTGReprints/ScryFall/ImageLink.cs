using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTGReprints.ScryFall
{
    public class ImageLink
    {
        [JsonPropertyName("large")]
        public string Large { get; set; }

    }
}
