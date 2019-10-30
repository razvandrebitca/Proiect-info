using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BD_Agenda_telefonica
{
    public partial class Form1 : Form
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbConnection connection = new OleDbConnection();
        OleDbDataReader dr;


        public Form1()
        {
            InitializeComponent(); 
        }
        /*cand deschidem programul se stabileste o conexiune cu baza de date.
         * Cand deschideti aplicatia pe un nou PC trebuie schimbat textul din "".
         * Stergeti textul din "" si intrati in View->Server Explorer si 
         * dati click dreapta pe baza de date si accesati Properties si
         * copiati textul din campul Connection string si il puneti in "" ca mai jos.*/
        private void Form1_Load(object sender, EventArgs e)
        {
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Profesor\Desktop\BD_Agenda telefonica\Agenda_telefonica.accdb;Persist Security Info=True";
            cmd.Connection = connection;
            loaddata();
        }
//functia asta afiseaza in listbox-uri toate contactele din baza de adate
        private void loaddata()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            try
            {
                string q = "select * from Agenda";
                cmd.CommandText = q;
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listBox4.Items.Add(dr[0].ToString());
                        listBox1.Items.Add(dr[1].ToString());
                        listBox2.Items.Add(dr[2].ToString());
                        listBox3.Items.Add(dr[3].ToString());
                    }
                }
                dr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Error!" + ex);
            }
        }

        private void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox l = sender as ListBox;

            if (l.SelectedIndex != -1)
            {
                listBox1.SelectedIndex = l.SelectedIndex;
                listBox2.SelectedIndex = l.SelectedIndex;
                listBox3.SelectedIndex = l.SelectedIndex;
                listBox4.SelectedIndex = l.SelectedIndex;
                textBox1.Text = listBox1.SelectedItem.ToString();
                textBox2.Text = listBox2.SelectedItem.ToString();
                textBox3.Text = listBox3.SelectedItem.ToString();
            }
        }
       //instructiuni pentru a adauga un contact nou in agenda
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                string s = "insert into Agenda (Nume,Prenume,Telefon) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "')";
                inserare(s);
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;

            }
        }

        private void inserare(String s)
        {
            try
            {
                connection.Open();
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
                connection.Close();
                loaddata();
            }
            catch (Exception ex1)
            {
                connection.Close();
                MessageBox.Show("Error!" + ex1);
            }
        }
        //instructiuni pentru a sterge un contact
        private void Button2_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text!= "" || textBox2.Text!="" || textBox3.Text!="")
            {
                connection.Open();
                cmd.CommandText = "delete from Agenda where Nume='"+textBox1.Text+"' or Prenume='"+textBox2.Text+"' or Telefon='"+textBox3.Text+"'";
                cmd.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("Deleted");
                loaddata();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

            }
        }
        //instructiuni pentru actualizarea unui contact din agenda
        private void Button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex!=-1)
            {
                connection.Open();
                cmd.CommandText = "update Agenda set Nume='" + textBox1.Text + "' , Prenume='" + textBox2.Text + "' , Telefon='" + textBox3.Text + "' where Nume='"+listBox1.SelectedItem.ToString()+"' and Prenume='"+listBox2.SelectedItem.ToString()+"' and Telefon='"+listBox3.SelectedItem.ToString()+"'";
                cmd.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("Modificat");
                loaddata();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
