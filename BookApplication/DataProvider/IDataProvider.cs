using System.Collections.Generic;

namespace BookApplication
{
    /// <summary>
    /// Interface for the data provider
    /// </summary>
    public interface IDataProvider
    {
        List<Book> GetBooks();
    }
}