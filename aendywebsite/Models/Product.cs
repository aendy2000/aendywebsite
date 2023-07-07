namespace aendywebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public partial class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string LinkOrder { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public string Images { get; set; }
        public int ID_Categories { get; set; }
    }
}