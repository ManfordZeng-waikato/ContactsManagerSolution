using ContacsManager.Infrastructure.DatabaseContext;
using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ContacsManager.Infrastructure.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Country> AddCountry(Country country)
        {
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryByCountryID(Guid countryID)
        {
            return await _db.Countries.FirstOrDefaultAsync(c =>
            c.CountryID == countryID);
        }

        public Task<Country?> GetCountryByCountryName(string countryName)
        {
            return _db.Countries.FirstOrDefaultAsync(c =>
            c.CountryName == countryName);
        }
    }
}
