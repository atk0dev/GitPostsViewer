using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitWebApp.Models.Github
{
    public class PushPayload
    {
        [JsonProperty("commits")]
        public IList<Commit> Commits { get; set; }
    }

    public class Commit
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("added")]
        public IList<string> Added { get; set; }

        [JsonProperty("modified")]
        public IList<string> Modified { get; set; }

        [JsonProperty("removed")]
        public IList<string> Removed { get; set; }
    }
}
