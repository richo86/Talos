using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Utilities
{
    public static class Helper
    {
        public static bool IsGuid(string id)
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

        public static bool checkBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}
