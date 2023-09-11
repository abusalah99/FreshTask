﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FreshTask
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly IInvoiceUnitOfWork _unitOfWork = new InvoiceUnitOfWork();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                List<Item> items = new List<Item>();

                if (Request.Form[$"net-value"] != string.Empty)
                {
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

                    Invoice invoice = new Invoice()
                    {
                        Items = items,
                        Total = Request.Form[$"net-value"]
                    };

                    await _unitOfWork.Create(invoice);
                }
            }
        }
    }
}