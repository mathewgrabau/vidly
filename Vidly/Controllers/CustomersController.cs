using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //private List<Customer> _customers = new List<Customer>();
        
        // GET
        public ActionResult Index()
        {
            var customers = _context.Customers.ToList();

            return View(customers);
        }

        /// <summary>
        /// Action to show the customer listing.
        /// </summary>
        /// <param name="id">The identifier of the customer to show.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
    }
}