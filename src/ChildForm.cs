using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Modul
{
    public partial class ChildForm : Form
    {
        string TableName;
        SQLiteConnection conn;
        List<string> ColumnNames;
        private TextBox[] textBoxes;

        public ChildForm(string TableName, SQLiteConnection conn)
        {
            InitializeComponent();
            this.TableName = TableName;
            this.conn = conn;
            GetTable();
        }

        private void GetTable()
        {
            SQLiteCommand com = new SQLiteCommand(String.Format("select * from {0};", TableName), conn);
            var reader = com.ExecuteReader();
            int count = reader.FieldCount;
            this.ColumnNames = new List<string>();
            for (int i = 0; i < count; i++)
            {
                reader.Read();
                string name = reader.GetName(i);
                dataGridView1.Columns.Add(name, name);
                ColumnNames.Add(name);
            }

            FillingTable();
        }

        private void FillingTable()
        {
            SQLiteCommand com1 = new SQLiteCommand(String.Format("select * from {0};", TableName), conn);
            var reader1 = com1.ExecuteReader();
            int NumRow = 0;
            while (reader1.Read())
            {
                dataGridView1.Rows.Add();
                string[] row = new string[ColumnNames.Count()];
                for (int i = 0; i < ColumnNames.Count(); i++)
                {
                    row[i] = Convert.ToString(reader1[ColumnNames[i]]);
                    dataGridView1.Rows[NumRow].Cells[i].Value = row[i];
                }

                NumRow++;
            }
        }

        //add
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form add = new Form();
            Label[] arr = new Label[this.ColumnNames.Count()];
            textBoxes = new TextBox[this.ColumnNames.Count()];

            for (int i = 0; i < this.ColumnNames.Count(); i++)
            {
                arr[i] = new Label();
                arr[i].Text = ColumnNames[i];
                arr[i].Location = new Point(10, 20 + i * 30);

                textBoxes[i] = new TextBox();
                textBoxes[i].Location = new Point(90, 20 + i * 30);

                add.Controls.Add(textBoxes[i]);
                add.Controls.Add(arr[i]);
            }
            Button ok = new Button();
            ok.Text = "Ok";
            ok.Click += new EventHandler(add_click);
            ok.Location = new Point(50, 20 + this.ColumnNames.Count() * 30);
            add.Controls.Add(ok);

            add.ShowDialog();
        }
        private void add_click(object sender, EventArgs e)
        {
            string columns = "";
            string values = "";
            for (int i = 0; i < ColumnNames.Count(); i++)
            {
                if (textBoxes[i].Text == "")
                {
                    MessageBox.Show("Not all values!");
                    return;
                }
                if (i == ColumnNames.Count() - 1)
                {
                    columns += ColumnNames[i];
                    values += "'" +  textBoxes[i].Text + "'";
                    break;
                }
                columns += ColumnNames[i] + ", ";
                values += "'" + textBoxes[i].Text + "', ";
            }
            SQLiteCommand com = new SQLiteCommand(String.Format("insert into {2} ({0}) values ({1});", columns, values, TableName), conn);
            try
            {
                com.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ne");
            }
            dataGridView1.Rows.Clear();
            FillingTable();
            //this.Close();
        }

        //del
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        //update
        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string name = ((ChildForm)sender).Text;
            MainForm parent = (MainForm)this.MdiParent;

            for (int i = 0; i < parent.toolStripButton2.DropDownItems.Count; i++)
            {
                if (parent.toolStripButton2.DropDownItems[i].Text == name)
                {
                    parent.toolStripButton2.DropDownItems.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
