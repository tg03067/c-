using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 연습용콘솔앱.Models
{
    public class Name
    {
        public int id { get; set; }
        public string name { get; set; }


        public Name(int i, string name)
        {
            this.id = i;
            this.name = name;
        }
        public Name()
        {

        }
    }
}
