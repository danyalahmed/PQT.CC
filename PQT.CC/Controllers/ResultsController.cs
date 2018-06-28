using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PQT.CC.Data;
using System.Threading.Tasks;

namespace PQT.CC.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            return View(await _context.Results
                .Include(r => r.Applicant)
                .Include(r => r.Card)
                .ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var results = await _context.Results
                .Include(r => r.Applicant)
                .Include(r => r.Card)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (results == null)
            {
                return NotFound();
            }

            return View(results);
        }      
    }
}
