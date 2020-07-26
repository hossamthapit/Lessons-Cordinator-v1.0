using Lessons_Cordinator_v1._0.Forms;
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

namespace Lessons_Cordinator_v1._0 {
    public partial class Home : Form {
        public Home() {
            InitializeComponent();
            setUp();
        }
        void setUp() {
            foreach (Models.Day d in Enum.GetValues(typeof(Models.Day))) {
                comboBoxDay.Items.Add(d);
            }
            foreach (Gender d in Enum.GetValues(typeof(Gender))) {
                genderCombo.Items.Add(d);
            }
            DataContext data = new DataContext();
            return; //////////  remove when connect the server
            foreach (Models.Groups group in data.groups.ToList()) {
                string groupName = group.gender.ToString() + "  " + group.day.ToString();
                groupName += "  " + group.hour.ToString() + ":" + group.minutes.ToString();
                groupCombo.Items.Add(groupName);
            }
        }

        void getStudents() {
            DataContext data = new DataContext();
            List<Student> Students = data.students.ToList();
            List<Groups> _groups = data.groups.ToList();
            foreach (Student st in Students) {
                gridViewStudents.Rows.Add(st.name, st.age, st.school, st.phone1, st.phone2,
                    _groups.FirstOrDefault(m => m.ID == st.groupID).ToString(), st.gender.ToString());
            }
            bool color = true;
            foreach (DataGridViewRow row in gridViewStudents.Rows) {
                if (color) row.DefaultCellStyle.BackColor = Color.LightGray;
                else row.DefaultCellStyle.BackColor = Color.LightBlue;
                color = !color;
            }
        }

        void getgroups() {
            List<Groups> _groups = new DataContext().groups.ToList();
            foreach (Groups g in _groups)
                groupsGridView.Rows.Add(g.day.ToString(), g.hour.ToString() + ":" + g.minutes.ToString(),
                    g.gender.ToString());

            bool color = true;
            foreach (DataGridViewRow row in groupsGridView.Rows) {
                if (color) row.DefaultCellStyle.BackColor = Color.LightGray;
                else row.DefaultCellStyle.BackColor = Color.LightBlue;
                color = !color;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e) {
            groupCombo.Items.Clear();
            List<Groups > _groups = new DataContext().groups.ToList();
            foreach (Groups g in _groups)
                groupCombo.Items.Add(g.day.ToString() + ", "+ g.hour.ToString() + ":" + g.minutes.ToString()+ " "+g.gender.ToString());

            tabControl.SelectedIndex = 1;
        //    new Add_Student().ShowDialog();
        }

        private void studentsBtn_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 2;
            gridViewStudents.Rows.Clear();
            getStudents();
        //    new Students().ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 0;
        //    new Add_Group().ShowDialog();
        }

        private void groupsBtn_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 3;
            groupsGridView.Rows.Clear();
            getgroups();
            //new Group().ShowDialog();
        }

        private void Home_Load(object sender, EventArgs e) {
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
        }

        private void picAddStudent_Click(object sender, EventArgs e) {
            DataContext data = new DataContext();
            List<Groups> _groups = data.groups.ToList();
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
            MessageBox.Show("تمت اضافه الطالب/ه بنجاح");
        }

        private void newGroupPic_Click(object sender, EventArgs e) {
            DataContext data = new DataContext();
            Gender _gender = (Gender)Enum.Parse(typeof(Gender), genderCombo.Text);
            Models.Day _day = (Models.Day)Enum.Parse(typeof(Models.Day), comboBoxDay.Text);

            Groups gr = new Groups {
                hour = int.Parse(hourBox.Text),
                minutes = int.Parse(minutesBox.Text),
                gender = _gender,
                day = _day
            };
            data.groups.Add(gr);
            data.SaveChanges();
            MessageBox.Show("تمت اضافه المجموعه بنجاح");
            setUp();
        }

        private void students_Click(object sender, EventArgs e) {

        }

        private void absenceBtn_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 4;
            ListBoxAbsence.Items.Clear();
            comboBoxGroups.Items.Clear();

            DataContext data = new DataContext();
            foreach (Groups group in data.groups.ToList()) {
                string groupName = group.gender.ToString() + "  " + group.day.ToString();
                groupName += "  " + group.hour.ToString() + ":" + group.minutes.ToString();
                comboBoxGroups.Items.Add(groupName);
            }

        }

        private void comboBoxGroups_SelectedIndexChanged(object sender, EventArgs e) {
            ListBoxAbsence.Items.Clear();
            DataContext data = new DataContext();
            Groups group = data.groups.ToList()[comboBoxGroups.SelectedIndex];

            List<Student> list = data.students.Where(m => m.groupID == group.ID).ToList();
            foreach (Student stu in list) 
                ListBoxAbsence.Items.Add(stu.name);
        }

        private void saveAbsence_Click(object sender, EventArgs e) {
            DataContext data = new DataContext();
            Groups group = data.groups.ToList()[comboBoxGroups.SelectedIndex];
            List<Student> list = data.students.Where(m => m.groupID == group.ID).ToList();

            Dictionary<int, bool> isAbsent = new Dictionary<int, bool>();
            foreach (var item in ListBoxAbsence.CheckedIndices) {
                isAbsent.Add(int.Parse(item.ToString()),false);
            }
            for(int i = 0; i < list.Count;i++) {
                absencePair pair;
                pair.date = dateTimePicker1.Value;
                if (isAbsent.ContainsKey(i))
                    pair.absent = false;
                else
                    pair.absent = true;
                if (list[i].absence == null) {
                    list[i].absence = new List<absencePair>();
                }
                list[i].absence.Add(pair);
            }
        }
        
        private void groupCombo_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
