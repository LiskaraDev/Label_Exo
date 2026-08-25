
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Label_Exo.Models;
using Label_Exo.Data;

public class PisteController : Controller
{
    private readonly LabelExoDbContext _context;

    public PisteController(LabelExoDbContext context)
    {
        _context = context;
    }

    // GET: PISTES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Pistes.ToListAsync());
    }

    // GET: PISTES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var piste = await _context.Pistes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (piste == null)
        {
            return NotFound();
        }

        return View(piste);
    }

    // GET: PISTES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PISTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titre,DureeSecondes,AlbumId,Album")] Piste piste)
    {
        if (ModelState.IsValid)
        {
            _context.Add(piste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(piste);
    }

    // GET: PISTES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var piste = await _context.Pistes.FindAsync(id);
        if (piste == null)
        {
            return NotFound();
        }
        return View(piste);
    }

    // POST: PISTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Titre,DureeSecondes,AlbumId,Album")] Piste piste)
    {
        if (id != piste.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(piste);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PisteExists(piste.Id))
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
        return View(piste);
    }

    // GET: PISTES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var piste = await _context.Pistes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (piste == null)
        {
            return NotFound();
        }

        return View(piste);
    }

    // POST: PISTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var piste = await _context.Pistes.FindAsync(id);
        if (piste != null)
        {
            _context.Pistes.Remove(piste);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PisteExists(int? id)
    {
        return _context.Pistes.Any(e => e.Id == id);
    }
}
