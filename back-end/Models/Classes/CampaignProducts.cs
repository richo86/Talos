using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class CampaignProducts
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public Guid ProductId { get; set; }
    }
}
