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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string acno = string.Empty;
        public static  string setAcno
        {
            get { return acno; }
            set { acno = value; }
        }
        //SqlConnection conn = new SqlConnection(@"Data Source=.\SERVER1;Initial Catalog=deepak;User Id=sa; Password=sa;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["globalconn"].ConnectionString);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(txtAc.ToString()=="" || pwdBox.Password.ToString()=="")
            {
                MessageBox.Show("Please Enter Your Account No and Pin then press Enter.","Oops", MessageBoxButton.OK,MessageBoxImage.Information);
            }
           else
            {
            try
            {
                
                SqlCommand cmd = new SqlCommand("[ACLogin].[SLogin]",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acno",txtAc.Text);
                cmd.Parameters.AddWithValue("@pin",pwdBox.Password);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                        setAcno = txtAc.Text;
                    Home windowTwo = new Home();
                    //this will open your child window
                    windowTwo.Show();
                    //this will close parent window. windowOne in this case
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have entered wrong A/C No and Pin,Please try again.","Oops",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(),"Oops",MessageBoxButton.OK,MessageBoxImage.Error);
                
            }
            finally
            {
                conn.Close();
            }

        }  

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txtAc.Text = "";
            pwdBox.Clear();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //for input numbers only in text box
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //for input text only in text box
        private void CharValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
