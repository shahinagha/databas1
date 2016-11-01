using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Databas_inlämningsupg1
{
    public partial class Form1 : Form
    {
        Personer Pp = new Personer();
        //Button1 = Spara DONE
        //Button2 = Ta bort DONE
        //Button3 = Uppdatera 
        //Button4 = visaalla
        public Form1()
        {
            InitializeComponent();
            UpdateListbox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string namn = txtNamn.Text;
            string gatuadress = txtGatuadress.Text;
            string postnr = txtPostnr.Text;
            string porsort = txtPostort.Text;
            string tel = txtTelefon.Text;
            string epost = txtEpost.Text;
            DateTime födelse = Convert.ToDateTime(dateTimePicker1.Value);

            using (var db = new InUpg1())
            {
                Personer pers = new Personer
                {
                    Namn = namn,
                    GatuAdress = gatuadress,
                    PostNummer = postnr,
                    Postort = porsort,
                    Telefon = tel,
                    Epost = epost,
                    Födelsedag = födelse
                };
                db.Pers.Add(pers);
                db.SaveChanges();

            }
            UpdateListbox();
        }// END OF BUTTON SPARA
        public void UpdateListbox()
        {
            listBox1.Items.Clear();
            using (var db = new InUpg1())
            {
                var kk = from x in db.Pers
                         orderby x.PersonerId
                         select x;

                foreach (var item in kk)
                {
                    listBox1.Items.Add(item.PersonerId + " " + item.Namn);
                }
                button2.Enabled = false;
            }
        }// END OF UPDATELISTBOX

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                button2.Enabled = true;
            
                string index = listBox1.SelectedItem.ToString();
                string[] varden = index.Split(' ');
                string id = varden[0].ToString();
                int idInt = Convert.ToInt32(id);

                using (var db = new InUpg1())
                {
                    var person = (from x in db.Pers
                                  where x.PersonerId == idInt
                                  select x).First();

                    txtNamn.Text = person.Namn;
                    txtGatuadress.Text = person.GatuAdress;
                    txtPostnr.Text = person.PostNummer;
                    txtPostort.Text = person.Postort;
                    txtTelefon.Text = person.Telefon;
                    txtEpost.Text = person.Epost;
                    dateTimePicker1.Text = person.Födelsedag.ToString();
                    txtSok.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string index = listBox1.SelectedItem.ToString();
            string[] varden = index.Split(' ');
            string id = varden[0].ToString();
            int idInt = Convert.ToInt32(id);
            using (var db = new InUpg1())
            {
                var person = db.Pers.SingleOrDefault(x => x.PersonerId == idInt);
                db.Pers.Remove(person);
                db.SaveChanges();
            }
            UpdateListbox();

        }// END OF DELETE-BUTTON.

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateListbox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string index = listBox1.SelectedItem.ToString();
            string[] varden = index.Split(' ');
            string id = varden[0].ToString();
            int idInt = Convert.ToInt32(id);

            using (var db = new InUpg1())
            {
                var person = db.Pers.SingleOrDefault(x => x.PersonerId == idInt);
                person.Namn = txtNamn.Text;
                person.GatuAdress = txtGatuadress.Text;
                person.PostNummer = txtPostnr.Text;
                person.Postort = txtPostort.Text;
                person.Telefon = txtTelefon.Text;
                person.Epost = txtEpost.Text;
                person.Födelsedag = Convert.ToDateTime(dateTimePicker1.Value);
                db.SaveChanges();
            }
            UpdateListbox();

        }// END OF UPDATEBUTTON.

        private void cmdSok_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            string numn = txtSok.Text;
            using (var db = new InUpg1())
            {
                var resultat = from x in db.Pers
                               where x.Namn.Contains(numn)
                               select x;

                foreach (var x in resultat)
                {
                    listBox1.Items.Add(x.PersonerId + " " + x.Namn);
                }
            }
        }
    }
}
