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
    }
}
