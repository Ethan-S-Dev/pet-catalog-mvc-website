using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        public CommentsController(ICommentService commentService,IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddComment(CommentViewModel comment,int id)
        {
            if(ModelState.IsValid)
            {
                comment.AnimalId = id;
                var realComment = mapper.Map<Comment>(comment);
                commentService.AddComment(realComment);
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
