using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySystem.DAL.Models
{
    public class Products
    {
        public int Id { get; set; }
        private string Name { get; set; }

        private ProductType Type { get; set; }
    }
}
