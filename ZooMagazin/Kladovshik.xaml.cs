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
using System.Windows.Shapes;

namespace ZooMagazin
{
    /// <summary>
    /// Логика взаимодействия для Kladovshik.xaml
    /// </summary>
    public partial class Kladovshik : Window
    {
        public Kladovshik()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tovar registration = new Tovar();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Spravoshnik registration = new Spravoshnik();
            registration.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow registration = new MainWindow();
            registration.Show();
            this.Close();
        }
    }
}
