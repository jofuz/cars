using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDatabase
{
    public partial class frmCars : Form
    {
        // store the current and total record numbers
        int totalRecords;
        int currentRecord;

        // check if a temporary page has been created. 
        // Temporary pages are created when addBtn is executed.
        bool onTempPage = false;

        public frmCars()
        {
            InitializeComponent();
        }

        private void frmCars_Load(object sender, EventArgs e)
        {
            // This loads data into the 'hireDataSet.tblCar' table. 
            this.tblCarTableAdapter.Fill(this.hireDataSet.tblCar);
            totalRecords = tblCarBindingSource.Count;
            changeRecordNum();
        }

        // this updates the record numbers
        public void changeRecordNum()
        {
            recordTotal.Text = (currentRecord + 1) + " of " + (totalRecords);
        }


        // this creates a new record
        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                tblCarBindingSource.AddNew();
                currentRecord = totalRecords;
                totalRecords++;
                changeRecordNum();
                // Temporary page is created
                onTempPage = true;

                MessageBox.Show("New record created. Press the Update button to write to the database.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: \n" + ex);
            }
        }

        // this cancels the creation of a new file
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancel creating a new record?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                tblCarBindingSource.CancelEdit();
                if (onTempPage)
                {
                    currentRecord--;
                    totalRecords--;
                    changeRecordNum();
                    onTempPage = false;
                }
            }
        }

        // this deletes the record that is currently being viewed
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.tblCarBindingSource.RemoveCurrent();
                totalRecords--;
                totalRecords = tblCarBindingSource.Count;
                changeRecordNum();
                currentRecord--;
                onTempPage = false;
                MessageBox.Show("Record deleted. Press the Update button to confirm this change.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: \n" + ex);
            }
        }

        // this updates any changes made to the database table
        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.tblCarBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.hireDataSet);
                totalRecords = tblCarBindingSource.Count;
                currentRecord = 0;
                changeRecordNum();
                MessageBox.Show("Update Successful");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update Failed: \n" + ex);
            }
            
        }

        // this closes the application
        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // this navigates to the last record in the table
        private void lastBtn_Click(object sender, EventArgs e)
        {
            tblCarBindingSource.MoveLast();
            if (currentRecord != (totalRecords - 1))
            {
                currentRecord = totalRecords - 1;
                changeRecordNum();
            }
        }

        // this navigates to the next record in the table
        private void nextBtn_Click(object sender, EventArgs e)
        {
            this.tblCarBindingSource.MoveNext();
            totalRecords = tblCarBindingSource.Count;
            currentRecord ++;
            changeRecordNum();
        }

        // this navigates to the previous record in the table
        private void previousBtn_Click(object sender, EventArgs e)
        {
            tblCarBindingSource.MovePrevious();
            if (currentRecord >= 1) 
            {
                currentRecord--;
                changeRecordNum();
            }
        }

        // this navigates to the first record in the table
        private void firstBtn_Click(object sender, EventArgs e)
        {
            this.tblCarBindingSource.MoveFirst();
            totalRecords = tblCarBindingSource.Count;
            currentRecord = 0;
            changeRecordNum();
        }

        // this opens up a the search form in a new window
        private void searchBtn_Click(object sender, EventArgs e)
        {
            frmSearch sf = new frmSearch();
            sf.Show();
        }
    }
}
