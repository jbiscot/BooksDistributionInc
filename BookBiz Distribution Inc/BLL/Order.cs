using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookBiz_Distribution_Inc.BLL
{
    public class Order
    {
        public int OrdNumber { get; set; }
        public Employee OrdEmployee { get; set; }
        public Clients OrdClient { get; set; }
        public Books OrdProduct { get; set; }
        public int OrdQuantity { get; set; }
        public decimal OrdTotal { get; set; }
        public DateTime OrdDate { get; set; }

    }
}
