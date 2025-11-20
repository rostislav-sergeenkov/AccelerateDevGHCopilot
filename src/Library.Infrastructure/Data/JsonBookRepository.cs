using Library.ApplicationCore.Entities;
using Library.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ApplicationCore.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> SearchBooks(string title);
    }
}

namespace Library.Infrastructure.Data
{
    public class JsonBookRepository : IBookRepository
    {
        private readonly JsonData _jsonData;

        public JsonBookRepository(JsonData jsonData)
        {
            _jsonData = jsonData;
        }

        public async Task<List<Book>> SearchBooks(string title)
        {
            await _jsonData.EnsureDataLoaded();

            var matchingBooks = _jsonData.Books!
                .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return matchingBooks;
        }
    }
}
