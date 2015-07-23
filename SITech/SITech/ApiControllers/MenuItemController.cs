using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    }
}
