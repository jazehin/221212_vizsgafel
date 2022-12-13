using Microsoft.Data.SqlClient;
using _221212gui.Properties;
using System.Diagnostics;
using System.Xml.Serialization;

namespace _221212gui
{
    public partial class Form1 : Form
    {
        private static SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new(Resources.ConnectionString);
            conn.Open();
            
            cbGenre.Items.Clear();
            string sql = "SELECT DISTINCT name FROM genre;";
            SqlDataReader reader = new SqlCommand(sql, conn).ExecuteReader();
            while (reader.Read())
            {
                cbGenre.Items.Add(reader["name"]);
            }
            reader.Close();

            dgv.Rows.Clear();
            sql = "SELECT game.id, game.title, game.year, genre.name as genre FROM game INNER JOIN genre ON game.genreId = genre.id;";
            reader = new SqlCommand(sql, conn).ExecuteReader();
            while (reader.Read())
            {
                dgv.Rows.Add(
                    reader["id"],
                    reader["title"],
                    reader["year"],
                    reader["genre"]
                    );
            }
            reader.Close();
        }

        

        private void UpdateDgv(object sender, EventArgs e)
        {
            string searchTerm = tbTitle.Text;
            string genre = cbGenre.Text;

            string sql = "SELECT game.id, game.title, game.year, genre.name as genre " +
                "FROM game INNER JOIN genre ON game.genreId = genre.id " +
                $"WHERE game.title LIKE '%{searchTerm}%' " +
                (string.IsNullOrWhiteSpace(genre) ? "" : $"AND genre.name LIKE '{genre.Replace("'", "''")}'") +
                ";";

            dgv.Rows.Clear();
            SqlDataReader reader = new SqlCommand(sql, conn).ExecuteReader();
            while (reader.Read())
            {
                dgv.Rows.Add(
                    reader["id"],
                    reader["title"],
                    reader["year"],
                    reader["genre"]
                    );
            }
            reader.Close();

            dgv.ClearSelection();
        }

        private void lnklbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(lnklbl.Text) { UseShellExecute = true });
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;

            string title = dgv.SelectedCells[1].Value.ToString();
            string link = "https://www.google.com/search?q=" + title.Replace(" ", "+");
            lnklbl.Text = link;
        }
    }
}