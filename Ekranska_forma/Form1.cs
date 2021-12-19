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


namespace Ekranska_forma
{
    public partial class Form1 : Form
    {
        string cs = "Data source = DESKTOP-UBL32UH; Initial catalog = Skola; Integrated security = true";
        DataTable podaci = new DataTable();
        SqlConnection veza;
        SqlDataAdapter adapter;
        string naziv, sprat, br_ucenika, br_racunara, projektor;

        private void nextbutton_Click(object sender, EventArgs e)
        {
            if (gde + 1 <= podaci.Rows.Count)
            {
                gde++;
                osvezi();
            }
        }

        private void previousbutton_Click(object sender, EventArgs e)
        {
            if (gde - 1 >= 0)
            {
                gde--;
                osvezi();
            }
        }

        private void firstbutton_Click(object sender, EventArgs e)
        {
            gde = 0;
            osvezi();
        }

        private void lastbutton_Click(object sender, EventArgs e)
        {
            gde = podaci.Rows.Count - 1;
            osvezi();
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand(String.Format($"DELETE from Ucionica where id= {txtID.Text}"), veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter("SELECT * from Ucionica", veza);
            adapter.Fill(podaci);
            gde = 0;
            osvezi();

        }

        private void insertbutton_Click(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            naziv = txtNaziv.Text;
            sprat = txtSprat.Text;
            br_ucenika = txtBrUcenika.Text;
            br_racunara = txtBrRacunara.Text;
            projektor = txtProjektor.Text;

            veza.Open();
            SqlCommand naredba = new SqlCommand($"insert into Ucionica values('{naziv}', '{sprat}', '{br_ucenika}', '{br_racunara}', '{projektor}')", veza);
            naredba.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter("SELECT * from Ucionica", veza);
            adapter.Fill(podaci);
            gde = podaci.Rows.Count - 1;
            osvezi();
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            naziv = txtNaziv.Text;
            sprat = txtSprat.Text;
            br_ucenika = txtBrUcenika.Text;
            br_racunara = txtBrRacunara.Text;
            projektor = txtProjektor.Text;

            if (naziv == "" && sprat == "" && br_ucenika == "" && br_racunara == "" && projektor == "")
            {
                MessageBox.Show("Neophodno je uneti podatak.");
            }

            veza.Open();
            SqlCommand naredba = new SqlCommand($"UPDATE Ucionica SET naziv = '{naziv}', sprat = '{sprat}', br_ucenika = '{br_ucenika}', br_racunara = '{br_racunara}', projektor = '{projektor}' WHERE id = {txtID.Text}", veza);
            naredba.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter("SELECT * from Ucionica", veza);
            adapter.Fill(podaci);
            osvezi();
        }

        private void Ocistiti_Click(object sender, EventArgs e)
        {
            txtNaziv.Text = "";
            txtSprat.Text = "";
            txtBrUcenika.Text = "";
            txtBrRacunara.Text = "";
            txtProjektor.Text = "";
        }

        int gde = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            adapter = new SqlDataAdapter("SELECT * from Ucionica", veza);
            adapter.Fill(podaci);
            txtID.Enabled = false;
            osvezi();
            
        }

        public void osvezi()
        {
            if (podaci.Rows.Count == 0)
            {
                updatebutton.Enabled = false;
                deletebutton.Enabled = false;
                firstbutton.Enabled = false;
                lastbutton.Enabled = false;
                previousbutton.Enabled = false;
                nextbutton.Enabled = false;

                txtID.Text = "";
                txtNaziv.Text = "";
                txtSprat.Text = "";
                txtBrUcenika.Text = "";
                txtBrRacunara.Text = "";
                txtProjektor.Text = "";
            }
            else 
            {
                txtID.Text = podaci.Rows[gde]["id"].ToString();
                txtNaziv.Text = podaci.Rows[gde]["naziv"].ToString();
                txtSprat.Text = podaci.Rows[gde]["sprat"].ToString();
                txtBrUcenika.Text = podaci.Rows[gde]["br_ucenika"].ToString();
                txtBrRacunara.Text = podaci.Rows[gde]["br_racunara"].ToString();
                txtProjektor.Text = podaci.Rows[gde]["projektor"].ToString();

                firstbutton.Enabled = (gde != 0);
                lastbutton.Enabled = (gde != podaci.Rows.Count - 1);
                previousbutton.Enabled = (gde != 0);
                nextbutton.Enabled = (gde != podaci.Rows.Count -1 );
            }
        }
        
        public Form1()
        {
            InitializeComponent();
        }


    }
}
