using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using Business.Services;

namespace SundayTask.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeaturesController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService service)
        {
            _featureService = service;
        }

        // GET: Admin/Features
        public async Task<IActionResult> Index()
        {
            List<Feature> features = await _featureService.GetAll();
            return View(features);
        }

        // GET: Admin/Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Feature feature = await _featureService.Get(id);

            return View(feature);
        }

        // GET: Admin/Features/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feature feature)
        {
            if (ModelState.IsValid)
            {
                await _featureService.Create(feature);
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        public async Task<IActionResult> Update(int? id)
        {
            Feature feature = await _featureService.Get(id);
            return View(feature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Feature feature)
        {
            if (id != feature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _featureService.Update(feature);
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            Feature feature = await _featureService.Get(id);

            return View(feature);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
