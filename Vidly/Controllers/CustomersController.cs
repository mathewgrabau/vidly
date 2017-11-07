using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //private List<Customer> _customers = new List<Customer>
        //{
        //    new Customer {Name = "John Smith", Id = 1},
        //    new Customer {Name = "Mary Williams", Id = 2}
        //};

            private List<Customer> _customers = new List<Customer>();
        
        // GET
        public ActionResult Index()
        {
            return View(_customers);
        }

        /// <summary>
        /// Action to show the customer listing.
        /// </summary>
        /// <param name="id">The identifier of the customer to show.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (!id.HasValue || id > _customers.Count)
            {
                // return a standard 404 for right now.
                return HttpNotFound();
            }

            return View(_customers[id.Value - 1]);
        }
    }
}