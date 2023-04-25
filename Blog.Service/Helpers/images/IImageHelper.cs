using Blog.Entity.Enum;
using Blog.Entity.VİewModels.Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Helpers.images
{
    public interface IImageHelper
    {
        Task<ImageUpLoadedDto> Upload(string name,IFormFile imageFile,ImageType ımageType, string folderName=null );

        void Delete(string imageName);
    }
}
