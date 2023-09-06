using System.Collections.Generic;

namespace FreshTask
{
    public class Invoice : BaseEntity
    {
        public virtual ICollection<Item> Items { get; set; }
        public string Total { get; set; }
    }
}