﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OthersFeatures
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Edm_v6Context : DbContext
    {
        public Edm_v6Context()
            : base("name=Edm_v6Context")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Enrollement> Enrollements { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<myView> myViews { get; set; }
        public virtual DbSet<ETUDIANT> ETUDIANTs { get; set; }
    
        [DbFunction("Edm_v6Context", "GetStudentGradesForCourse")]
        public virtual IQueryable<GetStudentGradesForCourse_Result> GetStudentGradesForCourse(Nullable<int> courseID)
        {
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetStudentGradesForCourse_Result>("[Edm_v6Context].[GetStudentGradesForCourse](@CourseID)", courseIDParameter);
        }
    
        public virtual ObjectResult<GetStudentGrades_Result> GetStudentGrades(Nullable<int> studentID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStudentGrades_Result>("GetStudentGrades", studentIDParameter);
        }
    }
}
