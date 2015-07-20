using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SITech.Models;
using SITech.DTO;

namespace SITech.Controllers
{
    public class CustomerController : Controller
    {
         UnitOfWork unitOfWork;

         public CustomerController()
          {
             unitOfWork = new UnitOfWork();
          }

       
        
        //
        // GET: /Customer/All
        public ActionResult Customers()
        {           
           return View(unitOfWork.Customers.GetAll(true));            
        }

        //
        // GET: /PDP/All
        public ActionResult PDPs()
        {          
           return View(unitOfWork.Customers.GetAll(false));
        }


        //
        // GET: Add Customer
        public ActionResult AddCustomer(bool active = false)
        {
            CustomerViewModel model = new CustomerViewModel();
            //var cusRoles = new ApplicationDbContext().Roles.Where(role => role.Name !="Admin").ToList();

            //foreach (var role in cusRoles)
            //{
            //    model.Roles.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            //}
            if (active)
            {
                model.RoleName = "Customer";
            }
            else
            {
                model.RoleName = "PDP";
            }
          

            return View(model);
        }

        //
        // POST: /Customer/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(CustomerViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = unitOfWork.Customers.Create(model, file);
               
                if (result.Succeeded)
	            {
                    if (model.RoleName == "Customer")
                {
                    return RedirectToAction("Customers", "Customer");
                }
                else
                {
                    return RedirectToAction("PDPs", "Customer");
                }
		          
	            }             
                
                AddErrors(result);
            }
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        

        // GET: /Customer/Edit 
        public ActionResult EditCustomer(string id, AccountController.ManageMessageId? message = null)
        {
            using (var db = new ApplicationDbContext())
            {
           
                Customer customer = unitOfWork.Customers.FindById(id);
                var model = new EditCustomerViewModel();
                
                model.CustomerId = customer.CustomerId;
                model.UserName = customer.User.UserName;
                model.FirstName = customer.User.FirstName;
                model.MiddleName = customer.User.MiddleName;
                model.LastName = customer.User.LastName;
                model.Email = customer.User.Email;
                model.RoleName = unitOfWork.Customers.GetRole(customer.CustomerId);

                var cusRoles = new ApplicationDbContext().Roles.Where(role => role.Name != "Admin").ToList();

                foreach (var role in cusRoles)
                {
                    model.Roles.Add(new SelectListItem { Text = role.Name, Value = role.Name });
                }

                ViewBag.MessageId = message;

                return View(model);
            }
        }


        // POST: /Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomer(EditCustomerViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                 IdentityResult result = await unitOfWork.Customers.Update(model, file);

                if (result.Succeeded)
	            {
		 
                    if (model.RoleName == "Customer")
                    {
                        return RedirectToAction("Customers", "Customer");
                    }
                    else
                    {
                        return RedirectToAction("PDPs", "Customer");
                    }
	            }
                    
              
                AddErrors(result);
            }
            return View(model);
        }
       

        // PUT: /Customer/Activate
        public JsonResult Activate(string UserId, string RoleName)
        {            
            unitOfWork.Customers.ChangeRoles(UserId, RoleName);
            return Json("Response from Activate");
        }

       
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }    

        #endregion

	}
}