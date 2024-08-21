using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.NTI.BLL.Helper
{
    public static class FileUploader
    {
        public static string Upload(string FolderName , IFormFile fileUrl)
        {

            // 1 ) Get Directory
            string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot" + FolderName;


            //2) Get File Name
            string FileName = Guid.NewGuid() + Path.GetFileName(fileUrl.FileName);


            // 3) Merge Path with File Name
            string FinalPath = Path.Combine(FolderPath, FileName);


            //4) Save File As Streams "Data Overtime"
            using (var Stream = new FileStream(FinalPath, FileMode.Create))
            {

                fileUrl.CopyTo(Stream);
            }


            return FileName;
        }

        public static void Remove(string FolderName , string FileName)
        {

            string path = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName + FileName;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
