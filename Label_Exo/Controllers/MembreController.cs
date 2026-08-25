
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Label_Exo.Models;
using Label_Exo.Data;

public class MembreController : Controller
{
    private readonly LabelExoDbContext _context;

    public MembreController(LabelExoDbContext context)
    {
        _context = context;
    }

    // GET: MEMBRES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Membres.ToListAsync());
    }

    // GET: MEMBRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membre = await _context.Membres
            .FirstOrDefaultAsync(m => m.Id == id);
        if (membre == null)
        {
            return NotFound();
        }

        return View(membre);
    }

    // GET: MEMBRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MEMBRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Instrument,DateNaissance,ArtisteId,Artiste")] Membre membre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(membre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(membre);
    }

    // GET: MEMBRES/Edit/5
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
        return View(membre);
    }

    // POST: MEMBRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nom,Prenom,Instrument,DateNaissance,ArtisteId,Artiste")] Membre membre)
    {
        if (id != membre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(membre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembreExists(membre.Id))
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
        return View(membre);
    }

    // GET: MEMBRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var membre = await _context.Membres
            .FirstOrDefaultAsync(m => m.Id == id);
        if (membre == null)
        {
            return NotFound();
        }

        return View(membre);
    }

    // POST: MEMBRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var membre = await _context.Membres.FindAsync(id);
        if (membre != null)
        {
            _context.Membres.Remove(membre);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MembreExists(int? id)
    {
        return _context.Membres.Any(e => e.Id == id);
    }
}
