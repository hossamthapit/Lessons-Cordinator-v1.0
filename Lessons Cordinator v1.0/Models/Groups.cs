using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons_Cordinator_v1._0.Models {

    enum Day{
        السبت,الاحد,الاثنين,الثلاثاء,الاربع,الخميس,الجمعه
    }
    public enum Gender {
        اولاد,
        بنات
    }

    class Groups {
        [Key]
        public int ID { get; set; }
        public int hour { get; set; }
        public int minutes { get; set; }
        public Day day { get; set; }
        public Gender gender { get; set; }
        public Groups() { }
        public Groups(string name , int hour , int minutes , Day day , Gender gender) {
            this.hour = hour;
            this.minutes = minutes;
            this.day = day;
            this.gender = gender;
        }
        public override string ToString() {
            string groupName = gender.ToString();
            groupName += "     " + day.ToString();
            groupName += "     " + hour.ToString() + ":" + minutes.ToString();
            return groupName;
        }
    }
}
