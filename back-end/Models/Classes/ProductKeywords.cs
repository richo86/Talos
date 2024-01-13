using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class ProductKeywords
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Keyword { get; set; }
    }
}
