using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitWebApp.Config;
using GitWebApp.Data;
using GitWebApp.Models.Github;

namespace GitWebApp.Services
{
    public class GithubPushProcessor
    {
        private readonly List<string> _procesed = new List<string>();

        private readonly FileStore _files;

        private readonly GithubPushConfig _config;

        public GithubPushProcessor(string wwwrootPath, GithubPushConfig config)
        {
            this._files = new FileStore(wwwrootPath);
            this._config = config;
        }

        public async Task ProcessAcync(Commit commit)
        {
            var filesToRetreive = commit.Added.Union(commit.Modified);
            string baseUrl = _config.BaseDownloadUrl;
            foreach (var file in filesToRetreive)
            {
                if (_procesed.Contains(file))
                {
                    continue;
                }

                var url = $@"{baseUrl}/{file}";
                var data = await GetFileAsync(url);
                await _files.CreateOrUpdateAsync(file, data);
                _procesed.Add(file);
            }

            foreach (var file in commit.Removed)
            {
                await _files.DeleteAsync(file);
            }
        }

        private async Task<string> GetFileAsync(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
