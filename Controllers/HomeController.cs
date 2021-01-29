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
        public IActionResult Index(PostModel Post)
        {
            List<PostModel> Posts = Post.GetPosts();

            if (HttpContext.Request.Cookies.ContainsKey("name"))
            {
                ViewBag.Hello = HttpContext.Request.Cookies["name"].ToString() + "!";
            }

            return View(Posts);
        }

        [HttpGet("/Skapa")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/Skapa")]
        public IActionResult Create(PostModel Post)
        {
            if (ModelState.IsValid)
            {
                Post.AddPost();
                ModelState.Clear();
                ViewBag.Message = "Inlägg skapat!";

                if (HttpContext.Request.Cookies.ContainsKey("name"))
                {
                    HttpContext.Response.Cookies.Delete("name");
                }

                HttpContext.Response.Cookies.Append("name", Post.Author);
            }
            return View();
        }

        [HttpGet("/{id}")]
        public IActionResult Details(int id, PostModel Post)
        {
            List<PostModel> Posts = Post.GetPosts();

            return View(Posts[id]);
        }

        [HttpPost("/{id}")]
        public IActionResult Delete(int id, PostModel Post)
        {
            Post.Id = id;
            Post.DeletePost();
            return RedirectToAction(nameof(Index));
        }
    }
}
