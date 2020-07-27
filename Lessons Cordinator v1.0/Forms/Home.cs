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
        DataContext data = new DataContext();
        Student editableStudent = new Student();
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

        }

        void getStudents(string str) {

            gridViewStudents.Rows.Clear();
            List<Student> Students = data.students.Where(st => st.name.Contains(str) == true).ToList();
            List<Groups> _groups = data.groups.ToList();

            foreach (Student st in Students) {

                if(st.absence == null) {
                    st.absence = new List<absencePair>();
                    st.absence.Add(new absencePair() { absent = true , date = DateTime.Now});
                }
                var info = data.dayInfoList.ToList();

                gridViewStudents.Rows.Add(st.name, st.phone1, st.phone2,
                    _groups.FirstOrDefault(m => m.ID == st.groupID).ToString(),
                    info.Count(inf => inf.studentID == st.ID && inf.absent == false) , "معلومات", "تعديل");
            }
            bool color = true;
            foreach (DataGridViewRow row in gridViewStudents.Rows) {
                if (color) row.DefaultCellStyle.BackColor = Color.LightGray;
                else row.DefaultCellStyle.BackColor = Color.LightBlue;
                color = !color;
            }
        }

        void getgroups() {
            List<Groups> _groups = data.groups.ToList();
            foreach (Groups g in _groups)
                groupsGridView.Rows.Add(g.day.ToString(), g.hour.ToString() + ":" + g.minutes.ToString(),
                    g.gender.ToString() , data.students.Count(st => st.groupID == g.ID));

            bool color = true;
            foreach (DataGridViewRow row in groupsGridView.Rows) {
                if (color) row.DefaultCellStyle.BackColor = Color.LightGray;
                else row.DefaultCellStyle.BackColor = Color.LightBlue;
                color = !color;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e) {
            groupCombo.Items.Clear();
            List<Groups > _groups = data.groups.ToList();
            foreach (Groups g in _groups)
                groupCombo.Items.Add(g.day.ToString() + ", "+ g.hour.ToString() + ":" + g.minutes.ToString()+ " "+g.gender.ToString());

            tabControl.SelectedIndex = 1;
        }

        private void studentsBtn_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 2;
            dataGridViewStudentRecord.Rows.Clear();
            gridViewStudents.Rows.Clear();
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 0;
        }

        private void groupsBtn_Click(object sender, EventArgs e) {
            tabControl.SelectedIndex = 3;
            groupsGridView.Rows.Clear();
            getgroups();
        }

        private void Home_Load(object sender, EventArgs e) {
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;
        }

        private void picAddStudent_Click(object sender, EventArgs e) {

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
            data.students.Add(stu);
            data.SaveChanges();
            MessageBox.Show("تمت اضافه الطالب/ه بنجاح");
        }

        private void newGroupPic_Click(object sender, EventArgs e) {

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

            foreach (Groups group in data.groups.ToList()) {
                string groupName = group.gender.ToString() + "  " + group.day.ToString();
                groupName += "  " + group.hour.ToString() + ":" + group.minutes.ToString();
                comboBoxGroups.Items.Add(groupName);
            }
        }

        private void comboBoxGroups_SelectedIndexChanged(object sender, EventArgs e) {
            ListBoxAbsence.Items.Clear();

            Groups group = data.groups.ToList()[comboBoxGroups.SelectedIndex];

            List<Student> list = data.students.Where(m => m.groupID == group.ID).ToList();
            foreach (Student stu in list) 
                ListBoxAbsence.Items.Add(stu.name);
        }
        
        private void groupCombo_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void btnStudentsSearch_Click(object sender, EventArgs e) {
            getStudents(textBoxStudentsSearch.Text);

        }

        private void gridViewStudents_CellClick(object sender, DataGridViewCellEventArgs e) {

            comboBoxStuGroup.Items.Clear();

            List<Student> Students = data.students.Where(st => st.name.Contains(textBoxStudentsSearch.Text) == true).ToList();
            int col = e.RowIndex, row = e.ColumnIndex;
            Student stu = Students[col];
            editableStudent = stu;

            if (row == 6) {
                tabControl.SelectedIndex = 5;
                textBoxStuName.Text = stu.name;
                textBoxStuAge.Text = stu.age.ToString();
                textBoxStuAddress.Text = stu.address;
                textBoxStuFatherPhone.Text = stu.phone2;
                textBoxStuPhone.Text = stu.phone1;
                textBoxStuSchool.Text = stu.school;

                int selectMe = 0, cnt = 0;

                foreach (Groups group in data.groups.ToList()) {
                    string groupName = group.gender.ToString() + "  " + group.day.ToString();
                    groupName += "  " + group.hour.ToString() + ":" + group.minutes.ToString();
                    comboBoxStuGroup.Items.Add(groupName);
                    if (group.ID == stu.groupID)
                        selectMe = cnt;
                    cnt++;
                }
                data.SaveChanges();
            }
            else if (row == 5) {
                tabControl.SelectedIndex = 6;
                dataGridViewStudentRecord.Rows.Clear();
                foreach (var rec in data.dayInfoList.Where(inf => inf.studentID == stu.ID).ToList()) {

                    string date = rec.date.Day.ToString() + "/" + rec.date.Month.ToString() + "/" +
                        rec.date.Year.ToString();
                    dataGridViewStudentRecord.Rows.Add(date,rec.mark,rec.absent==true?"حاضر":"غائب",rec.note);
                }
            }
        }

        private void btnStuSavetData_Click(object sender, EventArgs e) {
            
            Student stu = editableStudent;

            stu.name = textBoxStuName.Text;
            stu.age = int.Parse(textBoxStuAge.Text);
            stu.address = textBoxStuAddress.Text;
            stu.phone2 = textBoxStuFatherPhone.Text;
            stu.phone1 = textBoxStuPhone.Text;
            stu.school = textBoxStuSchool.Text;
            stu.groupID = data.groups.ToList()[comboBoxStuGroup.SelectedIndex].ID;
            data.SaveChanges();
            MessageBox.Show("تم الحفظ");
        }

        private void marksBtn_Click(object sender, EventArgs e) {
            for(int i = 0; i < data.students.Count();i++)
                if(data.students.ToList()[0].absence!=null)
                    data.students.ToList()[0].absence.Clear();
        }

        private void ListBoxAbsence_SelectedIndexChanged_1(object sender, EventArgs e) {
            Groups group = data.groups.ToList()[comboBoxGroups.SelectedIndex];
            List<Student> list = data.students.Where(m => m.groupID == group.ID).ToList();
            new Day_Info((list[ListBoxAbsence.SelectedIndex].ID) , dateTimePicker1.Value).ShowDialog();
        }
    }
}
