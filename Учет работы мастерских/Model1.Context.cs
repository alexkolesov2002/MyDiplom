﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<equipments> equipments { get; set; }
        public virtual DbSet<Equipments_In_Workshop> Equipments_In_Workshop { get; set; }
        public virtual DbSet<events> events { get; set; }
        public virtual DbSet<journal_use_workshop> journal_use_workshop { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<types_equipment> types_equipment { get; set; }
        public virtual DbSet<types_event> types_event { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<workshops> workshops { get; set; }
    }
}
