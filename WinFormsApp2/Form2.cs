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
    public partial class Form2 : Form
    {
        private Employee employee;
        public Form2(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            label3.Visible = false;
            label1.Text = $"Hello, {employee.FirstName} {employee.LastName}";
            LoadCurrentProjects();
            toolStripMenuItem3.Visible = CheckIfManagaer();
        }
        private bool CheckIfManagaer()
        {
            var context = new SoftUniContext();
            int result = context.Employees
                .Where(x => x.ManagerId == employee.EmployeeId)
                .Count();

            return result > 0;
        }
        private void LoadAllProjects()
        {
            var context = new SoftUniContext();
            var projects = context.EmployeesProjects
                .Where(x => x.EmployeeId == employee.EmployeeId)
                .Select(x => new
                {
                    x.Project.Name,
                    x.Project.Description,
                    x.Project.StartDate
                })
                .ToList();
            if (projects.Count == 0)
            {
                label3.ForeColor = Color.Red;
                label3.Visible = true;
            }
            else
            {
                dataGridView1.DataSource = projects;
            }
        }
        private void LoadCurrentProjects()
        {
            var context = new SoftUniContext();
            var projects = context.EmployeesProjects
                .Where(x => x.EmployeeId == employee.EmployeeId)
                .Where(x => x.Project.EndDate == null)
                .Select(x => new
                {
                    x.Project.Name,
                    x.Project.Description,
                    x.Project.StartDate
                })
                .ToList();
            if (projects.Count == 0)
            {
                label3.ForeColor = Color.Red;
                label3.Visible = true;
            }
            else
            {
                dataGridView1.DataSource = projects;
            }
        }

        private void allProjectsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LoadAllProjects();
        }

        private void currentProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;

            LoadCurrentProjects();
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            dataGridView1.Visible = false;

            groupBox1.Visible = true;
            textBox1.Text = employee.FirstName;
            textBox2.Text = employee.MiddleName;
            textBox3.Text = employee.LastName;
            var context = new SoftUniContext();

            textBox4.Text = context.Employees
                .Where(x => x.EmployeeId == employee.EmployeeId)
                .Select(x => $"{x.Address.AddressText}, {x.Address.Town.Name}")
                .First();
            textBox5.Text = employee.JobTitle;
            textBox6.Text = context.Departments
                .Where(x => x.DepartmentId == employee.DepartmentId)
                .Select(x => x.Name)
                .First();
            textBox7.Text = context.Employees
                .Where(x => x.EmployeeId == employee.ManagerId)
                .Select(x => $"{x.FirstName} {x.LastName}")
                .First();
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
            label1.Text = "Your Employees: ";

            var context = new SoftUniContext();
            dataGridView1.DataSource = context.Employees
                .Where(x => x.ManagerId == employee.EmployeeId)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,

                }
                )
                .ToList();
        }
    }
}
