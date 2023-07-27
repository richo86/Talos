using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Utilities
{
    public class Helper
    {
        public bool IsGuid(string id)
        {
            try
            {
                var test = Guid.Parse(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
