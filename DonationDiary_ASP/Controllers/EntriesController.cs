using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonationDiary_ASP.Data;
using DonationDiary_ASP.Models;
using DonationDiary_ASP.Services;
using DonationDiary_ASP.Views.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DonationDiary_ASP.Controllers
{
    public class EntriesController : Controller
    {
        private readonly IServices _service;
        private readonly UserManager<User> _userManager;

        public EntriesController(IServices service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }


        // GET: Entries
        public async Task<IActionResult> Index()
        {
            var userIdAsString = _userManager.GetUserId(User);
            if (int.TryParse(userIdAsString, out int userId))
            {
                var data = await _service.GetEntriesByUserIdAsync(userId);
                return View(data);

            }
            return View();
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntryViewModel entry)
        {
            var userIdAsString = _userManager.GetUserId(User);

            if (int.TryParse(userIdAsString, out int userId))
            {
                // userId jest teraz liczbą całkowitą i możesz go użyć
                await _service.AddAsync(entry, userId);
                return RedirectToAction(nameof(Index));
            }
                return View(entry);
          
        }

        // GET: Entries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return View();
            }

            return View(result);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EntryViewModel entry)
        {
           if (!ModelState.IsValid)
            {
                return View(entry);
            }

            await _service.UpdateAsync(entry);
            return RedirectToAction(nameof(Index));
        }

        // GET: Entries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return View();
            }

            return View(result);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return View();
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
