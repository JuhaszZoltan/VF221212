using Microsoft.Data.SqlClient;
using System.Diagnostics;
using WFA221213.Properties;

namespace WFA221213
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            tbSearch.TextChanged += OnSearchConditionChange;
            cbSearch.TextChanged += OnSearchConditionChange;
            this.Load += OnSearchConditionChange;
            this.Load += OnMainFromLoad;
            dgv.CellClick += OnDgvCellClick;
            linkGoogle.LinkClicked += OnLinkClicked;
        }

        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(linkGoogle.Text) { UseShellExecute = true });
        }

        private void OnDgvCellClick(object? sender, DataGridViewCellEventArgs e)
        {
            linkGoogle.Text = "https://www.google.com/search?q="
                + dgv[1, e.RowIndex].Value.ToString()!.Replace(' ', '+');
        }

        private void OnMainFromLoad(object? sender, EventArgs e)
        {
            using SqlConnection conn = new(Resources.ConnectionString);
            conn.Open();
            SqlDataReader rdr = new SqlCommand(
                "SELECT DISTINCT name FROM genre ORDER BY name ASC;", conn)
                .ExecuteReader();
            while (rdr.Read()) cbSearch.Items.Add(rdr[0]);
        }

        private void OnSearchConditionChange(object? sender, EventArgs e)
        {
            dgv.Rows.Clear();
            using SqlConnection conn = new(Resources.ConnectionString);
            conn.Open();

            SqlDataReader rdr = new SqlCommand(
                cmdText: "SELECT game.id, title, year, genre.name " +
                "FROM game INNER JOIN genre ON genreId = genre.id " +
                $"WHERE title LIKE '{tbSearch.Text}%' " +
                $"AND genre.name LIKE '{cbSearch.Text.Replace("'", "''")}%' " +
                $"ORDER BY title ASC;",
                connection: conn)
                .ExecuteReader();

            while (rdr.Read()) dgv.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
        }
    }
}