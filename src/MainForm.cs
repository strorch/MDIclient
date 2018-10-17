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
    public partial class MainForm : Form
    {
        SQLiteConnection conn;

        public MainForm(List<string> lst, SQLiteConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            for (int i = 0; i < lst.Count(); i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = lst[i];
                item.Size = new System.Drawing.Size(180, 22);
                item.Text = lst[i];
                item.Click += new EventHandler(tableClick);
                toolStripButton3.DropDownItems.AddRange(new ToolStripItem[]{ item
                });
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tableClick(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Text;
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (MdiChildren[i].Text == name)
                {
                    MdiChildren[i].Activate();
                    return;
                }
            }
            ChildForm f = new ChildForm(name, conn);
            f.Text = name;
            f.MdiParent = this;
            f.Show();

            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = name;
            item.Size = new System.Drawing.Size(180, 22);
            item.Text = name;
            item.Click += new EventHandler(activateClick);
            toolStripButton2.DropDownItems.AddRange(new ToolStripItem[]{ item
                });
        }

        private void activateClick(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Text;
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (MdiChildren[i].Text == name)
                {
                    MdiChildren[i].Activate();
                    return;
                }
            }
        }
    }
}
