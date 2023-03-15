using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для Spravoshnik.xaml
    /// </summary>
    public partial class Spravoshnik : Window
    {
        DataSet1 dataSet;
        Product_ReferenceTableAdapter product_ReferenceTable;
        ManufacturerTableAdapter manufacturerTableAdapter;
        Product_typeTableAdapter product_TypeTable;
        Variety_of_animalsTableAdapter variety_Of_AnimalsTableAdapter;
        View_ProductReferenceTableAdapter view_ProductReference;

        public Spravoshnik()
        {
            InitializeComponent();

            dataSet = new DataSet1();

            product_ReferenceTable = new Product_ReferenceTableAdapter();
            manufacturerTableAdapter = new ManufacturerTableAdapter();
            product_TypeTable = new Product_typeTableAdapter();
            variety_Of_AnimalsTableAdapter = new Variety_of_animalsTableAdapter();
            view_ProductReference = new View_ProductReferenceTableAdapter();

            product_ReferenceTable.Fill(dataSet.Product_Reference);
            manufacturerTableAdapter.Fill(dataSet.Manufacturer);
            product_TypeTable.Fill(dataSet.Product_type);
            variety_Of_AnimalsTableAdapter.Fill(dataSet.Variety_of_animals);

            view_ProductReference.Fill(dataSet.View_ProductReference);

            CBProizvod.ItemsSource = dataSet.Manufacturer.DefaultView;
            CBProizvod.DisplayMemberPath = "Country";
            CBProizvod.SelectedValuePath = "ID_Manufacturer";
            CBProizvod.SelectedIndex = 0;

            CBProductType.ItemsSource = dataSet.Product_type.DefaultView;
            CBProductType.DisplayMemberPath = "Product_type";
            CBProductType.SelectedValuePath = "ID_Product_type";
            CBProductType.SelectedIndex = 0;


            CBRaznovidnost.ItemsSource = dataSet.Variety_of_animals.DefaultView;
            CBRaznovidnost.DisplayMemberPath = "Variety_of_animals";
            CBRaznovidnost.SelectedValuePath = "ID_Variety_of_animals";
            CBRaznovidnost.SelectedIndex = 0;


            DataGrid.ItemsSource = dataSet.View_ProductReference.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Product_Reference";
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
                for (int i = 0; i < dataSet.View_ProductReference.Rows.Count; i++)
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
            bool b = Regex.Match(NameProduct.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(Salary.Text, "^[0-9][0-9][0-9][ ][а-яА-Я]*$").Success;
            bool c = Regex.Match(SrokGodn.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;

           

            if (!NameProduct.Text.Equals("") && b)
            {
                if (!SrokGodn.Text.Equals("") && c)
                {
                    if (!Salary.Text.Equals("") && a)
                    {
                        product_ReferenceTable.Insert(NameProduct.Text, SrokGodn.Text,
                            (int)CBProizvod.SelectedValue, (int)CBProductType.SelectedValue, (int)CBRaznovidnost.SelectedValue, Salary.Text);
                        view_ProductReference.Fill(dataSet.View_ProductReference);

                        NameProduct.Text = "";
                        Salary.Text = "";
                        SrokGodn.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Поле стоимость введено неккоректно");
                    }

                }
                else
                {
                    MessageBox.Show("Поле срок годности введено неккоректно");
                }
            }
            else
            {
                MessageBox.Show("Поле название товара введено неккоректно");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(NameProduct.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(Salary.Text, "^[0-9][0-9][0-9][ ][а-яА-Я]*$").Success;
            bool c = Regex.Match(SrokGodn.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;



            if (!NameProduct.Text.Equals("") && b)
            {
                if (!SrokGodn.Text.Equals("") && c)
                {
                    if (!Salary.Text.Equals("") && a)
                    {
                        product_ReferenceTable.UpdateQuery(NameProduct.Text, SrokGodn.Text,
                            (int)CBProizvod.SelectedValue, (int)CBProductType.SelectedValue, (int)CBRaznovidnost.SelectedValue, Salary.Text, (int)DataGrid.SelectedValue);
                        view_ProductReference.Fill(dataSet.View_ProductReference);
                    }
                    else
                    {
                        MessageBox.Show("Поле стоимость введено неккоректно");
                    }

                }
                else
                {
                    MessageBox.Show("Поле срок годности введено неккоректно");
                }
            }
            else
            {
                MessageBox.Show("Поле название товара введено неккоректно");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                product_ReferenceTable.DeleteQuery((int)DataGrid.SelectedValue);
                view_ProductReference.Fill(dataSet.View_ProductReference);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                NameProduct.Text = dataRowView.Row.Field<String>("Название_товара");
                SrokGodn.Text = dataRowView.Row.Field<String>("Срок_годности");
                Salary.Text = dataRowView.Row.Field<String>("Стоимость");
                CBProizvod.Text = dataRowView.Row.Field<String>("Страна");
                CBProductType.Text = dataRowView.Row.Field<String>("Тип_товара");
                CBRaznovidnost.Text = dataRowView.Row.Field<String>("Разновидность_животного");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            DataGrid.Columns[3].Visibility = Visibility.Hidden;
            DataGrid.Columns[4].Visibility = Visibility.Hidden;
            DataGrid.Columns[5].Visibility = Visibility.Hidden;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = "Spravochik_tovar.txt";
                StreamWriter sw = new StreamWriter(path);
                DataTable dt_user = dataSet.View_ProductReference;
                sw.WriteLine("Справочник о товаре: " + DateTime.Now.ToString() + " на текущий месяц");
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    sw.WriteLine("Название товара: " + dt_user.Rows[i][1].ToString());
                    sw.WriteLine("Срок годности: " + dt_user.Rows[i][2].ToString());
                    sw.WriteLine("Стоимость: " + dt_user.Rows[i][6].ToString());
                    sw.WriteLine("Разновидность животного: " + dt_user.Rows[i][7].ToString());
                    sw.WriteLine("Тип товара: " + dt_user.Rows[i][8].ToString());
                    sw.WriteLine("Страна: " + dt_user.Rows[i][9].ToString());


                    sw.WriteLine("");
                }

                sw.Close();
                Process.Start("notepad.exe", path);
            }
            catch
            {
                MessageBox.Show("Выберите значения, которые должны экспортироваться");
            }
        }
    }
}
