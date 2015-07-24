using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
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
        public HttpResponseMessage AddMenuItem(string name, float price, double menuPrice, double profit, string type, bool isActive, string beverageIds = null, string inventoryIds = null)
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
                    BeverageIds = beverageIds,
                    InventoryIds = inventoryIds
                };
                _unitOfWork.MenuItems.Create(menuItem);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getEditData")]
        public HttpResponseMessage GetEditData(string beverageIds = null, [FromUri] string inventoryIds = null )
        {
            try
            {
                List<BeverageInventory> beverageList = new List<BeverageInventory>();
                if (beverageIds != null)
                {
                    beverageIds = beverageIds[beverageIds.Length - 1] == ','
                        ? beverageIds.Remove(beverageIds.Length - 1)
                        : beverageIds;
                    foreach (var id in beverageIds.Split(','))
                    {
                        beverageList.Add(_unitOfWork.BeverageInventories.GetById(Int32.Parse(id)));
                    }
                }

                List<Inventory> inventoryList = new List<Inventory>();
                if (inventoryIds != null)
                {
                    inventoryIds = inventoryIds[inventoryIds.Length - 1] == ','
                        ? inventoryIds.Remove(inventoryIds.Length - 1)
                        : inventoryIds;
                    foreach (var id in inventoryIds.Split(','))
                    {
                        inventoryList.Add(_unitOfWork.Inventories.GetById(Int32.Parse(id)));
                    }
                }


                return Request.CreateResponse(HttpStatusCode.OK, new {InventoryList = inventoryList, BeverageList = beverageList});

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }




    }
}
