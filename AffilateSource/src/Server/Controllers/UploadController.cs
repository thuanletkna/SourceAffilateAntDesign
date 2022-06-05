using AffilateSource.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AffilateSource.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        public IWebHostEnvironment HostingEnvironment { get; set; }

        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(IFormFile file) // must match SaveField
        {
            if (file != null)
            {
                try
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    // save to wwwroot - Blazor Server only
                    //var saveLocation = Path.Combine(HostingEnvironment.WebRootPath, "user-attachments", files.FileName);
                    // save to project root - Blazor Server or WebAssembly
                    //var saveLocation = Path.Combine(HostingEnvironment.ContentRootPath, fileName);
                    var saveLocation = Path.Combine(HostingEnvironment.ContentRootPath + "\\Uploads\\PostImages", fileName);
                    //var physicalPath = Path.Combine(HostingEnvironment.ContentRootPath + "\\Uploads\\Temp", fileName);
                    using (var fileStream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var finalName = GetfileToSave(fileName);
                    //var rootPath = HostingEnvironment.ContentRootPath + "\\Uploads\\PostImages";
                    return Content(finalName);
                }
                catch
                {
                    Response.StatusCode = 500;
                    await Response.WriteAsync("Upload failed.");
                }
            }

            return new EmptyResult();
        }
        [HttpPost]
        [Route("Remove")]
        public ActionResult Remove(string file) // must match RemoveField
        {
            if (file != null)
            {
                try
                {
                    // delete from wwwroot - Blazor Server only
                    //var fileLocation = Path.Combine(HostingEnvironment.WebRootPath, file);
                    var fileLocation = Path.Combine(HostingEnvironment.ContentRootPath + "\\Uploads\\PostImages", file);
                    // delete from project root - Blazor Server or WebAssembly
                    //var fileLocation = Path.Combine(HostingEnvironment.ContentRootPath, files);

                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                catch
                {
                    Response.StatusCode = 500;
                    Response.WriteAsync("File deletion failed.");
                }
            }

            return new EmptyResult();
        }
        private string GetfileToSave(string filename)
        {
            try
            {
                var rootPath = HostingEnvironment.ContentRootPath + "\\Uploads\\PostImages";
                string strExtension = Path.GetExtension(filename).ToLower();
                //var saveLocation = Path.Combine(HostingEnvironment.ContentRootPath, filename);
                var fileFinal = LibHelper.FormatURLText(Path.GetFileNameWithoutExtension(filename)) + strExtension;
                //System.IO.File.Move(rootPath + "\\" + filename, rootPath + fileFinal);
                return fileFinal;
            }
            catch (Exception ex)
            {
                var mes = ex.Message;
                throw;
            }
        }
    }
}
