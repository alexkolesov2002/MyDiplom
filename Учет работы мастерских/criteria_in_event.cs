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
    
    public partial class criteria_in_event
    {
        public int id_criteria_in_event { get; set; }
        public Nullable<int> id_criterion { get; set; }
        public int id_event { get; set; }
        public Nullable<int> id_student { get; set; }
        public Nullable<int> rating { get; set; }
    
        public virtual criteria criteria { get; set; }
        public virtual events events { get; set; }
        public virtual students students { get; set; }
    }
}
