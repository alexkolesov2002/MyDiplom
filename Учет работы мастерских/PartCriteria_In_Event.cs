using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Учет_работы_мастерских
{
    partial class criteria_in_event

    {
        public List<criteria> listCriterial { get; set; } = new List<criteria>();
        public List<students> listStudent { get; set; } = new List<students>();

        public static dynamic getlistCriteria()
        {
            List<criteria_in_event> FullListCriteria = BaseModel.BaseConnect.criteria_in_event.ToList();
            var GroupJournal = from Event in FullListCriteria group Event by Event.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<criteria> equipments = new List<criteria>();
                List<students> students = new List<students>(); 
                foreach (criteria_in_event journal in ItemGroup)
                {
                    if (journal.criteria != null )
                    {
                        equipments.Add(journal.criteria);
                    }
                    if (journal.students != null)
                    {
                        students.Add(journal.students);
                    }
                }
                foreach (criteria_in_event journal in ItemGroup)
                {
                    journal.listCriterial = equipments;
                    journal.listStudent = students;
                }
            }
            return GroupJournal;

        }
    }
}
