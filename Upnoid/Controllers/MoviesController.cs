using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Upnoid.Core.Data;
using Upnoid.Domain.Helpers;
using Upnoid.Domain.Models;
using Upnoid.Core.Abstracts;

namespace Upnoid.Controllers
{
    public class MoviesController : Controller
    {
        private IMovieRepository _context;

        public MoviesController(IMovieRepository context)
        {
            _context = context;    
        }

        // GET: Movies
        /* public async Task<IActionResult> Index()
         {
             return View(await _context.Movies.ToListAsync());
         }
         */

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
            var movies = await _context.AllIncluding(x => x.MovieGenres);
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Name.Contains(searchString)
                                        || s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(s => s.Name);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Movie>.CreateAsync(movies, page ?? 1, pageSize));
        }
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.GetSingle(x=>x.Id==id.Value,x=>x.MovieGenres,x=>x.Trailer);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateAdded,Genres,Name,NumberInStock,Price,ReleaseDate")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _context.Add(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.GetSingle(m => m.Id == id,x=>x.Trailer,x=>x.MovieGenres);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateAdded,Genres,Name,NumberInStock,Price,ReleaseDate")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await MovieExistsAsync(movie.Id)))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.GetSingle(m => m.Id == id,x=>x.Trailer,x=>x.MovieGenres);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<bool> MovieExistsAsync(int id)
        {
            bool exist = false;
            Movie movie = await _context.GetSingle(id);
            if (movie != null)
                exist = true;
            return exist;
        }
    }
}
