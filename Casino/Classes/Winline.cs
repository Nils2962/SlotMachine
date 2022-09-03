using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class Winline
    {
        public int ID { get => iD; }
        public int[] LineIndexs { get => lineIndexs; set => lineIndexs = value; }

        private int iD;
        private int[] lineIndexs;

        public Winline(int id, int[] lineIndexs)
        {
            this.iD = id;
            this.lineIndexs = lineIndexs;
        }
    }
}
