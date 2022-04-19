using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Models;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("You have to enter first name and last name", "Missing data");
            }
            else
            {
                string firstName = textBox1.Text;
                string middleName = textBox2.Text;
                string lastName = textBox3.Text;
                var employees = new List<Employee>();
                var context = new SoftUniContext();

                if (middleName == "")
                {
                    employees = context.Employees
                        .Where(x => x.FirstName == firstName && string.IsNullOrEmpty(x.MiddleName) && x.LastName == lastName)
                        .Take(1)
                        .ToList();
                }
                else
                {
                    employees = context.Employees
                    .Where(x => x.FirstName == firstName && x.MiddleName == middleName && x.LastName == lastName)
                    .Take(1)
                    .ToList();
                }
                if (employees.Count == 0)
                {
                    MessageBox.Show("There is no employee with this name.", "No results");
                }
                else
                {
                    Hide();
                    Form2 form2 = new Form2(employees[0]);
                    form2.Show();
                }
            }
        }
    }
}
