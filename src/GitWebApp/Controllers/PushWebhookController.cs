using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitWebApp.Config;
using GitWebApp.Models.Github;
using GitWebApp.Services;
using GitWebApp.Validation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GitWebApp.Controllers
{
    [Route("api/[controller]")]
    public class PushWebhookController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly GithubPushConfig _config;

        public PushWebhookController(IHostingEnvironment env, IOptions<GithubPushConfig> options)
        {
            this._environment = env;
            this._config = options.Value;
        }

        [HttpPost]
        public async Task Post()
        {
            var payload = await Request.GetGithubPayloadAsync(_config.SecretKey);
            if (payload == null)
            {
                Response.StatusCode = 400;
                return;
            }

            
            // process
            var commits = payload.Commits.OrderByDescending(c => c.Timestamp);
            var processor = new GithubPushProcessor(_environment.WebRootPath, _config);
            foreach (var commit in commits)
            {
                await processor.ProcessAcync(commit);
            }

        //    Ok();
        }

        //[HttpPost]
        //public void Post([FromBody]PushPayload payload)
        //{
        //}
    }
}
