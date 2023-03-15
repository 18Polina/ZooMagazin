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
    /// Логика взаимодействия для Dogovor_postavki.xaml
    /// </summary>
    public partial class Dogovor_postavki : Window
    {
        DataSet1 dataSet;
        Delivery_invoiceTableAdapter delivery_InvoiceTable;

        View_deliveryInvoiceTableAdapter view_DeliveryInvoice;
        public Dogovor_postavki()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            delivery_InvoiceTable = new Delivery_invoiceTableAdapter();

            view_DeliveryInvoice = new View_deliveryInvoiceTableAdapter();

            delivery_InvoiceTable.Fill(dataSet.Delivery_invoice);

            view_DeliveryInvoice.Fill(dataSet.View_deliveryInvoice);


            DataGrid.ItemsSource = dataSet.View_deliveryInvoice.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Delivery_invoice";
            DataGrid.CanUserAddRows = false;
            DataGrid.CanUserDeleteRows = false;
            DataGrid.IsReadOnly = true;
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menedjer_po_postavki registration = new Menedjer_po_postavki();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(Kolvo.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;
            bool a = Regex.Match(Summa.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9][ ][а-яА-Я]*$").Success;
            bool c = Regex.Match(NameOrg.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool d = Regex.Match(MiddleNameSotr.Text, "^[А-Я][а-яА-Я]*$").Success;

            int age = DateTime.Today.Year - Data.SelectedDate.Value.Year;
            int month = DateTime.Today.Month - Data.SelectedDate.Value.Month;
            int day = DateTime.Today.Day - Data.SelectedDate.Value.Day;

            if (age < 0)
            {
                MessageBox.Show("Выберите правильные ");
            }
            else
            {
                if (month < 0)
                {
                    MessageBox.Show("Выберите  значения");
                }
                else
                {
                    if (day < 0)
                    {
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {

                        if (!Kolvo.Text.Equals("") && b)
                        {
                            if (!Summa.Text.Equals("") && a)
                            {
                                if (!NameOrg.Text.Equals("") && c)
                                {
                                    if (!MiddleNameSotr.Text.Equals("") && d)
                                    {
                                        delivery_InvoiceTable.Insert(Data.Text, Kolvo.Text, Summa.Text, NameOrg.Text, MiddleNameSotr.Text);
                                        view_DeliveryInvoice.Fill(dataSet.View_deliveryInvoice);

                                        Data.Text = "";
                                        Kolvo.Text = "";
                                        Summa.Text = "";
                                        NameOrg.Text = "";
                                        MiddleNameSotr.Text = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Поле фамилия исполнителя введено неккоректно");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Поле название организации введено неккоректно");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Поле сумма договора введено неккоректно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле количество дней введено неккоректно");
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(Kolvo.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;
            bool a = Regex.Match(Summa.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9][ ][а-яА-Я]*$").Success;
            bool c = Regex.Match(NameOrg.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool d = Regex.Match(MiddleNameSotr.Text, "^[А-Я][а-яА-Я]*$").Success;

            int age = DateTime.Today.Year - Data.SelectedDate.Value.Year;
            int month = DateTime.Today.Month - Data.SelectedDate.Value.Month;
            int day = DateTime.Today.Day - Data.SelectedDate.Value.Day;

            if (age < 0)
            {
                MessageBox.Show("Выберите правильные ");
            }
            else
            {
                if (month < 0)
                {
                    MessageBox.Show("Выберите  значения");
                }
                else
                {
                    if (day < 0)
                    {
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {

                        if (!Kolvo.Text.Equals("") && b)
                        {
                            if (!Summa.Text.Equals("") && a)
                            {
                                if (!NameOrg.Text.Equals("") && c)
                                {
                                    if (!MiddleNameSotr.Text.Equals("") && d)
                                    {
                                        delivery_InvoiceTable.UpdateQuery(Data.Text, Kolvo.Text, Summa.Text, NameOrg.Text, MiddleNameSotr.Text, (int)DataGrid.SelectedValue);
                                        view_DeliveryInvoice.Fill(dataSet.View_deliveryInvoice);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Поле дата принятия на работу введено неккоректно");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            if (DataGrid.SelectedItem != null)
            {
                delivery_InvoiceTable.DeleteQuery((int)DataGrid.SelectedValue);
                view_DeliveryInvoice.Fill(dataSet.View_deliveryInvoice);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                Data.Text = dataRowView.Row.Field<String>("Дата_поставки");
                Kolvo.Text = dataRowView.Row.Field<String>("Количество_дней");
                Summa.Text = dataRowView.Row.Field<String>("Стоимсоть");
                NameOrg.Text = dataRowView.Row.Field<String>("Название_организации");
                MiddleNameSotr.Text = dataRowView.Row.Field<String>("Фамилия_исполнителя");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string index = Poisk.Text;

            if (String.IsNullOrWhiteSpace(index))
            {
                return;
            }
            else
            {
                for (int i = 0; i < dataSet.View_deliveryInvoice.Rows.Count; i++)
                {

                    if (dataSet.View_deliveryInvoice.Rows[i][4].ToString().Contains(index))
                    {
                        DataGrid.SelectedIndex = i;
                        return;
                    }

                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = "Otchet_o_dogovore_postavki.txt";
                StreamWriter sw = new StreamWriter(path);
                DataTable dt_user = dataSet.View_deliveryInvoice;
                sw.WriteLine("Отчет о договоре поставки: " + DateTime.Now.ToString() + " на текущий месяц");
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                   sw.WriteLine("Дата поставки: " + dt_user.Rows[i][1].ToString());
                    sw.WriteLine("Количество дней: " + dt_user.Rows[i][2].ToString());
                    sw.WriteLine("Сумма договора: " + dt_user.Rows[i][3].ToString());
                    sw.WriteLine("Навзание организации: " + dt_user.Rows[i][4].ToString());
                    sw.WriteLine("Фамилия исполнителя: " + dt_user.Rows[i][5].ToString());
                    

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            
        }
    }
}
