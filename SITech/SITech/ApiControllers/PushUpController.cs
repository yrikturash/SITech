using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SITech.DTO;
using SITech.Models;

namespace SITech.ApiControllers
{
    [System.Web.Http.RoutePrefix("api/pushups")]
    public class PushUpController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public PushUpController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getdata")]
        public HttpResponseMessage GetPushUpData()
        {
            try
            {
                var customerId = User.Identity.GetUserId();

                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        BeverageList =
                            _unitOfWork.BeverageInventories.GetAll(),
                        InventoryList = _unitOfWork.Inventories.GetAll()
                    });

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("addMenuItem")]
        public HttpResponseMessage AddMenuItem(string name, float price, double menuPrice, double profit, string type, bool isActive, string beverageInventoryIds)
        {
            try
            {

                var menuItem = new MenuItemViewModel()
                {
                    IsActive = isActive,
                    CustomerId = User.Identity.GetUserId(),
                    ItemName = name,
                    ItemPrice = price,
                    ItemType = type,
                    Profit = profit,
                    MenuPrice = menuPrice,
                    BeverageInventoryIds = beverageInventoryIds
                };
                _unitOfWork.MenuItems.Create(menuItem);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
