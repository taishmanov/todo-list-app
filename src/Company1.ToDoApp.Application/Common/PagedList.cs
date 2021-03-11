using System;
using System.Collections.Generic;
using System.Text;

namespace Company1.ToDoApp.Application.Common
{
    public class PagedList<T>
    {
        public int PageNumber { get; private set; }
        public int PageCount { get; private set; }
        public int PageSize { get; private set; }
        public int? TotalItems { get; private set; }
        public ICollection<T> Items { get; private set; }

        public PagedList(
            ICollection<T> items,
            int pageNumber = 1,
            int pageCount = 1,
            int? totalItems = null)
        {
            if (items is null) throw new ArgumentNullException(nameof(items));

            Items = items;
            PageSize = items.Count;
            PageNumber = pageNumber;
            PageCount = pageCount;
            TotalItems = totalItems;
        }        
    }
}
