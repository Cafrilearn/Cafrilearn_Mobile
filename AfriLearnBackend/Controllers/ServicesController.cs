using AfriLearnBackend.Constants;
using AfriLearnBackend.Models;
using AfriLearnBackend.Services;
using CMapp_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;

namespace AfriLearnBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "Bearer")]
    public class ServicesController : ControllerBase
    {
        private readonly  AfriLearnDbContext _context;
        public ServicesController(AfriLearnDbContext context)
        {
           _context = context;
        }
                
        [HttpPost("ResetPassword")]
        public  IActionResult ChangePasswordDto([FromBody]ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var code = EmailServices.SendEmailForPasswordRecoveryCode(changePasswordDto.Email);
                return  Ok(code);
            }
            return BadRequest($"Could not send password recovery code to user with email {changePasswordDto.Email}");
        }

        [HttpPost("Images")]
        public  IActionResult StoreBlob([FromBody]BlobItemDto blobItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request Parameters");
            }
            var blobStorageService = new BlobStorageService();
            var connectionString = blobStorageService.GetCredentials();
            var account = CloudStorageAccount.Parse(connectionString);
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(AppConstants.ContainerName);
            string uniqueName = Guid.NewGuid().ToString();
            var userKey = blobItemDto.ContainerName;
            var blockBlob = container.GetBlockBlobReference($"{userKey}{uniqueName}.jpg");
            blockBlob.UploadFromStreamAsync(new MemoryStream(blobItemDto.TakenImageBytes));
            return Ok(blockBlob.Uri.OriginalString);
        }
    }
}
