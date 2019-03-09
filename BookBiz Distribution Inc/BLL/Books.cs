using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBiz_Distribution_Inc.BLL
{
    public class Books
    {
        public int ISBN { get; set; }
        public string title { get; set; }
        public decimal unitPrice { get; set; }
        public string yearPublished {get; set;}
        public int QOH { get; set; }
        public string publisher { get; set; }
        public string author { get; set; }
    }
}
