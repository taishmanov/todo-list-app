using System;
using System.Collections.Generic;
using System.Text;

namespace Company1.ToDoApp.Application
{
    public class InMemoryItemsCollection<T>
    {
        public readonly object Lock = new object();

        public readonly IDictionary<string, T> Items;

        public InMemoryItemsCollection(IDictionary<string, T> items)
        {
            Items = items;
        }
    }
}
