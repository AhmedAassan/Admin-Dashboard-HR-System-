using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect.BL.Helper
{
    public static class FileUploader
    {
        public static string UploadFile(string LocalPath, IFormFile File)
        {
            try
            {
                // 1 ) Get Directory

                                     // Get Directory on the Server
                string FilePath = Directory.GetCurrentDirectory() + LocalPath;


                //2) Get File Name
                                 //Unique Number     //اسم الملف بدون اي شوائب او زيادات
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);


                // 3) Merge Path with File Name

                                       //بيظبط الاسلاش
                string FinalImgPath = Path.Combine(FilePath, FileName);


                //4) Save File As Streams "Data Overtime"

                using (var Stream = new FileStream(FinalImgPath, FileMode.Create))

                {
                    // نفسه file
                    File.CopyTo(Stream);

                }
                return FileName;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }



        public static string RemoveFile(string LocalPath, string FileName)
        {
            try
            {
                string DeletePath = Directory.GetCurrentDirectory() + LocalPath + FileName;

                if (File.Exists(DeletePath))
                {
                    File.Delete(DeletePath);
                }
                var result = "Deleted";
                return result;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }


    
}
