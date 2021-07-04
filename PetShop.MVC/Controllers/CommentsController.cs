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
            var url = Request.Headers["Referer"].ToString();
            return Redirect(url);
        }

        
        public IActionResult DeleteComment(int id)
        {
            var url = Request.Headers["Referer"].ToString();
            if (id ==0) return Redirect(url);
            commentService.DeleteComment(id);    
            return Redirect(url);
        }
    }
}
