using Blog.Entity.VİewModels.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entity.Enum;

namespace Blog.Entity.Helpers.images
{
    public class ImageHelper : IImageHelper
    {
		private readonly IHostingEnvironment hostingEnvironment;
        private readonly string wwwroot;  //wwwroot adlı klasöre ulaştık
        private const string imgFolder = "images";  //genel kullanımda daha kolaylık sağlıyor bir çok yerde değil tek yerden değiştirmemizi sağlıyor
        private const string articleImagesFolder = "article-images";
        private const string UserImagesFolder = "user-images";

		public ImageHelper(IHostingEnvironment hostingEnvironment)
		{
			this.hostingEnvironment = hostingEnvironment;

            wwwroot = hostingEnvironment.WebRootPath;
		}

		private string ReplaceInvalidChars(string fileName)
		{
			return fileName.Replace("İ", "I")
				 .Replace("ı", "i")
				 .Replace("Ğ", "G")
				 .Replace("ğ", "g")
				 .Replace("Ü", "U")
				 .Replace("ü", "u")
				 .Replace("ş", "s")
				 .Replace("Ş", "S")
				 .Replace("Ö", "O")
				 .Replace("ö", "o")
				 .Replace("Ç", "C")
				 .Replace("ç", "c")
				 .Replace("é", "")
				 .Replace("!", "")
				 .Replace("'", "")
				 .Replace("^", "")
				 .Replace("+", "")
				 .Replace("%", "")
				 .Replace("/", "")
				 .Replace("(", "")
				 .Replace(")", "")
				 .Replace("=", "")
				 .Replace("?", "")
				 .Replace("_", "")
				 .Replace("*", "")
				 .Replace("æ", "")
				 .Replace("ß", "")
				 .Replace("@", "")
				 .Replace("€", "")
				 .Replace("<", "")
				 .Replace(">", "")
				 .Replace("#", "")
				 .Replace("$", "")
				 .Replace("½", "")
				 .Replace("{", "")
				 .Replace("[", "")
				 .Replace("]", "")
				 .Replace("}", "")
				 .Replace(@"\", "")
				 .Replace("|", "")
				 .Replace("~", "")
				 .Replace("¨", "")
				 .Replace(",", "")
				 .Replace(";", "")
				 .Replace("`", "")
				 .Replace(".", "")
				 .Replace(":", "")
				 .Replace(" ", "");
		}


        public async Task<ImageUpLoadedDto> Upload(string name, IFormFile imageFile,ImageType imageType, string folderName = null)
        {
			folderName ??= imageType == ImageType.User ? UserImagesFolder : articleImagesFolder;
			//klasörlere bakcaz böyle bir dosya açılmışmı 
			if (!Directory.Exists($"{wwwroot}/{imgFolder}/{folderName}"))
				Directory.CreateDirectory($"{wwwroot}/{imgFolder}/{folderName}");

			string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName);

			string fileExtension=Path.GetExtension(imageFile.FileName);

			name =ReplaceInvalidChars(name);

			DateTime dateTime = DateTime.Now;

			string newFileName = $"{name}_{dateTime.Millisecond}{fileExtension}";


			var path = Path.Combine($"{wwwroot}/{imgFolder}/{folderName}", newFileName);


			await using var stream=new FileStream(path,FileMode.Create, FileAccess.Write, FileShare.None,1024*1024,useAsync:false);

			await imageFile.CopyToAsync(stream);

			await stream.FlushAsync(); //flush boşalttık

			string message = imageType == ImageType.User ? $"{newFileName}isimli kullanıcı resmi başarı ile eklenmiştir." : $"{newFileName} isimli makele resmi başarı ile eklenmiştir.";

			return new ImageUpLoadedDto()
			{
				FullName = $"{folderName}/{newFileName}"
			};

		}

		public void Delete(string imageName)
		{
			var fileToDelete = Path.Combine($"{wwwroot}/{imgFolder}/{imageName}");

			if(File.Exists(fileToDelete))
				File.Delete(fileToDelete);

		}

	}
}
