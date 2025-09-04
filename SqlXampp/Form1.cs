using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data;
using System.Windows.Forms;

namespace SqlXampp
{
    public partial class Form1 : Form
    {
        MySqlConnection mySqlConnection = new MySqlConnection("server=127.0.0.1;user=root;database=sample; password=");
        private int? selectedGameId = null;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            dataGridView1.CellClick += DataGridView1_CellClick;
            btnUpdate.Click += BtnUpdate_Click;
        }
        

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGames();
            txtSearch.TextChanged += TxtSearch_TextChanged; 
            
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchGames(txtSearch.Text);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Live search as you type
            SearchGames(txtSearch.Text);
        }

        private void SearchGames(string keyword)
        {
            try
            {
                string query = "SELECT * FROM games " +
                               "WHERE title LIKE @keyword OR genre LIKE @keyword OR platform LIKE @keyword OR status LIKE @keyword";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection);
                adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching games: " + ex.Message);
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control ctrl = this.ActiveControl;

                if (ctrl == cmbStatus)
                {
                    if (selectedGameId == null)
                        InsertGame();
                    else
                        UpdateGame();
                }
                else
                {
                    this.SelectNextControl(ctrl, true, true, true, true);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ResetForm();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                DeleteGame();
            }

        }

        private void DeleteGame()
        {
            if (selectedGameId == null)
            {
                MessageBox.Show("Please select a game to delete.");
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this game?",
                                              "Confirm Delete",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM games WHERE game_id=@game_id";
                    MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.Parameters.AddWithValue("@game_id", selectedGameId);

                    mySqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    mySqlConnection.Close();

                    MessageBox.Show("Game deleted successfully!");
                    LoadGames();
                    ResetForm(); // clear inputs
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting game: " + ex.Message);
                    if (mySqlConnection.State == ConnectionState.Open)
                        mySqlConnection.Close();
                }
            }
        }


        private void ResetForm()
        {
            txtTitle.Clear();
            cmbGenre.SelectedIndex = -1;
            cmbPlatform.SelectedIndex = -1;
            dtpReleaseDate.Value = DateTime.Now;
            txtDeveloper.Clear();
            txtPublisher.Clear();
            cmbRating.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            cmbMultiplayer.SelectedIndex = -1;

            selectedGameId = null; // back to Insert mode
        }


        private void InsertGame()
        {
            try
            {
                string query = "INSERT INTO games " +
                               "(title, genre, platform, release_date, developer, publisher, rating, status, multiplayer) " +
                               "VALUES (@title, @genre, @platform, @release_date, @developer, @publisher, @rating, @status, @multiplayer)";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);

                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@genre", cmbGenre.Text);
                cmd.Parameters.AddWithValue("@platform", cmbPlatform.Text);
                cmd.Parameters.AddWithValue("@release_date", dtpReleaseDate.Value);
                cmd.Parameters.AddWithValue("@developer", txtDeveloper.Text);
                cmd.Parameters.AddWithValue("@publisher", txtPublisher.Text);
                cmd.Parameters.AddWithValue("@rating", cmbRating.Text);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                cmd.Parameters.AddWithValue("@multiplayer", cmbMultiplayer.Text);

                mySqlConnection.Open();
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();

                MessageBox.Show("Game added successfully!");
                LoadGames();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (mySqlConnection.State == ConnectionState.Open)
                    mySqlConnection.Close();
            }
        }

        private void LoadGames()
        {
            try
            {
                string query = "SELECT * FROM games";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading games: " + ex.Message);
            }
        }

        
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                selectedGameId = Convert.ToInt32(row.Cells["game_id"].Value);

                txtTitle.Text = row.Cells["title"].Value.ToString();
                cmbGenre.Text = row.Cells["genre"].Value.ToString();
                cmbPlatform.Text = row.Cells["platform"].Value.ToString();
                dtpReleaseDate.Value = Convert.ToDateTime(row.Cells["release_date"].Value);
                txtDeveloper.Text = row.Cells["developer"].Value.ToString();
                txtPublisher.Text = row.Cells["publisher"].Value.ToString();
                cmbRating.Text = row.Cells["rating"].Value.ToString();
                cmbStatus.Text = row.Cells["status"].Value.ToString();
                cmbMultiplayer.Text = row.Cells["multiplayer"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateGame()
        {
            try
            {
                if (selectedGameId == null) return;

                string query = "UPDATE games SET " +
                               "title=@title, genre=@genre, platform=@platform, release_date=@release_date, " +
                               "developer=@developer, publisher=@publisher, rating=@rating, status=@status, multiplayer=@multiplayer " +
                               "WHERE game_id=@game_id";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);

                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@genre", cmbGenre.Text);
                cmd.Parameters.AddWithValue("@platform", cmbPlatform.Text);
                cmd.Parameters.AddWithValue("@release_date", dtpReleaseDate.Value);
                cmd.Parameters.AddWithValue("@developer", txtDeveloper.Text);
                cmd.Parameters.AddWithValue("@publisher", txtPublisher.Text);
                cmd.Parameters.AddWithValue("@rating", cmbRating.Text);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                cmd.Parameters.AddWithValue("@multiplayer", cmbMultiplayer.Text);
                cmd.Parameters.AddWithValue("@game_id", selectedGameId);

                mySqlConnection.Open();
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();

                MessageBox.Show("Game updated successfully!");
                LoadGames();

                selectedGameId = null; // reset back to insert mode
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating game: " + ex.Message);
                if (mySqlConnection.State == ConnectionState.Open)
                    mySqlConnection.Close();
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbMultiplayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
