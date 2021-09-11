using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities
{
    public static class StringMetodlari
    {
        public static string ToLowerTR(this string metin)
        {
            return metin.ToLower(OzelMetodlar.TRCulture);
        }
        public static string ToUpperTR(this string metin)
        {
            return metin.ToUpper(OzelMetodlar.TRCulture);
        }
    }
}
