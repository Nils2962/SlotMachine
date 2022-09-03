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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino.UserControls
{
    /// <summary>
    /// Interaktionslogik für GamePreviewControl.xaml
    /// </summary>
    public partial class GamePreviewControl : UserControl
    {
        public ImageSource ImageSource { get => imageSource; set { imageSource = value; this.image.Source = value; } }
        public string Title { get => title; set { title = value; this.textBlockTitle.Text = value; } }
        public int ID { get => id; set => id = value; }

        private ImageSource imageSource;
        private string title;
        private int id;

        public GamePreviewControl()
        {
            this.InitializeComponent();
        }
    }
}
