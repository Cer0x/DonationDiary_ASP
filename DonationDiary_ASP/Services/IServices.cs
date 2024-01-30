using DonationDiary_ASP.Models;
using DonationDiary_ASP.Views.ViewModels;

namespace DonationDiary_ASP.Services
{
    public interface IServices
    {
        Task<IEnumerable<Entry>> GetAllAsync();
        Task AddAsync(EntryViewModel t, int userId);
        Task<Entry> GetByIdAsync(int id);
        Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(int id);
        //Task AddAsync(EntryViewModel t);
        Task UpdateAsync(EntryViewModel newt);
        Task DeleteAsync(int id);
        
        //Task<EntryViewModel> EntryViewModel();
    }
}
