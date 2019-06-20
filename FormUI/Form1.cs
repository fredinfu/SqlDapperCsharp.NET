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
            txtSearch.Focus();
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
            peopleListBox.SelectedItems.Clear();
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

            CreatePerson();

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

        private void PeopleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (peopleListBox.SelectedItem == null)
            {
                btnCreate.Visible = true;
                btnRemove.Visible = false;
                btnUpdate.Visible = false;
                CleanPanel1();
                txtSearch.Focus();
                return;
            }
            var person = peopleListBox.SelectedItem;
            BindPersonUpdateForm((Person)person);
        }

        private void BindPersonUpdateForm(Person person)
        {
            txtFirstName.Text = person.FirstName;
            txtLastName.Text = person.LastName;
            txtEmailAddress.Text = person.EmailAddress;
            txtPhoneNumber.Text = person.PhoneNumber;
            idPerson.Text = person.Id.ToString();
            btnUpdate.Visible = true;
            btnCreate.Visible = false;
        }


        private void CreatePerson()
        {
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

        private void UpdatePerson()
        {
            var person = new Person
            {
                Id = int.Parse(idPerson.Text),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                EmailAddress = txtEmailAddress.Text,
                PhoneNumber = txtPhoneNumber.Text
            };

            var dataAccess = new DataAccess();
            people = dataAccess.UpdatePeople(person);
            txtSearch.Text = person.LastName;
            SearchByText();
            txtSearch.Focus();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!SubmitValidation())
            {
                MessageBox.Show("First Name, Last Name and Email cannot be empty!");
                txtFirstName.Focus();
                return;
            }

            UpdatePerson();
        }
    }
}
