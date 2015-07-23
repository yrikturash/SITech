﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SITech.DTO;
using SITech.Models;

namespace SITech.ApiControllers
{
    [System.Web.Http.RoutePrefix("api/inventories")]
    public class InventoriesController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public InventoriesController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("add")]
        public HttpResponseMessage AddMenuItem([FromBody] InventoryViewModel model)
        {
            try
            {
                _unitOfWork.Inventories.Create(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("get")]
        public HttpResponseMessage GetInventory([FromUri] int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _unitOfWork.Inventories.Get(id));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("put")]
        public HttpResponseMessage UpdateInventory([FromBody] InventoryViewModel model)
        {
            try
            {
                _unitOfWork.Inventories.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }

}
