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

            peopleListBox.DataSource = people;
            peopleListBox.DisplayMember = "FullInfo";
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var dataAccess = new DataAccess();
            people = dataAccess.GetPeople(txtLastName.Text);
        }
    }
}
