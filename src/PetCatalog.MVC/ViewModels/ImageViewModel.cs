﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.ViewModels
{
    public class ImageViewModel
    {
        public int ImageId { get; set; }

        public string Name { get; set; }
        public byte[] data { get; set; }
        public AnimalViewModel Animal { get; set; }
    }
}
