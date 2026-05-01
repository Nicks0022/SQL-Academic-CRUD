using System;

namespace Model
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}