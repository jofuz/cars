using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CarsDatabase
{
    public partial class frmSearch : Form
    {
        OleDbConnection connection = new OleDbConnection();
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Hire.mdb";
        string queryString;
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();

        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            // TODO: This loads data into the 'hireDataSet.tblCar' table.
            this.tblCarTableAdapter.Fill(this.hireDataSet.tblCar);
        }

        // Closes the search form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Executes search command
        private void btnRun_Click(object sender, EventArgs e)
        {
            // Field, operator and value text field inputs are stored in these variables
            string field = cboField.Text;
            string op = cboOperator.Text;
            string searchValue = deValue.Text;

            // if the fields are not empty run this statement
            if (field != "" && op != "" && searchValue != "")
            {

                // this statement checks the field value to change the database query that is used.
                switch (field)
                {
                    case "Make":
                        connection = new OleDbConnection(connectionString);
                        queryString = "SELECT * FROM tblCar WHERE Make LIKE '%" + searchValue + "%';";
                        myAdapter = new OleDbDataAdapter(queryString, connection);
                        break;
                    case "EngineSize":
                        connection = new OleDbConnection(connectionString);
                        queryString = "SELECT * FROM tblCar WHERE EngineSize LIKE '%" + searchValue + "%';";
                        myAdapter = new OleDbDataAdapter(queryString, connection);
                        break;
                    case "RentalPerDay":
                        connection = new OleDbConnection(connectionString);
                        queryString = "SELECT * FROM tblCar WHERE RentalPerDay "  + op + searchValue;
                        myAdapter = new OleDbDataAdapter(queryString, connection);
                        break;
                    case "Available":
                        connection = new OleDbConnection(connectionString);
                        switch (searchValue) { 
                            case "true":
                                queryString = "SELECT * FROM tblCar WHERE Available";
                                break;
                            case "True":
                                queryString = "SELECT * FROM tblCar WHERE Available";
                                break;
                            case "yes":
                                queryString = "SELECT * FROM tblCar WHERE Available";
                                break;
                            case "Yes":
                                queryString = "SELECT * FROM tblCar WHERE Available";
                                break;
                            case "1":
                                queryString = "SELECT * FROM tblCar WHERE Available";
                                break;
                            case "false":
                                queryString = "SELECT * FROM tblCar WHERE not Available";
                                break;
                            case "False":
                                queryString = "SELECT * FROM tblCar WHERE not Available";
                                break;
                            case "no":
                                queryString = "SELECT * FROM tblCar WHERE not Available";
                                break;
                            case "No":
                                queryString = "SELECT * FROM tblCar WHERE not Available";
                                break;
                            case "0":
                                queryString = "SELECT * FROM tblCar WHERE not Available";
                                break;
                            default:
                                queryString = "SELECT * FROM tblCar WHERE Available";
                                break;
                        }
                        myAdapter = new OleDbDataAdapter(queryString, connection);                        
                        break;
                    default:
                        connection = new OleDbConnection(connectionString);
                        queryString = "SELECT * FROM tblCar WHERE " + field + op + searchValue;
                        myAdapter = new OleDbDataAdapter(queryString, connection);
                        break;
                }

                DataTable dt = new DataTable();
                myAdapter.Fill(dt);
                tblCarDataGridView.DataSource = dt;
            }
            else 
            {
                MessageBox.Show("Fields cannot be blank");
            }
        }
    }
}
