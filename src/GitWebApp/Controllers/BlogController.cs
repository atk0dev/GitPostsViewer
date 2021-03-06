﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitWebApp.Data;
using GitWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GitWebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly PostRepository _posts;
        private readonly ILogger _logger;

        public BlogController(IHostingEnvironment environment, ILogger<BlogController> logger)
        {
            _environment = environment;
            _logger = logger;
            _posts = new PostRepository(_environment.WebRootPath);
            _posts.SetLogger(_logger);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Getting all blog posts...");
                var posts = _posts.GetAll().OrderByDescending(p => p.PublishDate);
                var vm = new PostsViewModel
                {
                    Posts = posts.ToArray(),
                    Amount = posts.Count()
                };

                return View(vm);
            }
            catch (Exception ex)
            {
               _logger.LogError($@"Error while getting all blog posts. Message: {ex.Message}");
                throw;
            }
            
        }

        [Route("[controller]/{year}/{month}/{day}/{title}")]
        public IActionResult SinglePost(int year, int month, int day, string title)
        {
            var path = $@"{year}\{year}-{month:00}-{day:00}-{title}";
            var post = _posts.GetPost(path);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
