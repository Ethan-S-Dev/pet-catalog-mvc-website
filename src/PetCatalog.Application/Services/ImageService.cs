using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public string GetImageName(int id)
        {
            var image = imageRepository.Get(id);
            if (image is null) return null;
            return image.Name;
        }
    }
}
