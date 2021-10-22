using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat_App.Helper
{
    public class ImageUpladerReturn
    {
        public bool isOk { get; set; }
        public string value { get; set; }
    }

    public class ImageUploder
    {
        public static IWebHostEnvironment _enviroment;

        public ImageUploder(IWebHostEnvironment enviroment)
        {
            _enviroment = enviroment;
        }

        public ImageUpladerReturn UplodeFile(IFormFile File)
        {
            try
            {
                    if ( File != null && File.Length > 0)
                {
                    if (!Directory.Exists(_enviroment.WebRootPath + "\\Uplode\\"))
                    {
                        Directory.CreateDirectory(_enviroment.WebRootPath + "\\Uplode\\");
                    }

                    using FileStream fileStream = System.IO.File.Create(_enviroment.WebRootPath + "\\Uplode\\" + File.FileName);
                    File.CopyTo(fileStream);
                    fileStream.Flush();

                    return new ImageUpladerReturn()
                    {
                        isOk = true,
                        value = ("\\Uplode\\" + File.FileName)
                    };
                }
                else
                {
                    return new ImageUpladerReturn()
                    {
                        isOk = false,
                        value = ("Failed")
                    };
                }
            }
            catch (Exception ex)
            {
                return new ImageUpladerReturn()
                {
                    isOk = false,
                    value = ex.Message.ToString()
                };
            }

        }

    }
}
