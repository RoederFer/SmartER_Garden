// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartER_Garden_Library.SmartER_Garden_Models
{
    public partial class Standort
    {
        public Standort(object id, string name, string beschreibung)
        {
            Id = id;
            Name = name;
            Beschreibung = beschreibung;
        }

        public int Stid { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public int Pfid { get; set; }

        public virtual Pflanze Pf { get; set; }
        public object Id { get; }
    }
}