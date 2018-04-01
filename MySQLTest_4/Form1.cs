using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MySQLTest_4
{
    public partial class Form1 : Form
    {
        private int dgvCellRowIndex = -2;
        private int dgvCellColumnIndex = -2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = MyDatabase.GetDataSetBySql("select * from datainfo;");
            MyData.DataConvert(dt);
            dataGridView1.AllowUserToAddRows = false;//不允许用户添加新行
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvCellRowIndex = e.RowIndex;
                dgvCellColumnIndex = e.ColumnIndex;
                if (e.RowIndex >= 0 && e.ColumnIndex>=0)//选中的为纯数据区
                {
                    dataGridView1.ClearSelection();
                    //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;//设置为选中状态
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];//设置活动单元格
                    导出时删除此行ToolStripMenuItem.Enabled = true;
                    导出时删除此列ToolStripMenuItem.Enabled = true;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);//弹出操作菜单
                }
                else if (e.RowIndex < 0 && e.ColumnIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                    dataGridView1.Columns[e.ColumnIndex].Selected = true;//设置为选中状态
                    //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];//设置活动单元格
                    导出时删除此行ToolStripMenuItem.Enabled = false;
                    导出时删除此列ToolStripMenuItem.Enabled = true;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);//弹出操作菜单
                }
                else if (e.RowIndex >= 0 && e.ColumnIndex < 0)
                {
                    dataGridView1.ClearSelection();
                    //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.Rows[e.RowIndex].Selected = true;//设置为选中状态
                    //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];//设置活动单元格
                    导出时删除此行ToolStripMenuItem.Enabled = true;
                    导出时删除此列ToolStripMenuItem.Enabled = false;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);//弹出操作菜单
                }
                else if (e.RowIndex < 0 && e.ColumnIndex < 0)
                {
                    dataGridView1.ClearSelection();
                    //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;//设置为选中状态
                    //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];//设置活动单元格
                    导出时删除此行ToolStripMenuItem.Enabled = false;
                    导出时删除此列ToolStripMenuItem.Enabled = false;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);//弹出操作菜单
                }
            }
        }

        private void 导出时删除此行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            dataGridView1.Rows.RemoveAt(dgvCellRowIndex);
        }

        private void 导出时删除此列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.RemoveAt(dgvCellColumnIndex);
            //dataGridView1.Columns.RemoveAt(dataGridView1.CurrentCell.ColumnIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = MyData.GetDgvToTable(dataGridView1);
            MyData.DataConvert(dt);
            dataGridView2.AllowUserToAddRows = false;//不允许用户添加新行
            dataGridView2.DataSource = dt;
        }
    }
}
