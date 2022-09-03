using Casino.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino.Games.Slots
{
    /// <summary>
    /// Interaktionslogik für Symbol.xaml
    /// </summary>
    public partial class SymbolControl : UserControl
    {
        public Symbole Symbole { get => symbole; set { symbole = value; this.image.Source = symbole.ImageSource; } }

        private Symbole symbole;

        private Storyboard rollStoryboard;

        public SymbolControl()
        {
            this.InitializeComponent();
        }
    }
}
