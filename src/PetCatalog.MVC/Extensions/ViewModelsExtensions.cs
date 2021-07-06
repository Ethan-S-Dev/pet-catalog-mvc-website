using PetCatalog.Application.Interfaces;
using PetCatalog.Application.ViewModels;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Extensions
{
    public static class ViewModelsExtensions
    {
        public static void SetCategory(this AnimalViewModel animalVm, AnimalFormModel animalForm,ICategoryService categoryService)
        {
            if (animalForm.CategoryId < 0)
            {
                categoryService.AddCategory(animalForm.CategoryName, out int cateId);
                animalVm.CategoryId = cateId;
            }
            else
            {
                animalVm.CategoryId = animalForm.CategoryId;
            }
        }
        public static Stream SetPicture(this AnimalViewModel animalVm, AnimalFormModel animalForm)
        {
            Stream imgStream = null;
            if (animalForm.Image is not null)
            {
                var unsafeFileName = Path.GetFileName(animalForm.Image.FileName);
                var exten = Path.GetExtension(unsafeFileName);
                var newFileName = $"{Guid.NewGuid()}{exten}";
                imgStream = new MemoryStream();
                animalForm.Image.CopyTo(imgStream);
                imgStream.Position = 0;
                animalVm.PictureName = newFileName;
            }
            return imgStream;
        }
        public static void SetCategory(this AnimalViewModel animalVm, AnimalEditModel animalForm, ICategoryService categoryService)
        {
            if (animalForm.CategoryId < 0)
            {
                categoryService.AddCategory(animalForm.CategoryName, out int cateId);
                animalVm.CategoryId = cateId;
            }
            else
            {
                animalVm.CategoryId = animalForm.CategoryId;
            }
        }
        public static void SetPicture(this AnimalViewModel animalVm, AnimalEditModel animalForm,IImageService imageService)
        {
            var oldFileName = animalForm.Animal.PictureName;

            if (animalForm.Image is not null)
            {
                var unsafeFileName = Path.GetFileName(animalForm.Image.FileName);
                var exten = Path.GetExtension(unsafeFileName);
                var newFileName = $"{Guid.NewGuid()}{exten}";
                using var imgStream = new MemoryStream();
                animalForm.Image.CopyTo(imgStream);
                imgStream.Position = 0;
                if (imageService.UpdateImage(oldFileName, newFileName, imgStream))
                {
                    animalVm.PictureName = newFileName;
                    return;
                }
            }

            animalVm.PictureName = oldFileName;

        }
    }

}
