using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SITech.Models;

namespace SITech.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var unitOfWork = new UnitOfWork();

            ViewData["Inventories"] = unitOfWork.Inventories.GetAll();
            ViewData["Inventories"] = unitOfWork.BeverageInventories.GetAll();


        }
    }
}