using System;

namespace Model
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}