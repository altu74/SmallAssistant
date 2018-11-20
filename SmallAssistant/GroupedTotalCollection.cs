using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace SmallAssistant
{
    public class GroupedTotalCollection<K, S, T> : ObservableCollection<T>
    {
        public K Name { get; private set; }
        public S Total { get; set; }

        public GroupedTotalCollection(K name, S total, IEnumerable<T> items)
        {
            Name = name;
            Total = Total;
            items.ForEach(i => Items.Add(i));
        }
    }
}