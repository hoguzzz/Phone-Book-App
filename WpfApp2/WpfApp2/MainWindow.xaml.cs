using Npgsql;
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




namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432;Database=postgres; user ID=postgres;password=1234");
        private void listele_Click(object sender, RoutedEventArgs e)
        {
            string sorgu = "SELECT * FROM phone_book ORDER BY id";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            datagrid.ItemsSource = ds.Tables[0].DefaultView;


        }

        private void ekle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("Insert into phone_book(id,first_name,last_name,phone,email,adress) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                komut1.Parameters.AddWithValue("@p1", int.Parse(txtID.Text));
                komut1.Parameters.AddWithValue("@p2", txtName.Text);
                komut1.Parameters.AddWithValue("@p3", txtSurname.Text);
                komut1.Parameters.AddWithValue("@p4", txtPhone.Text);
                komut1.Parameters.AddWithValue("@p5", txtEmail.Text);
                komut1.Parameters.AddWithValue("@p6", txtAdress.Text);
                komut1.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi Eklendi.");
            }catch (Exception ex)
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz giriniz!!!");
            }
               

        }

        private void kaldir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM phone_book where id=@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtID.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi silindi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silinmesini istediğiniz kişinin bilgilerini giriniz...");
            }
        }

        private void guncelle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut3 = new NpgsqlCommand("UPDATE phone_book SET first_name=@p2,last_name=@p3,phone=@p4,email=@p5,adress=@p6 WHERE id=@p1", baglanti);
                komut3.Parameters.AddWithValue("@p1", int.Parse(txtID.Text));
                komut3.Parameters.AddWithValue("@p2", txtName.Text);
                komut3.Parameters.AddWithValue("@p3", txtSurname.Text);
                komut3.Parameters.AddWithValue("@p4", txtPhone.Text);
                komut3.Parameters.AddWithValue("@p5", txtEmail.Text);
                komut3.Parameters.AddWithValue("@p6", txtAdress.Text);
                komut3.ExecuteNonQuery();
                MessageBox.Show("Kişi Güncellendi");
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz giriniz!!!!");
            }
        }

        private void Ara_Click(object sender, RoutedEventArgs e)
        {
            if(sbn.IsChecked != null)
            {
                string sorgu2 = "SELECT first_name FROM phone_book ORDER BY id";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu2, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                datagrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                string sorgu3 = "SELECT last_name FROM phone_book ORDER BY id";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu3, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                datagrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Sbn_Checked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
