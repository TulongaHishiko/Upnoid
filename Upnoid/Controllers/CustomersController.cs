using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Upnoid.Core.Data;
using Upnoid.Domain.Helpers;
using Upnoid.Domain.Models;
using Upnoid.Core.Abstracts;

namespace Upnoid.Controllers
{
   // [Authorize (Roles ="Admin")]
    public class CustomersController : Controller
    {
        private ICustomerRepository _context;
 
        public CustomersController(ICustomerRepository context)
        {
            _context = context;    
        }

         public async Task<IActionResult> Index(
             string sortOrder, 
             string currentFilter,
             string searchString,
             int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var customers = await _context.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                        || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            { case "name_desc":
                    customers = customers.OrderByDescending(s => s.LastName);
                    break;
                case "Date": customers = customers.OrderBy(s => s.Birthdate);
                    break; case "date_desc": customers = customers.OrderByDescending(s => s.Birthdate);
                    break;
                default: customers = customers.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Customer>.CreateAsync(customers, page ?? 1, pageSize));
        }

    // GET: Customers/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.GetSingle(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,Email,Birthdate, IsSubcribedToNewsletter")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _context.Add(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.GetSingle(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,Email,Birthdate, IsSubcribedToNewsletter")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await CustomerExists(customer.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.GetSingle(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> CustomerExists(int id)
        {
            bool exist = false;
            Customer customer = await _context.GetSingle(id);
            if (customer != null)
                exist = true;
            return exist;
        }
    }
}
