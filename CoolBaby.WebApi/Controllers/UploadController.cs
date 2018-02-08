using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CoolBaby.WebApi.Controllers
{
    /// <summary>
    /// Upload api
    /// </summary>
    [Authorize]
    public class UploadController : ApiController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        ///
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage()
        {
            DateTime now = DateTime.Now;
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                return new BadRequestObjectResult(files);
            }
            else
            {
                var file = files[0];
                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB
                var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                var ext = filename.Substring(filename.LastIndexOf('.'));
                var extension = ext.ToLower();
                if (!AllowedFileExtensions.Contains(extension))
                {
                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                    return new BadRequestObjectResult(message);
                }
                else if (file.Length > MaxContentLength)
                {
                    var message = string.Format("Please Upload a file upto 1 mb.");
                    return new BadRequestObjectResult(message);
                }

                var imageFolder = $@"\uploaded\images\{now.ToString("yyyyMMdd")}";

                string folder = _hostingEnvironment.WebRootPath + imageFolder;

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string filePath = Path.Combine(folder, filename);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                return new OkObjectResult(Path.Combine(imageFolder, filename).Replace(@"\", @"/"));
            }
        }

        //[HttpPost]
        //public async Task UploadImageForCKEditor(IList<IFormFile> upload, string CKEditorFuncNum, string CKEditor, string langCode)
        //{
        //    DateTime now = DateTime.Now;
        //    if (upload.Count == 0)
        //    {
        //        await HttpContext.Response.WriteAsync("Yêu cầu nhập ảnh");
        //    }
        //    else
        //    {
        //        var file = upload[0];
        //        var filename = ContentDispositionHeaderValue
        //                            .Parse(file.ContentDisposition)
        //                            .FileName
        //                            .Trim('"');

        //        var imageFolder = $@"\uploaded\images\{now.ToString("yyyyMMdd")}";

        //        string folder = _hostingEnvironment.WebRootPath + imageFolder;

        //        if (!Directory.Exists(folder))
        //        {
        //            Directory.CreateDirectory(folder);
        //        }
        //        string filePath = Path.Combine(folder, filename);
        //        using (FileStream fs = System.IO.File.Create(filePath))
        //        {
        //            file.CopyTo(fs);
        //            fs.Flush();
        //        }
        //        await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + Path.Combine(imageFolder, filename).Replace(@"\", @"/") + "');</script>");
        //    }
        //}
    }
}