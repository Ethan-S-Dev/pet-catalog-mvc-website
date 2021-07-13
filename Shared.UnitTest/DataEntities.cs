using Microsoft.AspNetCore.Http;
using Moq;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.UnitTest
{
    public class DataEntities
    {
        private List<Category> categories;
        private List<Animal> animals;
        private List<Comment> comments;
        private List<Image> images;

        private List<CategoryViewModel> categoryViewModels;
        private List<AnimalViewModel> animalViewModels;
        private List<CommentViewModel> commentViewModels;

        public DataEntities()
        {
            var formFile = new Mock<IFormFile>();
            formFile.Setup(e => e.CopyTo(It.IsAny<Stream>()))
                    .Callback<Stream>((s) =>
                    {
                        var bytes = new byte[] { 1, 2, 3, 4, 5 };

                        using var meme = new MemoryStream(bytes);
                        meme.CopyTo(s);
                    });
            formFile.Setup(e => e.FileName)
                    .Returns("image");

            comments = new List<Comment>()
            {
                new Comment()
                {
                    CommentId = 1,
                    AnimalId = 1,
                    Value = "Animal 1 Comment 1"
                },
                new Comment()
                {
                    CommentId = 2,
                    AnimalId = 1,
                    Value = "Animal 1 Comment 2"
                },
                new Comment()
                {
                    CommentId = 3,
                    AnimalId = 1,
                    Value = "Animal 1 Comment 3"
                },
                new Comment()
                {
                    CommentId = 4,
                    AnimalId = 2,
                    Value = "Animal 2 Comment 1"
                },
                new Comment()
                {
                    CommentId = 5,
                    AnimalId = 2,
                    Value = "Animal 2 Comment 2"
                },
                new Comment()
                {
                    CommentId = 6,
                    AnimalId = 2,
                    Value = "Animal 2 Comment 3"
                },
                new Comment()
                {
                    CommentId = 7,
                    AnimalId = 3,
                    Value = "Animal 3 Comment 1"
                },
                new Comment()
                {
                    CommentId = 8,
                    AnimalId = 3,
                    Value = "Animal 3 Comment 2"
                },
                new Comment()
                {
                    CommentId = 9,
                    AnimalId = 3,
                    Value = "Animal 3 Comment 3"
                },
                new Comment()
                {
                    CommentId = 10,
                    AnimalId = 4,
                    Value = "Animal 4 Comment 1"
                }
            };

            images = new List<Image>()
            {
                new Image()
                {
                    ImageId = 1,
                    Name ="image",
                    Data = new byte[] { 1, 2, 3, 4, 5 }
                },
                new Image()
                {
                    ImageId = 2,
                    Name ="image",
                    Data = new byte[] { 1, 2, 3, 4, 5 }
                },
                new Image()
                {
                    ImageId = 3,
                    Name ="image",
                    Data = new byte[] { 1, 2, 3, 4, 5 }
                },
                new Image()
                {
                    ImageId = 4,
                    Name ="image",
                    Data = new byte[] { 1, 2, 3, 4, 5 }
                },
            };

            animals = new List<Animal>()
            {
                new Animal()
                {
                    AnimalId = 1,
                    CategoryId = 1,
                    Age = 5,
                    Name = "animal1",
                    Description = "this is animal 1",
                    ImageId = 1,
                    Image = images.Find(im=>im.ImageId == 1),
                    Comments = comments.Where(co=>co.AnimalId == 1).ToList()
                },
                new Animal()
                {
                    AnimalId = 2,
                     CategoryId = 1,
                     Age = 7,
                    Name = "animal2",
                    Description = "this is animal 2",
                    ImageId = 2,
                    Image = images.Find(im=>im.ImageId == 2),
                    Comments = comments.Where(co=>co.AnimalId == 2).ToList()
                },
                new Animal()
                {
                    AnimalId = 3,
                    CategoryId = 2,
                    Age = 23,
                    Name = "animal3",
                    Description = "this is animal 3",
                    ImageId = 3,
                    Image = images.Find(im=>im.ImageId == 3),
                    Comments = comments.Where(co=>co.AnimalId == 3).ToList()
                },
                new Animal()
                {
                    AnimalId = 4,
                    CategoryId = 2,
                    Age = 533,
                    Name = "animal4",
                    Description = "this is animal 4",
                    ImageId = 4,
                    Image = images.Find(im=>im.ImageId == 4),
                    Comments = comments.Where(co=>co.AnimalId == 4).ToList()
                }
            };

            categories = new List<Category>()
            {
                new Category()
                {
                    CategoryId = 1,
                    Name = "cate1",
                    Animals = animals.Where(ani=>ani.CategoryId == 1).ToList()
                },
                new Category()
                {
                    CategoryId = 2,
                    Name = "cate2",
                    Animals = animals.Where(ani=>ani.CategoryId == 2).ToList()
                }
            };

            comments.ForEach(co => co.Animal = animals.Find(an => an.AnimalId == co.AnimalId));
            animals.ForEach(ani => ani.Category = categories.Find(cat => cat.CategoryId == ani.CategoryId));


            commentViewModels = new List<CommentViewModel>()
            {
                new CommentViewModel()
                {
                    CommentId = 1,
                    AnimalId = 1,
                    Value = "Animal 1 Comment 1"
                },
                new CommentViewModel()
                {
                    CommentId = 2,
                    AnimalId = 1,
                    Value = "Animal 1 Comment 2"
                },
                new CommentViewModel()
                {
                    CommentId = 3,
                    AnimalId = 1,
                    Value = "Animal 1 Comment 3"
                },
                new CommentViewModel()
                {
                    CommentId = 4,
                    AnimalId = 2,
                    Value = "Animal 2 Comment 1"
                },
                new CommentViewModel()
                {
                    CommentId = 5,
                    AnimalId = 2,
                    Value = "Animal 2 Comment 2"
                },
                new CommentViewModel()
                {
                    CommentId = 6,
                    AnimalId = 2,
                    Value = "Animal 2 Comment 3"
                },
                new CommentViewModel()
                {
                    CommentId = 7,
                    AnimalId = 3,
                    Value = "Animal 3 Comment 1"
                },
                new CommentViewModel()
                {
                    CommentId = 8,
                    AnimalId = 3,
                    Value = "Animal 3 Comment 2"
                },
                new CommentViewModel()
                {
                    CommentId = 9,
                    AnimalId = 3,
                    Value = "Animal 3 Comment 3"
                },
                new CommentViewModel()
                {
                    CommentId = 10,
                    AnimalId = 4,
                    Value = "Animal 4 Comment 1"
                }
            };

            animalViewModels = new List<AnimalViewModel>()
            {
                new AnimalViewModel()
                {
                    AnimalId = 1,
                    CategoryId = 1,
                    Age = 5,
                    Name = "animal1",
                    Description = "this is animal 1",
                    ImageId = 1,                   
                    Image = formFile.Object,
                    Comments = commentViewModels.Where(co=>co.AnimalId == 1).ToList()
                },
                new AnimalViewModel()
                {
                    AnimalId = 2,
                     CategoryId = 1,
                     Age = 7,
                    Name = "animal2",
                    Description = "this is animal 2",
                    ImageId = 2,                   
                    Image = formFile.Object,
                    Comments = commentViewModels.Where(co=>co.AnimalId == 2).ToList()
                },
                new AnimalViewModel()
                {
                    AnimalId = 3,
                    CategoryId = 2,
                    Age = 23,
                    Name = "animal3",
                    Description = "this is animal 3",
                    ImageId = 3,
                    Image = formFile.Object,                  
                    Comments = commentViewModels.Where(co=>co.AnimalId == 3).ToList()
                },
                new AnimalViewModel()
                {
                    AnimalId = 4,
                    CategoryId = 2,
                    Age = 533,
                    Name = "animal4",
                    Description = "this is animal 4",
                    ImageId = 4,
                    Image = formFile.Object,                  
                    Comments = commentViewModels.Where(co=>co.AnimalId == 4).ToList()
                }
            };

            categoryViewModels = new List<CategoryViewModel>()
            {
                new CategoryViewModel()
                {
                    CategoryId = 1,
                    Name = "cate1",
                    Animals = animalViewModels.Where(ani=>ani.CategoryId == 1).ToList()
                },
                new CategoryViewModel()
                {
                    CategoryId = 2,
                    Name = "cate2",
                    Animals = animalViewModels.Where(ani=>ani.CategoryId == 2).ToList()
                }
            };           
            animalViewModels.ForEach(ani => ani.CategoryName = categoryViewModels.Find(cat => cat.CategoryId == ani.CategoryId).Name);

        }

        public IList<Category> GetCategorys() => categories; 
        public IList<Animal> GetAnimals() => animals;
        public IList<Comment> GetComments() => comments;
        public IList<Image> GetImages() => images;


        public IList<CategoryViewModel> GetCategoryViewModels() => categoryViewModels;
        public IList<AnimalViewModel> GetAnimalViewModels() => animalViewModels;
        public IList<CommentViewModel> GetCommentViewModels() => commentViewModels;

    }
}
