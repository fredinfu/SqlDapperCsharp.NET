using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI
{
    public partial class Form1 : Form
    {
        List<Person> people = new List<Person>();
        public Form1()
        {
            InitializeComponent();
            GetAllData();
        }

        private void GetAllData()
        {
            var dataAccess = new DataAccess();
            people = dataAccess.GetPeople();
            UpdatePeopleListBox();
        }

        private void UpdatePeopleListBox()
        {
            peopleListBox.DataSource = people;
            peopleListBox.DisplayMember = "FullInfo";
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                GetAllData();
                return;
            }

            SearchByText();
        }

        private void SearchByText()
        {
            var dataAccess = new DataAccess();
            people = dataAccess.GetPeopleByLastName(txtSearch.Text);
            UpdatePeopleListBox();
        }

        private void BtnStartCreatePerson_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            btnRemove.Visible = false;
            btnCreate.Visible = true;
            txtFirstName.Focus();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (!SubmitValidation())
            {
                MessageBox.Show("First Name, Last Name and Email cannot be empty!");
                txtFirstName.Focus();
                return;
            }

            var person = new Person
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                EmailAddress = txtEmailAddress.Text,
                PhoneNumber = txtPhoneNumber.Text
            };

            var dataAccess = new DataAccess();
            people = dataAccess.AddPeople(person);
            CleanPanel1();
            GetAllData();
            txtFirstName.Focus();
        }

        private bool SubmitValidation()
        {
            if ( String.IsNullOrWhiteSpace(txtFirstName.Text) || String.IsNullOrWhiteSpace(txtLastName.Text) || String.IsNullOrWhiteSpace(txtEmailAddress.Text) )
                return false;
            return true;
        }

        private void CleanPanel1()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailAddress.Text = "";
            txtPhoneNumber.Text = "";
        }
    }
}
