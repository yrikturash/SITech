using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using SITech.DTO;
using SITech.Models;

namespace SITech.ApiControllers
{
    [System.Web.Http.RoutePrefix("api/beverageInventories")]
    public class BeverageInventoriesController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public BeverageInventoriesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("add")]
        public HttpResponseMessage AddBeverageInventory([FromBody] BeverageInventoryViewModel model)
        {
            try
            {
                _unitOfWork.BeverageInventories.Create(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("get")]
        public HttpResponseMessage GetBeverageInventoryById(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _unitOfWork.BeverageInventories.Get(id));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("delete")]
        public HttpResponseMessage DeleteById(int id)
        {
            try
            {
                _unitOfWork.BeverageInventories.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("delete")]
        public HttpResponseMessage DeleteById([FromUri] string ids)
        {
            try
            {
                foreach (var id in ids.Split(','))
                {
                    _unitOfWork.BeverageInventories.Delete(Int32.Parse(id));
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("put")]
        public HttpResponseMessage Update([FromBody] BeverageInventoryViewModel model)
        {
            try
            {
                _unitOfWork.BeverageInventories.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
