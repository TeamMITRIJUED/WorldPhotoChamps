namespace Champ.App.Controllers
{
    using System.Web.Mvc;    
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Data;
    using Data.UnitOfWork;

    public class BaseController : Controller
    {
        protected static CloudBlobContainer imagesContainer;

        public BaseController()
            :this(new PhotoData(new PhotoContext()))
        {          
        }

        public BaseController(IPhotoData data)
        {
            this.Data = data;
            InitStorage();
        }

        protected IPhotoData Data { get; set; }

        private static void InitStorage()
        {
            var credentials = new StorageCredentials(resources.AccountName, resources.AccountKey);
            var storageAccount = new CloudStorageAccount(credentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            imagesContainer = blobClient.GetContainerReference("images");
        }
    }
}