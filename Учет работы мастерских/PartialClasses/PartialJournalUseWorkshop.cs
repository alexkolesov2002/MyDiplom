using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Учет_работы_мастерских
{
    public partial class journal_use_workshop
    {

        public List<equipments> listEquipmentsInEvents { get; set; } = new List<equipments>();

        public static dynamic getlistEquipments()
        {
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<equipments> equipments = new List<equipments>();
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    if (journal.equipments != null)
                    {
                       
                        journal. equipments.CountInEvent = (int)journal.count_equipment;
                        equipments.Add(journal.equipments);
                    }
                }
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    journal.listEquipmentsInEvents = equipments;
                }
            }

            return GroupJournal;
        }
    }
}
