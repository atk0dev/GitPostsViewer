using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitWebApp.Models;

namespace GitWebApp.Data
{
    public class PostRepository
    {
        private readonly FileStore _files;
        private readonly string _basePath;

        public PostRepository(string basePath)
        {
            _basePath = $@"{basePath}\posts";
            _files = new FileStore(_basePath);

        }

        public IEnumerable<Post> GetAll()
        {
            var files = _files.GetFiles("*.md");
            var posts = new List<Post>();
            
            foreach (var  file in files)
            {
                var content = ReadFile(file);
                var post = Post.Parse(content);
                if (post != null)
                {
                    posts.Add(post);
                }
            }

            return posts.ToArray();
        }

        public Post GetPost(string path)
        {
            var fileName = $@"{_basePath}\{path}.md";
            var content = ReadFile(fileName);

            if (content != null)
            {
                return Post.Parse(content);
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
    }
}
