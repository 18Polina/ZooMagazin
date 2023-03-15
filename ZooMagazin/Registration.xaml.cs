using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        DataSet1 dataSet;
        PersonalsTableAdapter personalsTableAdapter;
        LocationTableAdapter locationTable;
        Labor_bookTableAdapter labor_BookTableAdapter;
        RoleTableAdapter roleTableAdapter;
        GenderTableAdapter genderTableAdapter;

        View_PersonalTableAdapter view_PersonalTable;

        public string ConnectionString { get; }

        private SqlConnection connection;

        public Registration()
        {
            InitializeComponent();
            ConnectionString = @"Data Source=localhost;Initial Catalog=Kinoteatr;Integrated Security=True";
            connection = new SqlConnection(ConnectionString);


            dataSet = new DataSet1();

            personalsTableAdapter = new PersonalsTableAdapter();
            locationTable = new LocationTableAdapter();
            labor_BookTableAdapter = new Labor_bookTableAdapter();
            roleTableAdapter = new RoleTableAdapter();
            genderTableAdapter = new GenderTableAdapter();

            view_PersonalTable = new View_PersonalTableAdapter();

            locationTable.Fill(dataSet.Location);
            personalsTableAdapter.Fill(dataSet.Personals);
            labor_BookTableAdapter.Fill(dataSet.Labor_book);
            roleTableAdapter.Fill(dataSet.Role);
            genderTableAdapter.Fill(dataSet.Gender);

           


            CBCity.ItemsSource = dataSet.Location.DefaultView;
            CBCity.DisplayMemberPath = "City";
            CBCity.SelectedValuePath = "ID_Location";
            CBCity.SelectedIndex = 0;

            CBGender.ItemsSource = dataSet.Gender.DefaultView;
            CBGender.DisplayMemberPath = "Gender";
            CBGender.SelectedValuePath = "ID_Gender";
            CBGender.SelectedIndex = 0;

            CBLaborBook.ItemsSource = dataSet.Labor_book.DefaultView;
            CBLaborBook.DisplayMemberPath = "Number_labor_book";
            CBLaborBook.SelectedValuePath = "ID_Labor_book";
            CBLaborBook.SelectedIndex = 0;

            CBRole.ItemsSource = dataSet.Role.DefaultView;
            CBRole.DisplayMemberPath = "Role";
            CBRole.SelectedValuePath = "ID_Role";
            CBRole.SelectedIndex = 0;

            DataGrid.ItemsSource = dataSet.View_Personal.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Personal";
            DataGrid.CanUserAddRows = false;
            DataGrid.CanUserDeleteRows = false;
            DataGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administrator registration = new Administrator();
            registration.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(SurnamePers.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool c = Regex.Match(NamePers.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(MiddleNamePers.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool d = Regex.Match(Seria.Text, "^[0-9][0-9][0-9][0-9]*$").Success;
            bool f = Regex.Match(Nomer.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool s = Regex.Match(Snils.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool g = Regex.Match(Streets.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool v = Regex.Match(Home.Text, "^[0-9][0-9]*$").Success;
            bool n = Regex.Match(Flat.Text, "^[0-9]*$").Success;
            bool m = Regex.Match(Loogin.Text, "^[A-Z][a-zA-Z]*$").Success;
            bool o = Regex.Match(Pass.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;

            int age = DateTime.Today.Year - Brth.SelectedDate.Value.Year;
            int month = DateTime.Today.Month;
            int day = DateTime.Today.Day;

            if (age < 18)
            {
                MessageBox.Show("Выберите правильные значения");
            }
            else
            {
                if (month < 0 && Brth.SelectedDate.Value.Year < 0)
                {
                    MessageBox.Show("Выберите правильные значения");
                }
                else
                {
                    if (day < 0 && Brth.SelectedDate.Value.Month < 0)
                    {
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {

                        if (!SurnamePers.Text.Equals("") && b)
                        {
                            if (!NamePers.Text.Equals("") && c)
                            {
                                if (!MiddleNamePers.Text.Equals("") && a)
                                {
                                    if (!Seria.Text.Equals("") && d)
                                    {
                                        if (!Nomer.Text.Equals("") && f)
                                        {
                                            if (!Snils.Text.Equals("") && s)
                                            {
                                                if (!Streets.Text.Equals("") && g)
                                                {
                                                    if (!Home.Text.Equals("") && v)
                                                    {
                                                        if (!Flat.Text.Equals("") && n)
                                                        {
                                                            if (!Loogin.Text.Equals("") && m)
                                                            {
                                                                if (!Pass.Text.Equals("") && o)
                                                                {

                                                                    personalsTableAdapter.Insert(SurnamePers.Text, NamePers.Text, MiddleNamePers.Text,
                                                                        Brth.Text, Seria.Text, Nomer.Text, Snils.Text, (int)CBCity.SelectedValue,
                                                                        Streets.Text, Home.Text, Flat.Text, (int)CBGender.SelectedValue,
                                                                        (int)CBLaborBook.SelectedValue, Loogin.Text, Pass.Text, (int)CBRole.SelectedValue);
                                                                    view_PersonalTable.Fill(dataSet.View_Personal);

                                                                    SurnamePers.Text = "";
                                                                    NamePers.Text = "";
                                                                    MiddleNamePers.Text = "";
                                                                    Seria.Text = "";
                                                                    Nomer.Text = "";
                                                                    Snils.Text = "";
                                                                    Streets.Text = "";
                                                                    Home.Text = "";
                                                                    Flat.Text = "";
                                                                    Loogin.Text = "";
                                                                    Pass.Text = "";

                                                                    view_PersonalTable.Fill(dataSet.View_Personal);
                                                                    MessageBox.Show("Сотрудник добавлен");
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Поле пароль сотрудника введено неккоректно");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Поле логин сотрудника введено неккоректно");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Поле строение введен неккоректно");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Поле дома  введена неккоректно");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Поле улица сотрудника введено неккоректно");
                                                }

                                            }
                                            else
                                            {
                                                MessageBox.Show("Поле СНИЛС сотрудника введено неккоректно");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Поле номер паспорта сотрудника введено неккоректно");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Поле серия паспорта сотрудника исполнителя введено неккоректно");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Поле отчество сотрудника введено неккоректно");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Поле имя сотрудника введено неккоректно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле фамилия сотрудника введено неккоректно");
                        }
                    }
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            DataGrid.Columns[9].Visibility = Visibility.Hidden;
            DataGrid.Columns[14].Visibility = Visibility.Hidden;
            DataGrid.Columns[16].Visibility = Visibility.Hidden;
            DataGrid.Columns[19].Visibility = Visibility.Hidden;
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                SurnamePers.Text = dataRowView.Row.Field<String>("Фамилия");
                NamePers.Text = dataRowView.Row.Field<String>("Имя");
                MiddleNamePers.Text = dataRowView.Row.Field<String>("Отчество");
                Brth.Text = dataRowView.Row.Field<String>("Дата_рождения");
                Seria.Text = dataRowView.Row.Field<String>("Серия_паспорта");
                Nomer.Text = dataRowView.Row.Field<String>("Номер_паспорта");
                Snils.Text = dataRowView.Row.Field<String>("СНИЛС");
                CBGender.Text = dataRowView.Row.Field<String>("Пол");
                CBCity.Text = dataRowView.Row.Field<String>("Город");
                Streets.Text = dataRowView.Row.Field<String>("Улица");
                Home.Text = dataRowView.Row.Field<String>("Дом");
                Flat.Text = dataRowView.Row.Field<String>("Корпус");
                CBLaborBook.Text = dataRowView.Row.Field<String>("Номер_трудовой_книжки");
                Loogin.Text = dataRowView.Row.Field<String>("Логин");
                Pass.Text = dataRowView.Row.Field<String>("Пароль");
                CBRole.Text = dataRowView.Row.Field<String>("Роль");

            }
        }
    }
}
