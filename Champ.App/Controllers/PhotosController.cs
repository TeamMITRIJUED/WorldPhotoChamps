namespace Champ.App.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using System.Linq;

    using Champ.Models;
    using Models.PhotoModels;

    public class PhotosController : BaseController
    {
        [Authorize]
        [HttpGet]
        public ActionResult GetPhotos()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var photos = this.Data.Pictures.All()
                .Where(u => u.AuthorId == loggedUserId)
                .Take(10)
                .Select(p => new PhotoViewModel
                {
                    Location = p.LocationPath,
                    Author = p.Author.UserName
                })
                .ToList();
       
            return this.PartialView("_ViewPhoto", photos);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(int contestId, HttpPostedFileBase file)
        {
            CloudBlockBlob image = null;

            if (file != null && file.ContentLength > 0)
            {
                image= await this.UploadBlobAsync(file);
            }

            if (image == null)
            {
                throw new HttpException();
            }

            var loggedUserId = this.User.Identity.GetUserId();
            var contest = this.Data.Contests.Find(contestId);

            var photo = new Picture
            {
                AuthorId = loggedUserId,
                ContestId = contestId,
                CreatedOn = DateTime.Now,
                LocationPath = image.Uri.ToString()
            };

            contest.Pictures.Add(photo);
            this.Data.SaveChanges();

            return RedirectToAction("ViewContests", "Contest");
        }

        private async Task<CloudBlockBlob> UploadBlobAsync(HttpPostedFileBase imageFile)
        {

            string blobName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName); 
            var imageBlob = imagesContainer.GetBlockBlobReference(blobName);

            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }

            return imageBlob;
        }
    }
}