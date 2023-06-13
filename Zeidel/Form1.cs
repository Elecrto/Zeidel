using Npgsql;
namespace Zeidel
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Host=localhost;Port=5432;Database=first_db;Username=postgres;Password=admin";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                /*try
                {
                    // Создание подключения к PostgreSQL
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        // Открытие подключения
                        connection.Open();

                        // Создание команды для выполнения запроса INSERT
                        string insertQuery = "INSERT INTO users (username, password, email) VALUES (@value1, @value2, @value3)";
                        using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                        {
                            // Передача параметров запроса
                            command.Parameters.AddWithValue("@value1", textBox1.Text);
                            command.Parameters.AddWithValue("@value2", textBox2.Text);
                            command.Parameters.AddWithValue("@value3", textBox3.Text);
                            // Выполнение запроса
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data inserted successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Data insertion failed.");
                            }
                        }

                        // Закрытие подключения
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data into PostgreSQL: " + ex.Message);
                }*/
                label4.Text = "";
                Form2 newForm = new Form2();
                newForm.username = textBox1.Text;
                newForm.password = textBox2.Text;
                newForm.email = textBox3.Text;

                newForm.Show();
                //Hide();
            }
            else
            {
                label4.Text = "Заполните все поля!";
            }
        }
    }
}