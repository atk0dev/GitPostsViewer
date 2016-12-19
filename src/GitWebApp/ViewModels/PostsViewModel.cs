using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitWebApp.ViewModels
{
    public class PostsViewModel
    {
        public IEnumerable<GitWebApp.Models.Post> Posts { get; set; }
        public int Amount { get; set; }
    }
}
