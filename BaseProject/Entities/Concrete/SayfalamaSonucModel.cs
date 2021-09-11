using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Entities.Concrete
{
    public class SayfalamaSonucModel<T>
    {
        public int Sayfa { get; set; }
        public int ToplamVeriAdet { get; set; }
        public int FiltrelenmisVeriAdet { get; set; }
        public List<T> Veri { get; set; }
    }
}
