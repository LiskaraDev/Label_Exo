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
    }
}