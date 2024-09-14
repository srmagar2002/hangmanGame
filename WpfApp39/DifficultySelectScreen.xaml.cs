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

namespace WpfApp39
{
    /// <summary>
    /// Interaction logic for DifficultySelectScreen.xaml
    /// </summary>
    public partial class DifficultySelectScreen : UserControl
    {

        public delegate void ButtonClickedEventHandler(object sender, string buttonContent);
        public event ButtonClickedEventHandler ButtonClicked;
        public DifficultySelectScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var content = button?.Content.ToString();

            ButtonClicked?.Invoke(button, content);
        }
    }
}
