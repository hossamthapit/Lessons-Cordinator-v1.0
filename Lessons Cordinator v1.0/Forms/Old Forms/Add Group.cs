using Lessons_Cordinator_v1._0;
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
    public partial class Add_Group : Form {
        public Add_Group() {
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

        private void addSGroupbtn_Click(object sender, EventArgs e) {
            DataContext data = new DataContext();
            Gender _gender = (Gender)Enum.Parse(typeof(Gender),genderCombo.Text);
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
        }
    }
}
