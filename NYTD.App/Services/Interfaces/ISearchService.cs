using NYTD.App.Models;
using NYTD.App.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Interfaces
{
    public interface ISearchService
    {
        void SetSearchQuery(string query);
        Task<SearchResponse> GetResults();
        void Reset();
    }
}
