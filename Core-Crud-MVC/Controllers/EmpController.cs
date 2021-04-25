using Core_Crud_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Crud_MVC.Controllers
{
    public class EmpController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var displayData = _db.EmployeeTable.ToList();

            return View(displayData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewEmpClass nec)
        {
            if (ModelState.IsValid)
            {
                _db.Add(nec);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nec);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var results = await _db.EmployeeTable.FindAsync(id);

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewEmpClass nec)
        {
            if (ModelState.IsValid)
            {
                _db.Update(nec);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(nec);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var results = await _db.EmployeeTable.FindAsync(id);

            return View(results);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var results = await _db.EmployeeTable.FindAsync(id);

            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var results = await _db.EmployeeTable.FindAsync(id);

            _db.EmployeeTable.Remove(results);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
