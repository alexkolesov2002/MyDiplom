//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Учет_работы_мастерских
{
    using System;
    using System.Collections.Generic;
    
    public partial class journal_use_workshop
    {
        public int id_journal_use_workshop { get; set; }
        public int id_user { get; set; }
        public int id_workshop { get; set; }
        public Nullable<int> id_equipment { get; set; }
        public Nullable<int> count_equipment { get; set; }
        public System.DateTime date_use { get; set; }
        public Nullable<System.DateTime> date_end_use { get; set; }
        public Nullable<int> id_event { get; set; }
    
        public virtual equipments equipments { get; set; }
        public virtual events events { get; set; }
        public virtual users users { get; set; }
        public virtual workshops workshops { get; set; }
    }
}
