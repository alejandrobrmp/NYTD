using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ObservableCollectionExtensions
{
    public static void AddAll(this ObservableCollection<object> collection, IEnumerable<object> list)
    {
        foreach (object item in list)
        {
            collection.Add(item);
        }
    }
}
