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
    /// Логика взаимодействия для Menedjer_po_postavki.xaml
    /// </summary>
    public partial class Menedjer_po_postavki : Window
    {
        public Menedjer_po_postavki()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dogovor_postavki registration = new Dogovor_postavki();
            registration.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow registration = new MainWindow();
            registration.Show();
            this.Close();
        }
    }
}
