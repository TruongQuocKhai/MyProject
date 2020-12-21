using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonModels.ADO
{
    public class OrderADO
    {
        MyProjectDb db = null;
        public OrderADO()
        {
            db = new MyProjectDb();
        }

        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}