using System;
using System.Collections.Generic;
using System.Linq;

namespace FreshTask
{
    public partial class Default : System.Web.UI.Page
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                List<Item> items = new List<Item>();

                List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("Quantity")).ToList();

                foreach (string key in keys)
                {
                    string rowIndex = key.Replace("Quantity", "");
                    Item item = new Item()
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Item {rowIndex}",
                        Quntity = Request.Form[key],
                        UnitPrice = Request.Form[$"UnitPrice{rowIndex}"]
                    };
                    items.Add(item);
                }

                if (items.Count > 0)
                {
                    Invoice invoice = new Invoice()
                    {
                        Id = Guid.NewGuid(),
                        Items = items,
                        Total = Request.Form[$"net-value"]
                    };

                    dbContext.Invoices.Add(invoice);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}