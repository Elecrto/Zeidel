using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeidel
{
    public partial class Form2 : Form
    {
        public string username;
        public string password;
        public string email;
        private const string connectionString = "Host=localhost;Port=5432;Database=first_db;Username=postgres;Password=admin";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[,] A = { { Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox7.Text) }, { Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox8.Text) }, { Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox9.Text) } };
            double[] b = { Convert.ToDouble(textBox10.Text), Convert.ToDouble(textBox11.Text), Convert.ToDouble(textBox12.Text) };
            int maxIterations = 1000;
            double epsilon = 1e-6;
            Solve sol = new Solve();
            double[] solution = sol.Funk(A, b, maxIterations, epsilon);
            String ans = ConvertToString(solution);
            String coef = ConvertToString(A);
            try
            {
                // Создание подключения к PostgreSQL
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    // Открытие подключения
                    connection.Open();

                    // Создание команды для выполнения запроса INSERT
                    string insertQuery = "INSERT INTO users (username, password, email, coef, solv) VALUES (@value1, @value2, @value3, @value4, @value5)";
                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                    {
                        // Передача параметров запроса
                        command.Parameters.AddWithValue("@value1", username);
                        command.Parameters.AddWithValue("@value2", password);
                        command.Parameters.AddWithValue("@value3", email);
                        command.Parameters.AddWithValue("@value4", coef);
                        command.Parameters.AddWithValue("@value5", ans);
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
            }
        }
        private string ConvertToString(double[] array)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i]);
                if (i < array.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            return sb.ToString();
        }
        private string ConvertToString(double[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(array[i, j]);
                    if (j < cols - 1)
                    {
                        sb.Append(", ");
                    }
                }
                if (i < rows - 1)
                {
                    sb.Append(", ");
                }
            }
            return sb.ToString();
        }
    }
}
