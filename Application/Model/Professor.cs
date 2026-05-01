using System;

namespace Model
{
    public class Professor
    {
        public int ProfessorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Titulo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}