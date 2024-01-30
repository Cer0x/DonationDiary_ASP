using DonationDiary_ASP.Data;
using DonationDiary_ASP.Models;
using DonationDiary_ASP.Views.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DonationDiary_ASP.Services
{
    public class EntryService : IServices
    { 
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public EntryService(ApplicationDbContext context) 
        {
            _context = context;

        }
        public async Task AddAsync(EntryViewModel t, int userId)
        {
            //var userId = _userManager.GetUserId;


            // Tworzenie nowego wpisu
            var entryToAdd = new Entry()
            {
                BloodAmount = t.BloodAmount,
                DateOfDonation = t.DateOfDonation,
                Comment = t.Comment
            };

            
            await _context.Entry.AddAsync(entryToAdd);
            await _context.SaveChangesAsync();

            // Przypisywanie wpisu do użytkownika
            var userEntry = new user_entries
            {
                UserId = userId,
                EntryId = entryToAdd.Id
            };

            await _context.User_entries.AddAsync(userEntry);
            await _context.SaveChangesAsync();


            //var newEntry = new user_entries() { 
            //Entry = new Entry()
            //{
            //    BloodAmount = t.BloodAmount,
            //    DateOfDonation = t.DateOfDonation,
            //    Comment = t.Comment

            //}, 

            //};


            //var entryToAdd = new Entry()
            //{
            //    BloodAmount = t.BloodAmount,
            //    DateOfDonation = t.DateOfDonation,
            //    Comment = t.Comment

            //};
            //await _context.Entry.AddAsync(entryToAdd);
            //await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Entry.FirstOrDefaultAsync(x => x.Id == id);
            _context.Entry.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entry>> GetAllAsync()
        {
            var result = await _context.Entry.ToListAsync();
            return result;
        }

        public async Task<Entry> GetByIdAsync(int id)
        {
            var result = await _context.Entry.FirstOrDefaultAsync(x =>x.Id == id);
            return result;
        }

        public async Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(int userId)
        {
            var result = await _context.User_entries
                                    .Where(ue => ue.UserId == userId)
                                    .Select(ue => ue.Entry)
                                    .ToListAsync();
            return result;
        }

        public async Task UpdateAsync(EntryViewModel newt)
        {
            _context.Update(newt);
            await _context.SaveChangesAsync();
        }
    }
}
