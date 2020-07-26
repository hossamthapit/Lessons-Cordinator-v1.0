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
    public partial class Students : Form {
        public Students() {
            InitializeComponent();
            setUp();
        }

        void setUp() {
            DataContext data = new DataContext();
            List<Student> Students = data.students.ToList();
            List < Groups > _groups = data.groups.ToList();
            foreach (Student st in Students) {
                gridViewStudents.Rows.Add(st.name, st.age, st.school, st.phone1, st.phone2, 
                    _groups.FirstOrDefault(m=>m.ID == st.groupID).ToString(),st.gender.ToString());
            }
            bool color = true;
            foreach (DataGridViewRow row in gridViewStudents.Rows) {
                if (color) row.DefaultCellStyle.BackColor = Color.LightGray;
                else row.DefaultCellStyle.BackColor = Color.LightBlue;
                color = !color;
            }
        }

        private void gridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }
    }
}
