using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace food_ordering
{
    public partial class paymentgateway : Form
    {
        public paymentgateway()
        {
            InitializeComponent();
            thankyou1.Visible = false;
        }
        MySqlConnection conn;
        public void maincon()
        {
            string sql_conn = "server=localhost;user=root;database=munchbar_db;port=3306";
            conn = new MySqlConnection(sql_conn);
            conn.Open();
        }
        private void payBtn_Click(object sender, EventArgs e)
        {
            thankyou1.Visible = true;
            thankyou1.BringToFront();
            maincon();
            double total = 0;
            int id = 0;
            try
            {

                //taking amount
                string amount_sum = "SELECT SUM(amount) FROM temp_bill";
                MySqlCommand cmd_amount = new MySqlCommand(amount_sum, conn);
                MySqlDataReader reader_amount = cmd_amount.ExecuteReader();
                if (reader_amount.Read())
                {
                    total = reader_amount.GetDouble(0);
                }
                reader_amount.Close();

                //inserting orders table

                DateTime now = DateTime.Now;
                string formattedDate = now.ToString("yyyy-MM-dd HH:mm:ss");

                string order_insert = "INSERT INTO orders(amount,date) VALUES(@total,@date)";
                MySqlCommand cmd_insert = new MySqlCommand(order_insert, conn);
                cmd_insert.Parameters.AddWithValue("@total", total);
                cmd_insert.Parameters.AddWithValue("@date", formattedDate);
                cmd_insert.ExecuteNonQuery();

                //taking id of order

                string id_order = "SELECT id FROM orders WHERE date = @date";
                MySqlCommand cmd_id = new MySqlCommand(@id_order, conn);
                cmd_id.Parameters.AddWithValue("@date", formattedDate);
                MySqlDataReader id_reader = cmd_id.ExecuteReader();
                if (id_reader.Read())
                {
                    id = id_reader.GetInt32(0);
                }
                id_reader.Close();

                //selecting order details from temp_bill
                // Fetch data from temp_bill
                string connectionString = "server=localhost;user=root;database=munchbar_db;port=3306";

                using (MySqlConnection conn1 = new MySqlConnection(connectionString))
                {
                    conn1.Open();
                    string order_details = "SELECT itemname, quantity FROM temp_bill";

                    using (MySqlCommand cmd = new MySqlCommand(order_details, conn1))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString("itemname");
                                int quantity = reader.GetInt32("quantity");

                                // Insert into order_details
                                using (MySqlConnection conn2 = new MySqlConnection(connectionString))
                                {
                                    conn2.Open();
                                    string insert_order_details = "INSERT INTO order_details (id, itemname, quantity) VALUES (@id, @itemname, @quantity)";

                                    using (MySqlCommand cmd_order_details = new MySqlCommand(insert_order_details, conn2))
                                    {
                                        cmd_order_details.Parameters.AddWithValue("@id", id); // Ensure 'id' is defined
                                        cmd_order_details.Parameters.AddWithValue("@itemname", name);
                                        cmd_order_details.Parameters.AddWithValue("@quantity", quantity);

                                        // Execute the insert command
                                        cmd_order_details.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }


                //clear temp_bill

                string del_temp = "DELETE FROM temp_bill";
                MySqlCommand cmd_del = new MySqlCommand(del_temp, conn);
                cmd_del.ExecuteNonQuery();

                MessageBox.Show("This is your order Id:" + id,"Order Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
