//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogicLayer.DbLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class pm_user
    {
        public pm_user()
        {
            this.pm_rsvp = new HashSet<pm_rsvp>();
        }
    
        public int uid { get; set; }
        public string registrationId { get; set; }
        public string userType { get; set; }
        public string gender { get; set; }
        public string firstName { get; set; }
        public string otherName { get; set; }
        public string lastName { get; set; }
        public System.DateTime dateOfBirth { get; set; }
        public string email { get; set; }
        public System.DateTime lastVisit { get; set; }
    
        public virtual ICollection<pm_rsvp> pm_rsvp { get; set; }
    }
}