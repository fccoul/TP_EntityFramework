//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        public Course()
        {
            this.Enrollements = new HashSet<Enrollement>();
        }
    
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }
    
        public virtual ICollection<Enrollement> Enrollements { get; set; }
    }
}
