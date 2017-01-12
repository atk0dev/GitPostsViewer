using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitWebApp.Models;
using Microsoft.Extensions.Logging;

namespace GitWebApp.Data
{
    public class PostRepository
    {
        private readonly FileStore _files;
        private readonly string _basePath;
        private ILogger _logger; 

        public PostRepository(string basePath)
        {
            _basePath = $@"{basePath}\posts";
            _files = new FileStore(_basePath);

        }

        public IEnumerable<Post> GetAll()
        {
            var files = _files.GetFiles("*.md");
            LogInfo($@"Found {files?.Count()} *.md files.");
            var posts = new List<Post>();
            
            foreach (var  file in files)
            {
                string msg;
                LogInfo($@"Processing file: {file}");
                var content = ReadFile(file);
                var post = Post.Parse(content, out msg);
                if (post != null)
                {
                    LogInfo($@"File {file} has been parsed successfully with message: {msg}");
                    posts.Add(post);
                }
                else
                {
                    LogInfo($@"Error. File {file} has not been parsed successfully. Message: {msg}");
                }
            }

            return posts.ToArray();
        }

        public Post GetPost(string path)
        {
            string msg = string.Empty;
            var fileName = $@"{_basePath}\{path}.md";
            var content = ReadFile(fileName);

            if (content != null)
            {
                return Post.Parse(content, out msg);
            }

            return null;
        }

        private string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        private void LogInfo(string message)
        {
            _logger?.LogInformation(message);
        }
    }
}
