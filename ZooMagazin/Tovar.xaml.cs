using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZooMagazin.DataSet1TableAdapters;

namespace ZooMagazin
{
    /// <summary>
    /// Логика взаимодействия для Tovar.xaml
    /// </summary>
    public partial class Tovar : Window
    {
        DataSet1 dataSet;
        ProductTableAdapter productTableAdapter;
        Delivery_invoiceTableAdapter delivery_InvoiceTableAdapter;
        Product_ReferenceTableAdapter product_ReferenceTableAdapter;
        View_ProductTableAdapter View_ProductTableAdapter;


        public Tovar()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            productTableAdapter = new ProductTableAdapter();
            View_ProductTableAdapter = new View_ProductTableAdapter();
            delivery_InvoiceTableAdapter = new Delivery_invoiceTableAdapter();
            product_ReferenceTableAdapter = new Product_ReferenceTableAdapter();

            productTableAdapter.Fill(dataSet.Product);
            delivery_InvoiceTableAdapter.Fill(dataSet.Delivery_invoice);
            product_ReferenceTableAdapter.Fill(dataSet.Product_Reference);

            View_ProductTableAdapter.Fill(dataSet.View_Product);

            CBNSpravochnik.ItemsSource = dataSet.Product_Reference.DefaultView;
            CBNSpravochnik.DisplayMemberPath = "Name";
            CBNSpravochnik.SelectedValuePath = "ID_Product_Reference";
            CBNSpravochnik.SelectedIndex = 0;

            CBDogovor.ItemsSource = dataSet.Delivery_invoice.DefaultView;
            CBDogovor.DisplayMemberPath = "Name_Contr";
            CBDogovor.SelectedValuePath = "ID_Delivery_invoice";
            CBDogovor.SelectedIndex = 0;


            DataGrid.ItemsSource = dataSet.View_Product.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Product";
            DataGrid.CanUserAddRows = false;
            DataGrid.CanUserDeleteRows = false;
            DataGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string index = Poisk.Text;

            if (String.IsNullOrWhiteSpace(index))
            {
                return;
            }
            else
            {
                for (int i = 0; i < dataSet.View_Product.Rows.Count; i++)
                {

                    if (dataSet.View_Product.Rows[i][1].ToString().Contains(index))
                    {
                        DataGrid.SelectedIndex = i;
                        return;
                    }

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Kladovshik registration = new Kladovshik();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(NameTovar.Text, "^[А-Я][а-яА-Я]*$").Success;


            if (!NameTovar.Text.Equals("") && b)
            {
                productTableAdapter.Insert(NameTovar.Text, (int)CBNSpravochnik.SelectedValue, (int)CBDogovor.SelectedValue);
                View_ProductTableAdapter.Fill(dataSet.View_Product);

                NameTovar.Text = "";
            }
            else
            {
                MessageBox.Show("Поле название товара введено неккоректно");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(NameTovar.Text, "^[А-Я][а-яА-Я]*$").Success;

            if (!NameTovar.Text.Equals("") && b && DataGrid.SelectedItem != null)
            {
                productTableAdapter.UpdateQuery(NameTovar.Text, (int)CBNSpravochnik.SelectedValue, (int)CBDogovor.SelectedValue, (int)DataGrid.SelectedValue);
                View_ProductTableAdapter.Fill(dataSet.View_Product);
            }
            else
            {
                MessageBox.Show("Поле название товара введено неккоректно");
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                NameTovar.Text = dataRowView.Row.Field<String>("Название_товара");
                CBNSpravochnik.Text = dataRowView.Row.Field<String>("Название_справочника");
                CBDogovor.Text = dataRowView.Row.Field<String>("Название_организации");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                productTableAdapter.DeleteQuery((int)DataGrid.SelectedValue);
                View_ProductTableAdapter.Fill(dataSet.View_Product);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            DataGrid.Columns[2].Visibility = Visibility.Hidden;
            DataGrid.Columns[3].Visibility = Visibility.Hidden;
        }
    }
}
