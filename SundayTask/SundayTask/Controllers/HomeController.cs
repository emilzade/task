using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SundayTask.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SundayTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Features =await _context.Features.Where(n => !n.IsDeleted).ToListAsync();
            return View(homeVM);
        }
    }
}
