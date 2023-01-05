using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migyston_web_api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}