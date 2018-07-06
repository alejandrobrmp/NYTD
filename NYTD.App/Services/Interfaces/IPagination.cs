using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Services.Interfaces
{
    public abstract class IPagination<T, R>
    {
        protected R LastRequest;

        protected async Task<List<T>> LoadItems()
        {
            (IList<T>, R) result = await Load();
            LastRequest = result.Item2;
            return new List<T>(result.Item1);
        }
        protected abstract Task<(IList<T>, R)> Load();

        /// <summary>
        /// If method is overwritten, make sure you call parent
        /// </summary>
        protected void ResetRequest()
        {
            LastRequest = default(R);
        }
    }
}
