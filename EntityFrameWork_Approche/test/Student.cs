//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public Student()
        {
            this.Enrollements = new HashSet<Enrollement>();
        }
    
        public int StudentID { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public System.DateTime EnrollmentDate { get; set; }
    
        public virtual ICollection<Enrollement> Enrollements { get; set; }
    }
}