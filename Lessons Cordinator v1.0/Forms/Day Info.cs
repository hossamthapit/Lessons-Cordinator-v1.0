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
    public partial class Day_Info : Form {
        int id;
        DateTime date;
        DataContext data = new DataContext();

        public Day_Info(int id , DateTime date) {
            InitializeComponent();
            this.id = id;
            this.date = date;
            textBoxFormStuName.Text = data.students.FirstOrDefault(st => st.ID == id).name;
        }

        private void btnFormSave_Click(object sender, EventArgs e) {
            
            dayInformation todayInf = data.dayInfoList.Where(inf => inf.studentID == id && inf.date.Year == date.Year && 
                inf.date.Month == date.Month && inf.date.Day == date.Day).FirstOrDefault();

            int isNew = 0;

            if (todayInf == null) {
                todayInf = new dayInformation();
                isNew = 1;
            }

            todayInf.absent = checkBoxFormAbsentCheckBox.Checked;
            todayInf.note = textBoxFormNote.Text;

            try {
                todayInf.mark = double.Parse(textBoxFormDegree.Text);
            }
            catch {
                todayInf.mark = 0;
            }


            if (isNew == 1) {
                todayInf.studentID = id;
                todayInf.date = date;
                data.dayInfoList.Add(todayInf);
            }
            data.SaveChanges();
            this.Close();            
        }

        private void label2_Click(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
