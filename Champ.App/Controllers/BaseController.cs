
namespace Champ.App.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Data.UnitOfWork;

    public class BaseController : Controller
    {
        public BaseController()
            :this(new PhotoData(new PhotoContext()))
        {          
        }

        public BaseController(IPhotoData data)
        {
            this.Data = data;
        }

        protected IPhotoData Data { get; set; }
    }
}