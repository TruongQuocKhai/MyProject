using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonModels.ADO
{
    public class OrderDetailADO
    {
        MyProjectDb db = null;
        public OrderDetailADO()
        {
            db = new MyProjectDb();
        }

        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}