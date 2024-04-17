using HandT_Api_Layer.DomainInterface;
using HandT_Test_Mysql.DomainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace HandT_Api_Layer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationImage : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public DestinationImage(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        //[HttpPut("UploadImage")]
        //public async Task<IActionResult> UploadImage(IFormFile formFile, string destinationcode)


        [HttpPut("MultiUploadImage")]
        public async Task<IActionResult> MultiUploadImage(IFormFileCollection fileCollection, string destinationcode)
        {
            ApiResponse response = new ApiResponse();
            int passcount = 0; int errorcount = 0;
            try
            {
                string filePath = GetFilepath(destinationcode);
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                foreach (var file in fileCollection)
                {
                    string imagepath = filePath + "\\" + file.FileName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        passcount++;

                    }
                }

            }
            catch (Exception ex)
            {
                errorcount++;
                response.ResponseMessage = ex.Message;
                return BadRequest(response); // Return BadRequest with the error message
            }
            response.ResponseCode = 200;
            response.ResponseMessage = passcount + "Files uploaded &" + errorcount + "files failed";
            return Ok(response);
        }

        private string GetFilepath(string destinationcode)
        {
            return this.environment.WebRootPath + "\\Upload\\image\\" + destinationcode;
        }

        //[HttpGet("GetImage")]
        //    public async Task<IActionResult> GetImage(string destinationcode)
        //    {
        //        string Imageurl = string.Empty;
        //        string hosturl = $"{this.Request.Scheme}://{this.Request.PathBase}";
        //        try
        //        {
        //            string filePath = GetFilepath(destinationcode);
        //            string imagepath = filePath + "\\" + destinationcode + ".png";
        //            if (System.IO.File.Exists(imagepath))
        //            {
        //                Imageurl = hosturl + "Upload/image/" + destinationcode + "/" + destinationcode + ".png";

        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        return Ok(Imageurl);
        //    }
        //}
        //    [HttpGet("GetMultiImage")]
        //    public async Task<IActionResult> GetMultiImage(string destinationcode)
        //    {
        //        List<string> ImageUrls = new List<string>();
        //        string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //        try
        //        {
        //            string filePath = GetFilepath(destinationcode);

        //            if (Directory.Exists(filePath))
        //            {
        //                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
        //                FileInfo[] fileInfos = directoryInfo.GetFiles();

        //                foreach (FileInfo fileInfo in fileInfos)
        //                {
        //                    string filename = fileInfo.Name;
        //                    string imageUrl = $"{hostUrl}/Upload/image/{destinationcode}/{filename}";

        //                    ImageUrls.Add(imageUrl);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle the exception (log, return an error response, etc.)
        //            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //        }

        //        return Ok(ImageUrls);
        //    }
        //}

        [HttpGet("download")]
        public async Task<IActionResult> download(string destinationcode)
        {
            //List<string> ImageUrls = new List<string>();
            //string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string filePath = GetFilepath(destinationcode);
                string imagepath = filePath + "\\" + destinationcode + ".png";

                if (System.IO.File.Exists(imagepath))
                {
                    MemoryStream stream = new MemoryStream();
                    using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                    stream.Position = 0;
                    return File(stream, "image/png", destinationcode + ".png");


                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, return an error response, etc.)
                return NotFound();
            }


        }


        //[HttpGet("remove")]
        //public async Task<IActionResult> remove(string destinationcode)
        //{
        //    // string Imageurl = string.Empty;
        //    //string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string Filepath = GetFilepath(destinationcode);
        //        string imagepath = Filepath + "\\" + destinationcode + ".png";
        //        if (System.IO.File.Exists(imagepath))
        //        {
        //            System.IO.File.Delete(imagepath);
        //            return Ok("pass");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }


        //}


        //[HttpGet("remove")]
        //public async Task<IActionResult> Remove(string destinationcode)
        //{
        //    try
        //    {
        //        string directoryPath = GetFilepath(destinationcode);

        //        if (Directory.Exists(directoryPath))
        //        {
        //            Directory.Delete(directoryPath, true); // Set to true to delete recursively
        //            return Ok("pass");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as needed
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}


        //[HttpGet("remove")]
        //public async Task<IActionResult> Remove(string destinationcode)
        //{
        //    try
        //    {
        //        string directoryPath = GetFilepath(destinationcode);

        //        if (Directory.Exists(directoryPath))
        //        {
        //            foreach (string filePath in Directory.GetFiles(directoryPath))
        //            {
        //                System.IO.File.Delete(filePath);
        //            }

        //            return Ok("pass");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as needed
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}


        [HttpGet("multiremove")]
        public async Task<IActionResult> multiremove(string destinationcode)
        {
            // string Imageurl = string.Empty;
            //string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilepath(destinationcode);
                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        fileInfo.Delete();
                    }
                    return Ok("pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }


        }





        //[HttpPut("DBMultiUploadImage")]
        //public async Task<IActionResult> DBMultiUploadImage(IFormFileCollection filecollection, string destinationcode)
        //{
        //    APIResponse response = new APIResponse();
        //    int passcount = 0; int errorcount = 0;
        //    try
        //    {
        //        foreach (var file in filecollection)
        //        {
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                await file.CopyToAsync(stream);
        //                this.context.DestinationImages.Add(new DestinationImage()
        //                {
        //                    DestinationCode = destinationcode,
        //                    ProductImage = stream.ToArray()
        //                });
        //                await this.context.SaveChangesAsync();
        //                passcount++;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorcount++;
        //        response.Errormessage = ex.Message;
        //    }

        //    response.ResponseCode = 200;
        //    response.Result = passcount + " Files uploaded & " + errorcount + " files failed";
        //    return Ok(response);
        //}




    }
}



