using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Models
{
  public class Tasks
  {
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Finalizada { get; set; }
    public string Status { get; set; }
    public string Cor { get; set; }
  }
}
