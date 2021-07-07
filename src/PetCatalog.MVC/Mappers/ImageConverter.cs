using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Mappers
{
    public class ImageConverter : ITypeConverter<IFormFile, Image>, IValueConverter<IFormFile,Image>
    {

        public Image Convert(IFormFile sourceMember, ResolutionContext context)
        {
            if (sourceMember is null) return new Image();

            using var stream = new MemoryStream();
            sourceMember.CopyTo(stream);
            stream.Position = 0;
            return new Image()
            {
                ImageId = 0,
                Name = sourceMember.FileName,
                Data = stream.ToArray(),
            };
        }

        public Image Convert(IFormFile source, Image destination, ResolutionContext context)
        {
            if (source is null) return new Image();

            using var stream = new MemoryStream();
            source.CopyTo(stream);
            stream.Position = 0;
            return new Image()
            {
                ImageId = 0,
                Name = source.FileName,
                Data = stream.ToArray(),
            };
        }
    }
}
