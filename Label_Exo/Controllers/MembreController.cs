using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Label_Exo.Models;
using Label_Exo.Data;
using System.Globalization;

namespace Label_Exo.Controllers
{
    public class MembreController : Controller
    {
        private readonly LabelExoDbContext _context;

        public MembreController(LabelExoDbContext context)
        {
            _context = context;
        }

        // GET: Membre
        public async Task<IActionResult> Index()
        {
            var membres = await _context.Membres
                .Include(membre => membre.Artiste)
                .ToListAsync();

            return View(membres);
        }

        // GET: Membre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .Include(membre => membre.Artiste)
                .FirstOrDefaultAsync(membre => membre.Id == id);

            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // GET: Membre/Create
        public async Task<IActionResult> Create()
        {
            await LoadArtistes();

            return View();
        }

        // POST: Membre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nom,Prenom,Instrument,ArtisteId")] Membre membre,
            string? dateNaissance)
        {
            if (!string.IsNullOrWhiteSpace(dateNaissance))
            {
                if (DateTime.TryParseExact(
                    dateNaissance,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
                {
                    membre.DateNaissance = date;
                }
                else
                {
                    ModelState.AddModelError(
                        "dateNaissance",
                        "La date doit être au format JJ/MM/AAAA."
                    );
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadArtistes();

                return View(membre);
            }

            _context.Membres.Add(membre);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Membre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres.FindAsync(id);

            if (membre == null)
            {
                return NotFound();
            }

            await LoadArtistes();

            return View(membre);
        }

        // POST: Membre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nom,Prenom,Instrument,ArtisteId")] Membre membre,
            string? dateNaissance)
        {
            if (id != membre.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(dateNaissance))
            {
                if (DateTime.TryParseExact(
                    dateNaissance,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
                {
                    membre.DateNaissance = date;
                }
                else
                {
                    ModelState.AddModelError(
                        "dateNaissance",
                        "La date doit être au format JJ/MM/AAAA."
                    );
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadArtistes();

                return View(membre);
            }

            try
            {
                _context.Membres.Update(membre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembreExists(membre.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Membre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .Include(membre => membre.Artiste)
                .FirstOrDefaultAsync(membre => membre.Id == id);

            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // POST: Membre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membre = await _context.Membres.FindAsync(id);

            if (membre != null)
            {
                _context.Membres.Remove(membre);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MembreExists(int id)
        {
            return _context.Membres.Any(e => e.Id == id);
        }

        // Charge les artistes/groupes pour les listes déroulantes
        private async Task LoadArtistes()
        {
            ViewBag.Artistes = new SelectList(
                await _context.Artistes
                    .OrderBy(a => a.NomScenique)
                    .ToListAsync(),
                "Id",
                "NomScenique"
            );
        }
    }
}