using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HandT_Api_Layer.DomainInterface
{
    public interface IDestinationImageRepo
    {
        //public async Task<IActionResult> UploadImage(IFormFile formFile, string destinationcode)

        public Task<IActionResult> MultiUploadImage(IFormFileCollection fileCollection, string destinationcode);
        public Task<IActionResult> GetImage(string destinationcode);

        public Task<IActionResult> GetMultiImage(string destinationcode);

        public Task<IActionResult> download(string destinationcode);

    }
}




