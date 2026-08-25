using Label_Exo.Data;
using Label_Exo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Label_Exo.Controllers
{
    public class AlbumController : Controller
    {
        private readonly LabelExoDbContext _context;

        public AlbumController(LabelExoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var albums = _context.Albums
                .Include(album => album.Artiste)
                .ToList();

            return View(albums);
        }

        public IActionResult ByArtiste(int artisteId)
        {
            var albums = _context.Albums
                .Include(album => album.Artiste)
                .Where(album => album.ArtisteId == artisteId)
                .ToList();

            return View("Index", albums);
        }

        // GET: ALBUM/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ALBUM/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Titre, DateSortie, PrixVente, ArtisteId, Artiste")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: ALBUMS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: ALBUM/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Nom,Prenom,Instrument,DateNaissance,ArtisteId,Artiste")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            return View(album);
        }

        // GET: ALBUMS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: ALBUMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int? id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}