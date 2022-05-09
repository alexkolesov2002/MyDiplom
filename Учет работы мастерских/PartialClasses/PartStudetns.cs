using System.Collections.Generic;
using System.Linq;

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


       // public List<groups> listgroup { get; set; } = BaseModel.BaseConnect.groups.ToList();

    }
}
