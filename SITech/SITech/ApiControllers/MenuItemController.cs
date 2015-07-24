using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using SITech.DTO;
using SITech.Models;

namespace SITech.ApiControllers
{

    [System.Web.Http.RoutePrefix("api/menuitem")]
    public class MenuItemController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public MenuItemController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("get")]
        public HttpResponseMessage GetMenuItem(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    _unitOfWork.MenuItems.Get(id));

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("delete")]
        public HttpResponseMessage DeleteMenuItem(int id)
        {
            try
            {
                _unitOfWork.MenuItems.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("delete")]
        public HttpResponseMessage DeleteMenuItem([FromUri] string ids)
        {
            try
            {
                var idsList = ids.Split(',');
                foreach (var id in idsList)
                {
                    _unitOfWork.MenuItems.Delete(Convert.ToInt32(id));
                }
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("put")]
        public HttpResponseMessage UpdateMenuItem(MenuItemViewModel item)
        {
            try
            {
                _unitOfWork.MenuItems.Update(item);
                _unitOfWork.Save();
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPut]
        [System.Web.Http.Route("deactivate")]
        public HttpResponseMessage DectivateMenuItems([FromUri]string ids)
        {
            try
            {
                var idsList = ids.Split(',');
                foreach (var id in idsList)
                {
                    _unitOfWork.MenuItems.DeActivate(Convert.ToInt32(id));
                }
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [System.Web.Http.Route("activate")]
        public HttpResponseMessage ActivateMenuItems([FromUri] string ids)
        {
            try
            {
                var idsList = ids.Split(',');
                foreach (var id in idsList)
                {
                    _unitOfWork.MenuItems.Activate(Convert.ToInt32(id));
                }
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [System.Web.Http.Route("deleteBeverageId")]
        public HttpResponseMessage DeleteBeveregeId(int menuItemId, string id)
        {
            try
            {
                var model = _unitOfWork.MenuItems.GetById(menuItemId);




                if (!model.BeverageIds.Contains(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Item with that id not exist");
                }

                if (model.BeverageIds.Length == 2)
                {
                    model.BeverageIds = null;
                    _unitOfWork.MenuItems.Update(model);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }


                string ids = "";
                var temp = model.BeverageIds.Split(',').ToList();
                temp.Remove(id);
                foreach (var tId in temp)
                {
                    ids += tId + ",";
                }
                model.BeverageIds = ids;

                _unitOfWork.MenuItems.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [System.Web.Http.Route("deleteInventoryId")]
        public HttpResponseMessage DeleteInventoryId(int menuItemId, string id)
        {
            try
            {
                var model = _unitOfWork.MenuItems.GetById(menuItemId);



                if (!model.InventoryIds.Contains(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Item with that id not exist");
                }

                if (model.InventoryIds.Length == 2)
                {
                    model.InventoryIds = null;
                    _unitOfWork.MenuItems.Update(model);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }


                string ids = "";
                var temp = model.InventoryIds.Split(',').ToList();
                temp.Remove(id);
                foreach (var tId in temp)
                {
                    ids += tId + ",";
                }
                model.InventoryIds = ids;


                _unitOfWork.MenuItems.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [System.Web.Http.Route("addInventoryId")]
        public HttpResponseMessage AddInventoryId(int menuItemId, string id)
        {
            try
            {
                var model = _unitOfWork.MenuItems.GetById(menuItemId);
                if (model.InventoryIds.IsNullOrWhiteSpace())
                {
                    model.InventoryIds = id + ",";
                    ;
                }
                else if (model.InventoryIds.Contains(id))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else if (model.InventoryIds[model.InventoryIds.Length - 1] == ',')
                {
                    model.InventoryIds = model.InventoryIds + id + ",";
                }
                else
                {
                    model.InventoryIds = "," + model.InventoryIds + "," + id;
                }
                _unitOfWork.MenuItems.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [System.Web.Http.Route("addBeverageId")]
        public HttpResponseMessage AddBeverageId(int menuItemId, string id)
        {
            try
            {
                var model = _unitOfWork.MenuItems.GetById(menuItemId);
                if (model.BeverageIds.IsNullOrWhiteSpace())
                {
                    model.BeverageIds = id + ",";
                }
                else if (model.BeverageIds.Contains(id))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else if (model.BeverageIds[model.BeverageIds.Length - 1] == ',')
                {
                    model.BeverageIds = model.BeverageIds + id + ",";
                }
                else
                {
                    model.BeverageIds = "," + model.BeverageIds + "," + id;
                }
                _unitOfWork.MenuItems.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
