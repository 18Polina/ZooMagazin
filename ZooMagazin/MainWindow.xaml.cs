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
using System.Data;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZooMagazin.DataSet1TableAdapters;

namespace ZooMagazin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet1 dataSet;
        PersonalsTableAdapter personalsTable;

        public MainWindow()
        {
            InitializeComponent();
            dataSet = new DataSet1();
            personalsTable = new PersonalsTableAdapter();

            personalsTable.Fill(dataSet.Personals);

            DataGrid.ItemsSource = dataSet.Personals.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Personal";
            DataGrid.CanUserAddRows = false;
            DataGrid.CanUserDeleteRows = false;
            DataGrid.IsReadOnly = true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count = dataSet.Personals.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRowView dataRowView = (DataRowView)DataGrid.Items[index: i];

                string login, pass;
                int c;
                login = dataRowView.Row.Field<String>("Login");
                pass = dataRowView.Row.Field<String>("Password");
                c = dataRowView.Row.Field<int>("Role_ID");

                if (LoginTx.Text == login && PassworTx.Password  == pass)
                {

                    if (c == 1)
                    {
                        Administrator registration = new Administrator();
                        registration.Show();
                        this.Close();
                    }
                    else if (c == 2)
                    {
                        Kladovshik registration = new Kladovshik();
                        registration.Show();
                        this.Close();
                    }

                    else if (c == 3)
                    {
                        Buhgalter registration = new Buhgalter();
                        registration.Show();
                        this.Close();
                    }
                    else if (c == 4)
                    {
                        Kadrovik registration = new Kadrovik();
                        registration.Show();
                        this.Close();
                    }
                    else if (c == 5)
                    {
                        Menedjer_po_nedv registration = new Menedjer_po_nedv();
                        registration.Show();
                        this.Close();
                    }
                    else if (c == 6)
                    {
                        Menedjer_po_postavki registration = new Menedjer_po_postavki();
                        registration.Show();
                        this.Close();
                    }
                }

            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
