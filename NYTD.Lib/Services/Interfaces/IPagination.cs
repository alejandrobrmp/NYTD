using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.Lib.Services.Interfaces
{
    public abstract class IPagination<T, R>
    {
        public delegate void ItemsAdded(object sender, IList<T> fullList, IList<T> newItems);
        public event ItemsAdded OnItemsAdded;

        protected R LastRequest;
        public List<T> Items;

        public async void LoadItems()
        {
            (IList<T>, R) result = await Load();
            Items.AddRange(result.Item1);
            LastRequest = result.Item2;
            OnItemsAdded?.Invoke(this, Items, result.Item1);
        }
        protected abstract Task<(IList<T>, R)> Load();

    }
}
