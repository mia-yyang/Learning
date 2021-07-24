using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository
    {
        Student Add(Student student);
        Student Delete(int id);
        Student Update(Student updateStudent);
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(int id);
    }
}
