using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Context;
using PetCatalog.Infra.Data.FileSavers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Repositorys
{
    public class ImageRepository : IImageRepository
    {
        private readonly FileSaver fileSaver;
        private readonly PetCatalogDbContext dbContext;
        public ImageRepository(FileSaver fileSaver,PetCatalogDbContext dbContext)
        {
            this.fileSaver = fileSaver;
            this.dbContext = dbContext;
        }
        public void Create(Image obj)
        {
            if (obj is null) throw new ArgumentNullException();
            if (obj.Data is null) throw new ArgumentNullException();
            if (obj.Name is null) throw new ArgumentNullException();

            var extention = Path.GetExtension(obj.Name);
            var newName = $"{Guid.NewGuid()}{extention}";
            if(fileSaver.Save(newName,obj.Data))
            {
                obj.Name = newName;
                dbContext.Images.Add(obj);
                dbContext.SaveChanges();
            }
        }

        public Image Delete(int id)
        {
            var image = dbContext.Images.Find(id);
            if (image is null) return null;

            if (fileSaver.Delete(image.Name))
            {
                dbContext.Images.Remove(image);
                dbContext.SaveChanges();
                return image;
            }

            return null;
        }

        public Image Get(int id)
        {
            return dbContext.Images.Find(id);
        }

        public IEnumerable<Image> GetAll()
        {
            return dbContext.Images;
        }

        public void Update(Image obj)
        {
            if (obj is null) throw new ArgumentNullException();
            if (obj.Data is null) throw new ArgumentNullException();
            if (obj.Name is null) throw new ArgumentNullException();

            var oldImage = dbContext.Images.Find(obj.ImageId);
            if (oldImage is null) throw new ArgumentNullException();

            var extention = Path.GetExtension(obj.Name);
            var newName = $"{Guid.NewGuid()}{extention}";
            if (fileSaver.Update(oldImage.Name,newName, obj.Data))
            {
                oldImage.Name = newName;
                dbContext.SaveChanges();
            }
        }
    }
}
