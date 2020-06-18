using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication
{
    public class DataProvider : IDataProvider
    {
        private const string JsonFilePathPath = "BookList.json";

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <returns>List&lt;Book&gt;.</returns>
        List<Book> IDataProvider.GetBooks()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", JsonFilePathPath);
            List<Book> books = new List<Book>();
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                books = JsonConvert.DeserializeObject<List<Book>>(json);
            }

            return books;
        }
    }
}