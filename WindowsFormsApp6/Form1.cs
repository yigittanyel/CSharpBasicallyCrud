using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }

        //LİSTELE( SELECT )
        void OgrenciListele()
        {
            baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=kutuphane;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * from ogrenci",baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("E");
            comboBox1.Items.Add("K");

            comboBox2.Items.Add("9B");
            comboBox2.Items.Add("9C");
            comboBox2.Items.Add("10A");
            comboBox2.Items.Add("10B");
            comboBox2.Items.Add("10C");
            comboBox2.Items.Add("11A");
            comboBox2.Items.Add("11C");

            OgrenciListele();
            
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        //EKLE
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT into ogrenci(ograd,ogrsoyad,cinsiyet,dtarih,sinif,puan) values(@ograd,@ogrsoyad,@cinsiyet,@dtarih,@sinif,@puan)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ograd", textBox2.Text);
            komut.Parameters.AddWithValue("@ogrsoyad", textBox3.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@sinif", comboBox2.Text);
            komut.Parameters.AddWithValue("@puan", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
            MessageBox.Show("Kayıt başarıyla eklendi.");
        }

        //SİL
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE from ogrenci where ogrno=@ogrno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ogrno", Convert.ToInt32(textBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
            MessageBox.Show("Kayıt başarıyla silindi.");
        }

       //GÜNCELLE
        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE ogrenci set ograd=@ograd,ogrsoyad=@ogrsoyad,cinsiyet=@cinsiyet,dtarih=@dtarih,sinif=@sinif,puan=@puan where ogrno=@ogrno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ogrno", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@ograd", textBox2.Text);
            komut.Parameters.AddWithValue("@ogrsoyad", textBox3.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@sinif", comboBox2.Text);
            komut.Parameters.AddWithValue("@puan", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
            MessageBox.Show("Kayıt başarıyla güncellendi.");
            // {0} nolu kayıt güncellendi nasıl yapılır?
        }
    }
}
