using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.MVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }


        [HttpPost]
        public IActionResult AddComment(CommentViewModel comment)
        {
            if(ModelState.IsValid)
            {
                commentService.AddComment(comment);
            }
            return RedirectToAction("index","home");
        }

        [HttpGet]
        public IActionResult DeleteComment(int id)
        {
            commentService.DeleteComment(id);
            
            return RedirectToAction("Index", "Admin");
        }
    }
}
