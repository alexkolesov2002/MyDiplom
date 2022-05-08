using System;
using System.Collections.Generic;
using System.Linq;

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

                        journal.equipments.CountInEvent = (int)journal.count_equipment;
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
        public static dynamic getlistEquipmentsAndWork(workshops workshop)
        {
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.id_workshop == workshop.id_workshop).ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<equipments> equipments = new List<equipments>();
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    if (journal.equipments != null)
                    {

                        journal.equipments.CountInEvent = (int)journal.count_equipment;
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
        public static dynamic getlistEquipmentsAndWork(workshops workshop, DateTime s, DateTime po)
        {
            po = po.AddDays(1);
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.date_use > s && x.date_use < po && x.id_workshop == workshop.id_workshop).ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<equipments> equipments = new List<equipments>();
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    if (journal.equipments != null)
                    {

                        journal.equipments.CountInEvent = (int)journal.count_equipment;
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
        public static dynamic getlistEquipments(DateTime s, DateTime po)
        {
            po = po.AddDays(1);
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.date_use > s && x.date_use < po).ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<equipments> equipments = new List<equipments>();
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    if (journal.equipments != null)
                    {

                        journal.equipments.CountInEvent = (int)journal.count_equipment;
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
        public static dynamic getlistEquipments(int index)
        {
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.id_user == index).ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<equipments> equipments = new List<equipments>();
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    if (journal.equipments != null)
                    {

                        journal.equipments.CountInEvent = (int)journal.count_equipment;
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
        public static dynamic getlistEquipments(int index, DateTime s, DateTime po)
        {
            po = po.AddDays(1);
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.Where(x => x.id_user == index && (x.date_use > s && x.date_use < po)).ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;
            foreach (var ItemGroup in GroupJournal)
            {
                List<equipments> equipments = new List<equipments>();
                foreach (journal_use_workshop journal in ItemGroup)
                {
                    if (journal.equipments != null)
                    {

                        journal.equipments.CountInEvent = (int)journal.count_equipment;
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
