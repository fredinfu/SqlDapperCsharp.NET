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
    }
}
