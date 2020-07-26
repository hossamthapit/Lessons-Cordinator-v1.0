using Lessons_Cordinator_v1._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lessons_Cordinator_v1._0.Forms {
    public partial class Add_Student : Form {
        public Add_Student() {
            InitializeComponent();
            setUp();
        }
        void setUp() {
            DataContext data = new DataContext();
            foreach (Models.Groups group in data.groups.ToList()) {
                string groupName = group.gender.ToString()+ "  " + group.day.ToString();
                groupName += "  " + group.hour.ToString() + ":" + group.minutes.ToString();
                groupCombo.Items.Add(groupName);
            }
        }
        private void addStudentBtn_Click(object sender, EventArgs e) {
            DataContext data = new DataContext();
            List < Groups > _groups = data.groups.ToList();
            Student stu = new Student {
                name = nameBox.Text,
                address = addressBox.Text,
                age = int.Parse(ageBox.Text),
                phone1 = phone1Box.Text,
                phone2 = phone2Box.Text,
                school = schoolBox.Text,
                groupID = _groups[groupCombo.SelectedIndex].ID,
            };
            Groups g = data.groups.FirstOrDefault();
            data.students.Add(stu);
            data.SaveChanges();
            MessageBox.Show("تمت الاضافه بنجاح");
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void panel1_Paint(object sender, PaintEventArgs e) {

        }
    }
}
