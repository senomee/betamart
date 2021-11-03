using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace betamart
{
    public partial class Form1 : Form
    { 
        //main window
        public Form1()
        {
            InitializeComponent();
        }
        //search
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                    dataGridView.DataSource = mainBindingSource;
                else
                {
                    var query = from x in this.appData.main
                                where x.nama.Contains(txtSearch.Text) || x.harga == txtSearch.Text || x.jumlah == txtSearch.Text || x.distri.Contains(txtSearch.Text)
                                select x;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }
        //Delete Key
        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) 
            {
                if (MessageBox.Show("Apa kamu yakin menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    mainBindingSource.RemoveCurrent();
            }
        }
        //delete button
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name== "delete")
            {
                if (MessageBox.Show("Apa kamu yakin menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    mainBindingSource.RemoveCurrent();
            }
        }
        //new
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNama.Focus();
                this.appData.main.AddmainRow(this.appData.main.NewmainRow());
                mainBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainBindingSource.ResetBindings(false);
            }
        }
        //edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtNama.Focus();
        }
        //cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            mainBindingSource.ResetBindings(false);
        }
        //save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                mainBindingSource.EndEdit();
                mainTableAdapter.Update(this.appData.main);
                panel.Enabled=false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainBindingSource.ResetBindings(false);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.main' table. You can move, or remove it, as needed.
            this.mainTableAdapter.Fill(this.appData.main);
            mainBindingSource.DataSource = this.appData.main;
        }
    }
}
