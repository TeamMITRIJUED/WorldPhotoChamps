﻿namespace Champ.App.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;

    using Champ.Models;

    public class PhotosController : BaseController
    {
        [Authorize]
        public JsonResult Vote(int photoId)
        {
            var loggedUserId = this.User.Identity.GetUserId();
            var loggedUser = this.Data.Users.Find(loggedUserId);
            var photo = this.Data.Pictures.Find(photoId);
            
            var vote = new Vote
            {
                VotedOn = DateTime.Now,
                Voter = loggedUser,
                Picture = photo
            };

            photo.Votes.Add(vote);
            this.Data.SaveChanges();

            return this.Json(new {result = "success"}, JsonRequestBehavior.AllowGet);
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

            return RedirectToAction("GetContest", "Contest", new { id = contest.Id});
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