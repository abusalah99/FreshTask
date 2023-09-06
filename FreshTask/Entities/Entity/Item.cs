using System;

namespace FreshTask
{
    public class Item : BaseEntitySetting
    {
        public string Quntity { get; set; }
        public string UnitPrice { get; set; }
        public virtual Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; } 
    }
}