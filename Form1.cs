using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class Form1 : Form
    {

        EventsEntities db;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e) => RefreshEvents();
        


        #region Button Controls

        //btn New
        private void btnNew_Click(object sender, EventArgs e)
        {
            using (frmNewEdit frm = new frmNewEdit(null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    eventsTableBindingSource.DataSource = db.EventsTable.ToList();
                }
                RefreshEvents();
            }
        }

        //btn Edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (eventsTableBindingSource.Current == null)
                return;
            using (frmNewEdit frm = new frmNewEdit(eventsTableBindingSource.Current as EventsTable))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    eventsTableBindingSource.DataSource = db.EventsTable.ToList();
                RefreshEvents();
            }
        }

        //btn Delete and Try catch example(catch was called when trying to delete when no records)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    db.EventsTable.Remove(eventsTableBindingSource.Current as EventsTable);
                    eventsTableBindingSource.RemoveCurrent();
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("There is nothing to Delete", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //btn Close App
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion


        #region Filter Buttons

        private void btnFEventType_Click(object sender, EventArgs e)
        {
            eventsTableBindingSource.DataSource = db.EventsTable.OrderBy(et => et.Event_Type).ToList();
        }

        private void btnFDate_Click(object sender, EventArgs e)
        {
            RefreshEvents();
        }

        private void btnFContactName_Click(object sender, EventArgs e)
        {
            eventsTableBindingSource.DataSource = db.EventsTable.OrderBy(cn => cn.Contact_Name).ToList();
        }

        private void btnFLocation_Click(object sender, EventArgs e)
        {
            eventsTableBindingSource.DataSource = db.EventsTable.OrderBy(l => l.Location).ToList();
        }

        #endregion

        // set all records in the table( closest date first and so on)
        // used when opening the app
        // also used for date filtering in filter buttons region
        private void RefreshEvents()
        {
            db = new EventsEntities();
            eventsTableBindingSource.DataSource = db.EventsTable.OrderBy(d => d.Date).ToList();
        }
    }
}
