using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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
            var customers = _context.Customers.Include(c=>c.MembershipType).ToList();

            return View(customers);
        }

        /// <summary>
        /// Action to show the customer listing.
        /// </summary>
        /// <param name="id">The identifier of the customer to show.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer =  new Customer()  // prevent the validation error
            };

            return View("CustomerForm", viewModel);
        }

        // Hybrid action (either updates OR creates a new one, depending if the record exists).
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                // New customer
                _context.Customers.Add(customer);
            }
            else
            {
                // Throws an exception, but desirable here.
                var databaseCustomer = _context.Customers.Single(c => c.Id == customer.Id);

                // Special controller method of updating, TryUpdateModel --> not the recommended type (might not want all updates based on priviledges, etc). The method allows everything and 
                // is a security problem. Work-around is a white-listing of property names (not great)
                databaseCustomer.Name = customer.Name;
                databaseCustomer.BirthDate = customer.BirthDate;
                databaseCustomer.MembershipTypeId = customer.MembershipTypeId;
                databaseCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                // Could also use AutoMapper to perform the above update.
            }
            _context.SaveChanges(); // Create the changes in the database now.

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}