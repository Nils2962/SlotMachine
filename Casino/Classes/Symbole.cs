using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Casino.Classes
{
    public class Symbole
    {
        public ImageSource ImageSource { get => imageSource; set { imageSource = value; } }
        public int ID { get => iD; set => iD = value; }
        public int Value { get => value; set => this.value = value; }

        private ImageSource imageSource;
        private int iD;
        private int value;
    }
}
