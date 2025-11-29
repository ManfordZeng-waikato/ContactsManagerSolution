using Entities;
using OfficeOpenXml;
using RepositoryContract;
using ServiceContracts;

namespace Services
{
    /// <summary>
    /// Service for importing countries from an Excel file.
    /// This implementation does NOT depend on ASP.NET Core.
    /// It works with a simple Stream so that this service can be reused
    /// in console apps, background workers, or Web API without tight coupling.
    /// </summary>
    public class CountriesUploaderService : ICountriesUploaderService
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesUploaderService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        /// <summary>
        /// Reads country names from an Excel stream and inserts new countries into the database.
        /// Skips records if they already exist.
        /// </summary>
        /// <param name="excelStream">The Excel file stream.</param>
        /// <returns>The number of countries inserted.</returns>
        public async Task<int> UploadCountriesFromExcelStreamAsync(Stream excelStream)
        {
            if (excelStream == null)
                throw new ArgumentNullException(nameof(excelStream));

            // Ensure the stream starts from the beginning
            if (excelStream.CanSeek)
                excelStream.Position = 0;

            int countriesInserted = 0;

            using (var package = new ExcelPackage(excelStream))
            {
                // Try to get worksheet named "Countries"
                var worksheet = package.Workbook.Worksheets["Countries"];

                // If not found, fall back to the first worksheet
                if (worksheet == null && package.Workbook.Worksheets.Count > 0)
                {
                    worksheet = package.Workbook.Worksheets[0];
                }

                if (worksheet == null)
                    throw new InvalidOperationException("No worksheet found in the Excel file.");

                int rowCount = worksheet.Dimension.Rows;

                // Start from row 2 (assuming row 1 is the header)
                for (int row = 2; row <= rowCount; row++)
                {
                    var cellValue = worksheet.Cells[row, 1].Value;
                    string? countryName = Convert.ToString(cellValue)?.Trim();

                    // Skip empty cells
                    if (string.IsNullOrEmpty(countryName))
                        continue;

                    // Check if the country already exists
                    var existingCountry = await _countriesRepository.GetCountryByCountryName(countryName);
                    if (existingCountry != null)
                        continue;

                    // Insert new country
                    var country = new Country
                    {
                        CountryName = countryName
                    };

                    await _countriesRepository.AddCountry(country);
                    countriesInserted++;
                }
            }

            return countriesInserted;
        }
    }
}
