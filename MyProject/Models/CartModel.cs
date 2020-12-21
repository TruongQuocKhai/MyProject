using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    [Serializable] // Sắp xếp tuần tự
    public class CartModel
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}