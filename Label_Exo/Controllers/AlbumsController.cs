using Label_Exo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Label_Exo.Controllers
{
    public class AlbumsController : Controller
    {
        public static readonly List<Albums> _album = new()
        {
            new Albums { Id = 1, Name = "Fallen", ArtistId = 1, Year = 2003},
            new Albums { Id = 1, Name = "The Open Door", ArtistId = 1, Year = 2006},
            new Albums { Id = 2, Name = "Toxicity", ArtistId = 2, Year = 2001},
            new Albums { Id = 3, Name = "Meteora (Bonus Edition)", ArtistId = 3, Year = 2003},
            new Albums { Id = 3, Name = "Hybrid Theory", ArtistId = 3, Year = 2000},
            new Albums { Id = 4, Name = "Slipknot", ArtistId = 4, Year = 1999},
            new Albums { Id = 5, Name = "Popular Monster", ArtistId = 5, Year = 2024},
            new Albums { Id = 6, Name = "Decades", ArtistId = 6, Year = 2026},
            new Albums { Id = 7, Name = "Ten$ion", ArtistId = 7, Year = 2012},
        };
        public IActionResult Index()
        {
            return View(_album);
        }

        public IActionResult ByArtist(int artistId)
        {
            var album = _album
                .Where(album => album.ArtistId == artistId)
                .ToList();

            return View("Index", album);
        }
    }
}