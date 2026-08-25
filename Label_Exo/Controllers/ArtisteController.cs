using Label_Exo.Data;
using Label_Exo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Label_Exo.Controllers
{
    public class ArtisteController : Controller
    {
        private readonly LabelExoDbContext _context;

        public ArtisteController(LabelExoDbContext context)
        {
            _context = context;
        }

        // GET: Artiste
        public async Task<IActionResult> Index()
        {
            var artistes = await _context.Artistes
                .Include(artiste => artiste.Label)
                .ToListAsync();

            return View(artistes);
        }

        // GET: Artiste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artiste = await _context.Artistes
                .Include(artiste => artiste.Label)
                .Include(artiste => artiste.Albums)
                .Include(artiste => artiste.Membres)
                .FirstOrDefaultAsync(artiste => artiste.Id == id);

            if (artiste == null)
            {
                return NotFound();
            }

            return View(artiste);
        }

        // GET: Artiste/Create
        public async Task<IActionResult> Create()
        {
            await LoadLabels();

            return View();
        }

        // POST: Artiste/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NomScenique,StyleMusical,LabelId")] Artiste artiste,
            string? anneeSignature)
        {
            if (!string.IsNullOrWhiteSpace(anneeSignature))
            {
                if (int.TryParse(anneeSignature, out int annee))
                {
                    if (annee >= 1900 && annee <= 2100)
                    {
                        artiste.DateSignature = new DateTime(annee, 1, 1);
                    }
                    else
                    {
                        ModelState.AddModelError(
                            "anneeSignature",
                            "L'année doit être comprise entre 1900 et 2100."
                        );
                    }
                }
                else
                {
                    ModelState.AddModelError(
                        "anneeSignature",
                        "L'année doit être un nombre valide."
                    );
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadLabels();

                return View(artiste);
            }

            _context.Artistes.Add(artiste);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Artiste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artiste = await _context.Artistes.FindAsync(id);

            if (artiste == null)
            {
                return NotFound();
            }

            await LoadLabels();

            return View(artiste);
        }

        // POST: Artiste/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,NomScenique,StyleMusical,LabelId")] Artiste artiste,
            string? anneeSignature)
        {
            if (id != artiste.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(anneeSignature))
            {
                if (int.TryParse(anneeSignature, out int annee))
                {
                    if (annee >= 1900 && annee <= 2100)
                    {
                        artiste.DateSignature = new DateTime(annee, 1, 1);
                    }
                    else
                    {
                        ModelState.AddModelError(
                            "anneeSignature",
                            "L'année doit être comprise entre 1900 et 2100."
                        );
                    }
                }
                else
                {
                    ModelState.AddModelError(
                        "anneeSignature",
                        "L'année doit être un nombre valide."
                    );
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadLabels();

                return View(artiste);
            }

            try
            {
                _context.Artistes.Update(artiste);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtisteExists(artiste.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Artiste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artiste = await _context.Artistes
                .Include(artiste => artiste.Label)
                .FirstOrDefaultAsync(artiste => artiste.Id == id);

            if (artiste == null)
            {
                return NotFound();
            }

            return View(artiste);
        }

        // POST: Artiste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artiste = await _context.Artistes.FindAsync(id);

            if (artiste != null)
            {
                _context.Artistes.Remove(artiste);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ArtisteExists(int id)
        {
            return _context.Artistes.Any(e => e.Id == id);
        }

        private async Task LoadLabels()
        {
            ViewBag.Labels = new SelectList(
                await _context.MusicLabels
                    .OrderBy(label => label.Nom)
                    .ToListAsync(),
                "Id",
                "Nom"
            );
        }
    }
}