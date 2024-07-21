using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace food_ordering
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        public void maincon()
        {
            string sql_conn = "server=localhost;user=root;database=munchbar_db;port=3306";
            conn = new MySqlConnection(sql_conn);
            conn.Open();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void guna2GradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void LoadItems()
        {
            maincon();
            string qry = "SELECT itemname,unitprice,category,image FROM item";
            MySqlCommand cmd = new MySqlCommand(qry, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearry = (byte[])item["image"];

                AddItems(item["itemname"].ToString(), item["unitprice"].ToString(), item["category"].ToString(), Image.FromStream(new MemoryStream(imagearry)));
            }
            conn.Close();
        }
        private void AddItems(string name, string price, string category, Image image)
        {
            var w = new product_widget()
            {

                Name = name,
                Price = price,
                Category = category,
                PImage = image


            };
            itemPanel.Controls.Add(w);
            w.onSelect += (ss, ee) =>
            {
                var itemwdg = (product_widget)ss;

                foreach (DataGridViewRow item in dgvItemtable.Rows)
                {
                    if (item.Cells["dgvName"].Value.ToString() == itemwdg.Name)
                    {
                        item.Cells["dgvQuantity"].Value = int.Parse(item.Cells["dgvQuantity"].Value.ToString()) + itemwdg.Count;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQuantity"].Value.ToString()) * decimal.Parse(item.Cells["dgvPrice"].Value.ToString());
                        GetTotal();
                        return;
                    }

                }
                dgvItemtable.Rows.Add(new object[] { itemwdg.Name, itemwdg.Count, itemwdg.Price, itemwdg.Price, "Remove" });
                GetTotal();
            };
        }

        private void GetTotal()
        {
            decimal total = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in dgvItemtable.Rows)
            {
                total += decimal.Parse(item.Cells["dgvAmount"].Value.ToString());
            }

            lblTotal.Text = total.ToString();
        }

        private void dgvItemtable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvItemtable.Columns["dgvAction"].Index && e.RowIndex >= 0)
            {
                dgvItemtable.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            maincon();
            foreach (DataGridViewRow row in dgvItemtable.Rows)
            {
                // Extract data from DataGridView row

                string itemName = row.Cells["dgvName"].Value.ToString();
                int quantity = Convert.ToInt32(row.Cells["dgvQuantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["dgvPrice"].Value);
                decimal amount = Convert.ToDecimal(row.Cells["dgvAmount"].Value);

                // Define SQL command
                string sql = "INSERT INTO temp_bill VALUES (@itemName, @quantity, @price, @amount)";

                // Create and execute SQL command
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@itemName", itemName);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
            dgvItemtable.Rows.Clear();
            lblTotal.Text = "";
            new paymentgateway().ShowDialog();
        }
    }
}
