using System;
using System.Collections.Generic;
using System.Text;

namespace BasicXamarin.Shared
{
    public class OrderElementDescription
    {
        public OrderElementDescription(string property, bool isAscending)
        {
            Property = property;
            IsAscending = isAscending;
        }
        public bool IsAscending { get; set; }
        public string Property { get; set; }
    }
}
