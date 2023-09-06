using System;
using System.Collections.Generic;

namespace FreshTask
{
    public partial class Default : System.Web.UI.Page
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e) { }

        protected void SaveBillButton_Click(object sender, EventArgs e)
        {
            List<Item> items = new List<Item>();

            if (Quantity1.Text != null && !string.IsNullOrWhiteSpace(Quantity1.Text))
            {
                items.Add(new Item()
                {
                    Id = Guid.NewGuid(),
                    Name = Item1.Text,
                    Quntity = Quantity1.Text,
                    UnitPrice = Price1.Text
                });
            }

            if (Quantity2.Text != null && !string.IsNullOrWhiteSpace(Quantity2.Text))
            {
                items.Add(new Item()
                {
                    Id = Guid.NewGuid(),
                    Name = Item2.Text,
                    Quntity = Quantity2.Text,
                    UnitPrice = Price2.Text
                });
            }

            if (Quantity3.Text != null && !string.IsNullOrWhiteSpace(Quantity3.Text))
            {
                items.Add(new Item()
                {
                    Id = Guid.NewGuid(),
                    Name = Item3.Text,
                    Quntity = Quantity3.Text,
                    UnitPrice = Price3.Text
                });
            }

            Invoice invoice = new Invoice()
            {
                Id = Guid.NewGuid(),
                Items = items,
                Total = CalculateToTal(items)
            };

            dbContext.Invoices.Add(invoice);
            dbContext.SaveChanges();
        }

        private string CalculateToTal(List<Item> items)
        {
            float Total = 0;

            foreach (Item item in items)
                Total += (float.Parse(item.Quntity) * float.Parse(item.UnitPrice));

            return Total.ToString("F3");
        }

    }
}