using FirebirdSql.Data.FirebirdClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TestingWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public long UserId;
        private Configuration myConfiguration;
        private ISessionFactory mySessionFactory;
        private ISession mySession;

        public MainWindow()
        {
            InitializeComponent();
        }
                
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myConfiguration = new Configuration();
            myConfiguration.Configure();
            mySessionFactory = myConfiguration.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();
            try
            {
                mySession = mySessionFactory.OpenSession();
            }
            catch (Exception exp) { }
        }

        private void updateDataGrid()
        {
            using (mySession.BeginTransaction())
            {
                var datagrid = mySession.CreateCriteria<Users>().List<Users>();
                UsersList.ItemsSource = datagrid;
            }
        }

        private void Get_list_btn_Click(object sender, RoutedEventArgs e)
        {
            updateDataGrid();
        }

        private void Window_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mySession.Close();
        }                

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            using (mySession.BeginTransaction())
            {
                Users person = new Users { LOGIN = textBox_Login.Text, FIRST_NAME = textBox_First_Name.Text, LAST_NAME = textBox_Last_Name.Text, MIDDLE_NAME = textBox_Middle_Name.Text };
                //Добаление аккаунта пока только по возможным полям, надо будет доработать функционал по обязательному наполнению формы, клиентом, всех текстовых полей
                mySession.Save(person);
                UserId = person.ID;
                mySession.Transaction.Commit();
                updateDataGrid();
                mySession.Clear();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (mySession.BeginTransaction())
            {
                var datagrid = mySession.CreateCriteria<Users>().List<Users>();

                foreach (var person in datagrid)
                {
                    if(person.LOGIN.Equals(textBox_Login.Text))//Удаление аккаунта, пока только по совпадению Логина
                    {
                        mySession.Delete(person);
                        mySession.Transaction.Commit();
                    }
                }
                updateDataGrid();
            }
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            using (mySession.BeginTransaction())
            {
                var datagrid = mySession.QueryOver<Users>().Where(person => person.LOGIN == textBlock_Login.Text).List();

                //Вариант с модификацией через форму заполнения пока будет на втором месте, надо освоить редактирование данных через грид и нажатием клавиши "применить", также реализовать поиск
                //foreach (var person in datagrid)
                //{
                //    if ()
                //    {
                //        person.LOGIN = person.LOGIN.Replace(person.LOGIN, textBox_Login.Text);
                //    }
                //    mySession.Update(person);
                //}
            mySession.Transaction.Commit();
            updateDataGrid();
            }
        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            using (mySession.BeginTransaction())
            {
                var datagrid = mySession.CreateCriteria<Users>().List<Users>();

                foreach (var person in datagrid)
                {
                    if(person.LOGIN.Contains(textBox_Login.Text))
                    {
                        mySession.Delete(person);
                    }
                }
            }
        }

        private void Issue_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Confirm_btn_Click(object sender, RoutedEventArgs e)//
        {
            
        }

        
    }
}
