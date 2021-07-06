using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Context;
using PetCatalog.Infra.Data.FileSavers;
using System;
using System.Collections.Generic;
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
            if (obj.Data is null) throw new ArgumentNullException();

        }

        public Image Delete(int animalId)
        {
            throw new NotImplementedException();
        }

        public Image Get(int id)
        {
            
        }

        public IEnumerable<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Image animal)
        {
            throw new NotImplementedException();
        }
    }
}
