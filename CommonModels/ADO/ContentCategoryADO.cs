using CommonModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModels.ADO
{
    public class ContentCategoryADO
    {
        MyProjectDb db = null;
        public ContentCategoryADO()
        {
            db = new MyProjectDb();
        }

        public List<ContentCategory>GetContentCategory()
        {
            return db.ContentCategories.ToList();
        }
    }
}
