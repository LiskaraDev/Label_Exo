using Label_Exo.Data;
using Label_Exo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Label_Exo.Controllers
{
    public class ArtisteController : Controller
    {
        private readonly LabelExoDbContext _context;

        public ArtisteController(LabelExoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var artistes = _context.Artistes
                .Include(artiste => artiste.Label)
                .ToList();

            return View(artistes);
        }

        public IActionResult Details(int id)
        {
            var artiste = _context.Artistes
                .Include(artiste => artiste.Label)
                .Include(artiste => artiste.Albums)
                .Include(artiste => artiste.Membres)
                .FirstOrDefault(artiste => artiste.Id == id);

            if (artiste == null)
            {
                return NotFound();
            }

            return View(artiste);
        }
    }
}