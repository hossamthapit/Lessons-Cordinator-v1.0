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
    public partial class Group : Form {
        public Group() {
            InitializeComponent();
            setUp();
        }
        void setUp() {
            List<Groups> _groups = new DataContext().groups.ToList();
            foreach (Groups g in _groups)
                gridViewStudents.Rows.Add(g.day.ToString(), g.hour.ToString() + ":" + g.minutes.ToString(), 
                    g.gender.ToString());

            bool color = true;
            foreach (DataGridViewRow row in gridViewStudents.Rows) {
                if(color)row.DefaultCellStyle.BackColor = Color.LightGray;
                else row.DefaultCellStyle.BackColor = Color.LightBlue;
                color = !color;
            }

        }
        private void Groups_Load(object sender, EventArgs e) {

        }
    }
}
