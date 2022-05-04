using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Учет_работы_мастерских
{
    public partial class students
    {
        private string fullName;

        public string FullName
        {
            get { return surname + " " + name + " " + patronymic; }
            set { fullName = value; }
        }

    }
}
