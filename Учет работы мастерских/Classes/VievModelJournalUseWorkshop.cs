using System.Collections.Generic;
using System.Linq;

namespace Учет_работы_мастерских
{
    internal class VievModelJournalUseWorkshop
    {
        public List<journal_use_workshop> WorkshopsList()
        {
            try
            {

           
            List<journal_use_workshop> journal_Use_Workshops = BaseModel.BaseConnect.journal_use_workshop.ToList();
            var GroupJournal = from journal in journal_Use_Workshops group journal by journal.id_event;

            foreach (journal_use_workshop item in GroupJournal)
            {



            }

            return (List<journal_use_workshop>)GroupJournal;
            }
            catch
            {
                return null;
            }
        }
    }
}
