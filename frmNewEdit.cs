using System;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class frmNewEdit : Form
    {
        EventsEntities db;

        public frmNewEdit(EventsTable obj)
        {
            InitializeComponent();
            db = new EventsEntities();
            if (obj == null)
            {
                eventsTableBindingSource.DataSource = new EventsTable();
                db.EventsTable.Add(eventsTableBindingSource.Current as EventsTable);
            }
            else
            {
                eventsTableBindingSource.DataSource = obj;
                db.EventsTable.Attach(eventsTableBindingSource.Current as EventsTable);
            }
        }

        private void frmNewEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(txtEventType.Text))
                {
                    MessageBox.Show("Please enter Event Type.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEventType.Focus();
                    e.Cancel = true;
                    return;
                }
                db.SaveChanges(); //saving changes to new or edited Event if pass above condition
                e.Cancel = false;
            }
            e.Cancel = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allowing only numbers in contact number field.
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                MessageBox.Show("This field accepts only numbers 0 - 9. \nMaximum 20 digits.", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
    }
}
