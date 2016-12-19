using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitWebApp.Models.Github;
using GitWebApp.Validation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GitWebApp
{
    public static  class RequestExtentions
    {
        public static async Task<PushPayload> GetGithubPayloadAsync(this HttpRequest request, string secret)
        {
            var body = await request.Body.ReadToEndAsync();
            var payload = JsonConvert.DeserializeObject<PushPayload>(body);
            var header = request.Headers["X-Hub-Signature"];
            if (!GithubValidate.Validate(header, secret, body))
            {
                return null;
            }

            return payload;
        }
    }
}
