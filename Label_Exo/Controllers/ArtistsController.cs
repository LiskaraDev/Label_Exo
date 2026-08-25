using AspNetCoreGeneratedDocument;
using Label_Exo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Label_Exo.Controllers
{
    public class ArtistsController : Controller
    {
        public static readonly List<Artists> _artist = new()
        {
            new Artists { Id = 1, Name = "Evanescence", Style = "Alt Metal", Started = 1994, Language ="EN", IsActive = true },
            new Artists { Id = 2, Name = "System of a Down", Style = "Metal", Started = 1995, Language ="EN", IsActive = true },
            new Artists { Id = 3, Name = "Linkin Park", Style = "Nu Metal", Started = 1996, Language ="EN", IsActive = true },
            new Artists { Id = 4, Name = "Slipknot", Style = "Nu Metal", Started = 1995, Language ="EN", IsActive = true },
            new Artists { Id = 5, Name = "Falling In Reverse", Style = "Alt Metal", Started = 2008, Language ="EN", IsActive = true },
            new Artists { Id = 6, Name = "Motionless In White", Style = "MetalCore", Started = 2005, Language ="EN", IsActive = true },
            new Artists { Id = 7, Name = "Die Antwoord", Style = "Hip-Hop-Rave", Started = 2007, Language ="EN", IsActive = true },

        };

        public IActionResult Index()
        {
            return View(_artist);
        }

        public IActionResult ByAlbum(int id)
        {
            var artist = _artist
                .Where(a => a.Id == id)
                .ToList();

            return View(_artist);
        }
    }
}
