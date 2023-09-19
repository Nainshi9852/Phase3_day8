using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagement.Models;

namespace BookManagement.Controllers
{
    public class BookStoresController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookStoresController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET: BookStores
        public async Task<IActionResult> Index()
        {
              return _context.BookStores != null ? 
                          View(await _context.BookStores.ToListAsync()) :
                          Problem("Entity set 'BookStoreDbContext.BookStores'  is null.");
        }

        // GET: BookStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookStores == null)
            {
                return NotFound();
            }

            var bookStore = await _context.BookStores
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        // GET: BookStores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookName,AuthorName,PublisherName,Price,Category")] BookStore bookStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookStore);
        }

        // GET: BookStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookStores == null)
            {
                return NotFound();
            }

            var bookStore = await _context.BookStores.FindAsync(id);
            if (bookStore == null)
            {
                return NotFound();
            }
            return View(bookStore);
        }

        // POST: BookStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,BookName,AuthorName,PublisherName,Price,Category")] BookStore bookStore)
        {
            if (id != bookStore.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookStoreExists(bookStore.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookStore);
        }

        // GET: BookStores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookStores == null)
            {
                return NotFound();
            }

            var bookStore = await _context.BookStores
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (bookStore == null)
            {
                return NotFound();
            }

            return View(bookStore);
        }

        // POST: BookStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookStores == null)
            {
                return Problem("Entity set 'BookStoreDbContext.BookStores'  is null.");
            }
            var bookStore = await _context.BookStores.FindAsync(id);
            if (bookStore != null)
            {
                _context.BookStores.Remove(bookStore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookStoreExists(int id)
        {
          return (_context.BookStores?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
