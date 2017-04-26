using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class FileController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        [Route("file/uploads/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData(string id)
        {

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //string root = HttpContext.Current.Server.MapPath("~/App_Data");
            string uploads = HttpContext.Current.Server.MapPath("~/uploads");
            //http://takwira.azurewebsites.net/uploads/image_aa.jpg
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);
            var provider = new MultipartFormDataStreamProvider(uploads);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);


              
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                    string dest = uploads + "/image_" + id + ".jpg";

                   
                    File.Copy(file.LocalFileName, dest, true);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
