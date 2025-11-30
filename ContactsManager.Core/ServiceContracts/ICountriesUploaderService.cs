namespace ContactsManager.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesUploaderService
    {
        Task<int> UploadCountriesFromExcelStreamAsync(Stream excelStream);
    }
}
