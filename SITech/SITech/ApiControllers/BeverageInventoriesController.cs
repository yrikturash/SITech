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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("get")]
        public HttpResponseMessage GetBeverageInventoryById(int id)
        {
            try
            {
                
                return Request.CreateResponse(HttpStatusCode.OK, _unitOfWork.BeverageInventories.GetById(id));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
