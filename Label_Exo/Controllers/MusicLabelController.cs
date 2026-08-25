using Label_Exo.Data;
using Label_Exo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Label_Exo.Controllers
{
    public class MusicLabelController : Controller
    {
        private readonly LabelExoDbContext _context;

        public MusicLabelController(LabelExoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var labels = _context.MusicLabels.ToList();

            return View(labels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MusicLabel label)
        {
            if (ModelState.IsValid)
            {
                _context.MusicLabels.Add(label);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(label);
        }
    }
}