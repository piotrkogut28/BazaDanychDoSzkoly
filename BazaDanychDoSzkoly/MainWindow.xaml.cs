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
using MySql.Data.MySqlClient;
using System.Windows.Resources;

namespace BazaDanychDoSzkoly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string get_id;
        MySqlConnection con= new MySqlConnection("SERVER=localhost;DATABASE=bazaszkola;UID=root;PASSWORD=;");
        MySqlCommand cmd;
        MySqlDataAdapter adapt;
        int ID = 1;
        public MainWindow()
        {
            InitializeComponent();
            DisplayData();

        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            if (txt_name.Text !="" && txt_secondName.Text !="" && txt_country.Text !="")
            {
                cmd = new MySqlCommand("insert into bazawpf(Imie,Nazwisko,Kraj) values(@name,@secondName,@country)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name",txt_name.Text);
                cmd.Parameters.AddWithValue("@secondName",txt_secondName.Text);
                cmd.Parameters.AddWithValue("@country",txt_country.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Pomyslnie dodano rekord!");
                DisplayData();
                ClearData();

            }
            else
            {
                MessageBox.Show("Wprowadz wszystkie dane");
            }
        }
        
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select * from bazawpf", con);
            adapt.Fill(dt);
            datagrid1.DataContext= dt;
            con.Close();
        }
       
        private void ClearData()
        {
            txt_name.Text = "";
            txt_secondName.Text = "";
            txt_country.Text = "";
        }


        


        private void btn_Update_Click(object sender, EventArgs e)
        {
            get_id = search_txt.Text;
            if (txt_name.Text != "" && txt_secondName.Text != "" && txt_country.Text !="")
            {
                cmd = new MySqlCommand("update bazawpf set Imie=@name,Nazwisko=@secondName,Kraj=@country where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", get_id);
                cmd.Parameters.AddWithValue("@name", txt_name.Text);
                cmd.Parameters.AddWithValue("@secondName", txt_secondName.Text);
                cmd.Parameters.AddWithValue("@country", txt_country.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rekord Zaakutalizowany! ");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Wybierz rekord!");
            }
       


        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            get_id = search_txt.Text;
            if (ID != 0)
            {
                cmd = new MySqlCommand("delete from bazawpf where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", get_id);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("usunięto pomyslnie!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Wybierz rekord do usunięcia");
            }

        }

        private void datagrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(datagrid1.SelectedCells.ToString());

        }
    }

}
