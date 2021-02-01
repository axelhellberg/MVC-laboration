using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_laboration.Models;

namespace MVC_laboration.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("name"))
            {
                ViewBag.Hello = "Välkommen, " + HttpContext.Request.Cookies["name"] + "!";
            }
            else
            {
                ViewBag.Hello = "Välkommen!";
            }

            PostModel Post = new PostModel();

            List<Post> Posts = Post.GetPosts();

            return View(Posts);
        }

        [HttpGet("/Skapa")]
        public IActionResult Create()
        {
            if (HttpContext.Request.Cookies.ContainsKey("name"))
            {
                ViewBag.Author = HttpContext.Request.Cookies["name"].ToString();
            }

            return View();
        }

        [HttpPost("/Skapa")]
        public IActionResult Create(Post NewPost)
        {
            PostModel Post = new PostModel();

            if (ModelState.IsValid)
            {
                if (String.IsNullOrWhiteSpace(NewPost.Author)) NewPost.Author = "Anonym";

                Post.AddPost(NewPost);
                ModelState.Clear();
                ViewData["Message"] = "Inlägg skapat!";

                if (NewPost.RememberMe)
                {
                    if (HttpContext.Request.Cookies.ContainsKey("name"))
                    {
                        HttpContext.Response.Cookies.Delete("name");
                    }

                    HttpContext.Response.Cookies.Append("name", NewPost.Author);
                }
            }
            return View();
        }

        [HttpGet("/{id}")]
        public IActionResult Details(int Id)
        {
            PostModel Post = new PostModel();

            List<Post> Posts = Post.GetPosts();

            return View(Posts[Id]);
        }

        [HttpPost("/{id}")]
        public IActionResult Delete(int Id)
        {
            PostModel Post = new PostModel();

            List<Post> Posts = Post.GetPosts();

            Post.DeletePost(Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
