using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using HeyRed.MarkdownSharp;

namespace GitWebApp.Models
{
    public class Post
    {
        public string Title { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Content { get; private set; }

        public static Post Parse(string fileData, out string msg)
        {
            msg = string.Empty;
            var lines = fileData.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            var metadata = new Dictionary<string, string>();

            if (lines.Length == 0)
            {
                msg = "There are no lines in file.";
                return null;
            }

            if (!lines[0].Contains("---"))
            {
                msg = $@"First line is not equal to --- (it is {lines[0]}. All: {lines.ToArray()})";
                return null;
            }

            var ii = 1;
            for (; ii < lines.Length; ii++)
            {
                if (lines[ii].Substring(0, 3) == "---")
                {
                    break;
                }

                var parts = lines[ii].Split(':');
                metadata.Add(parts[0].Trim(), parts[1].Trim());


            }

            var content = string.Join(Environment.NewLine, lines.Skip(ii + 1).ToArray());

            var md = new Markdown();

            return new Post
            {
                Title = metadata["title"],
                PublishDate = DateTime.Parse(metadata["date"]),
                Content = md.Transform(content)
            };
        }

        public static Post Build(string title, string content)
        {
            return new Post
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.Now
            };
        }
    }
}
