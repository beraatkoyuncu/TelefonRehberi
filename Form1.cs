using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelephoneDirectory
{
    public partial class Form1 : Form
    {

        SqlConnection connection;
        SqlCommand command;




        public Form1()
        {
            InitializeComponent();
        }


        void Listing()
        {
            connection = new SqlConnection("Data Source=DESKTOP-Q9BA0JM;Initial Catalog=TelephoneBook;Integrated Security=True");
            command = new SqlCommand("select[id], [name], [surname], [telephone] from Persons", connection);
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    dataGridView1.Rows.Add(dataReader.GetInt32(0),dataReader.GetString(1),dataReader.GetString(2),dataReader.GetString(3) );
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Numaralar alınamadı.");
                
            }
            finally
            {
                if(connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Listing();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int result = -1;
            connection = new SqlConnection("Data Source=DESKTOP-Q9BA0JM;Initial Catalog=TelephoneBook;Integrated Security=True");
            command = new SqlCommand("insert into Persons([name], [surname], [telephone]) values(@n, @s, @t)", connection);
            command.Parameters.AddWithValue("@n", textBoxName.Text);
            command.Parameters.AddWithValue("@s", textBoxSurname.Text);
            command.Parameters.AddWithValue("@t", textBoxNumber.Text);

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Bağlantıda hata oluştu.");

            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            if (result > 0)
            {
                MessageBox.Show("Telefon numarası kaydedildi.");
                Listing();
            }
            else
            {
                MessageBox.Show("Telefon numarası kaydedilemedi.");
                
            }

           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            object value = dataGridView1.SelectedRows[0].Cells[0].Value;
            int id = (int)value;
            connection = new SqlConnection("Data Source=DESKTOP-Q9BA0JM;Initial Catalog=TelephoneBook;Integrated Security=True");
            command = new SqlCommand("select top 1 [id], [name], [surname], [telephone] from Persons where id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    textBoxName.Text = dataReader.GetString(1);
                    textBoxSurname.Text = dataReader.GetString(2);
                    textBoxNumber.Text = dataReader.GetString(3);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Telefon numarası alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }




        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int res = -1;
            if ( dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Numara silinecek, onaylıyor musunuz ?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            object value = dataGridView1.SelectedRows[0].Cells[0].Value;
            int id = (int)value;

            connection = new SqlConnection("Data Source=DESKTOP-Q9BA0JM;Initial Catalog=TelephoneBook;Integrated Security=True");
            command = new SqlCommand("delete Persons where [id]=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                res = command.ExecuteNonQuery();
                MessageBox.Show($"Telefon numarası sili{(res>0 ? "ndi" : "nemedi")}." );
            }
            catch (Exception)
            {
                

            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            if (res>0)
            {
                Listing();
            }
            
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int result = -1;
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            object value = dataGridView1.SelectedRows[0].Cells[0].Value;
            int id = (int)value;
            connection = new SqlConnection("Data Source=DESKTOP-Q9BA0JM;Initial Catalog=TelephoneBook;Integrated Security=True");
            command = new SqlCommand("update Persons set name=@n, surname=@s, telephone=@t where id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@n", textBoxName.Text);
            command.Parameters.AddWithValue("@s", textBoxSurname.Text);
            command.Parameters.AddWithValue("@t", textBoxNumber.Text);


            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Bağlantıda hata oluştu.");

            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            if (result > 0)
            {
                MessageBox.Show("Telefon numarası kaydedildi.");
                Listing();
            }
            else
            {
                MessageBox.Show("Telefon numarası kaydedilemedi.");
                
            }

           


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
