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

namespace Not_Kayit_Sistemi1
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection
            (@"Data Source=CAN-DEMIRPC\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS(OGRNUMARA,OGRAD,OGRSOYAD)values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1=Convert.ToDouble(TxtSinav1.Text);
            s2=Convert.ToDouble(TxtSinav2.Text);
            s3=Convert.ToDouble(TxtSinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text=ortalama.ToString();
            if (ortalama >= 50)
            {
                durum = "true";
            }
            else
            {
                durum = "false";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@p1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@p4,DURUM=@p5 WHERE OGRNUMARA=@P6",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@p2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@p3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci notları güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
            LblKalanSayisi.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == false).ToString();
            LblGecenSayisi.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == true).ToString();
            LblSnfOrt.Text = dbNotKayitDataSet.TBLDERS.Sum(y => y.ORTALAMA / (Convert.ToInt32(LblGecenSayisi.Text) + Convert.ToInt32(LblKalanSayisi.Text))).ToString();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

    }
}
