using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _180301_001_ExtendedObservableCollection.Collections;

namespace _180301_001_ExtendedObservableCollection
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var collection = new ExtendedObservableCollection<string>();
            collection.CollectionChanged += Collection_CollectionChanged;

            collection.Add("item1");
            collection.AddRange(new string[] { "item2", "item3", "item4" });
            collection[0] = "item1 mk2";
            collection.Move(0, 3);
            collection.Remove("item3");
            collection.Clear();
        }

        private static void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e is ExtendedObservableCollectionChangedEventArgs)
            {
                var param = (ExtendedObservableCollectionChangedEventArgs)e;
                Console.WriteLine("Cleared!");
                Console.WriteLine("{0} items removed!", param.PreviousItems.Count);
            }
            else
                Console.WriteLine("Changed!");
        }
    }
}
