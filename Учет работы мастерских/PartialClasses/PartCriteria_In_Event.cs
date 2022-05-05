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

        public static dynamic getlistCriteria(int index)
        {
            List<criteria_in_event> FullListCriteria = BaseModel.BaseConnect.criteria_in_event.Where(x => x.id_event == index).ToList();
            var GroupJournal = from Event in FullListCriteria group Event by Event.id_event;
            List<criteria_in_event> NewList = new List<criteria_in_event>();
            foreach (var ItemGroup in GroupJournal)
            {
                //List<criteria> equipments = new List<criteria>();
                //List<students> students = new List<students>(); 
                //foreach (criteria_in_event journal in ItemGroup)
                //{
                //    if (journal.criteria != null )
                //    {
                //        equipments.Add(journal.criteria);
                //    }
                //    if (journal.students != null)
                //    {
                //        students.Add(journal.students);
                //    }
                //}
                foreach (criteria_in_event journal in ItemGroup)
                {
                    //journal.listCriterial = equipments;
                    //journal.listStudent = students;
                    NewList.Add(journal);
                }

            }

            var GroupJournalNew = from Event in NewList group Event by Event.id_student;
            foreach (var ItemGroup in GroupJournalNew)
            {
                List<criteria> equipments = new List<criteria>();
                foreach (criteria_in_event criteria_In_Event in ItemGroup)
                {
                    if (criteria_In_Event.students != null)
                    {
                        criteria_In_Event.criteria.Rating = (int)criteria_In_Event.rating;
                        equipments.Add(criteria_In_Event.criteria);
                    }
                  
                        
                }
                foreach (criteria_in_event journal in ItemGroup)
                {
                    journal.listCriterial = equipments;
                }

                //criteria d = new criteria ();   
                //d.criteria_in_event
            }
            return GroupJournalNew;

        }
    }
}
