using Newtonsoft.Json;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Projeto.ViewModels
{
  [QueryProperty("taskSerialized", "taskSerialized")]
  public class DetailViewModel : BaseViewModel
  {
    public Tasks task { get; set; }
    public string taskSerialized 
    { 
      set 
      {
        task = JsonConvert.DeserializeObject<Tasks>(Uri.UnescapeDataString(value));
        //usa OnPropertyChanged para quando alterar a tarefa alterar aqui tambem
        OnPropertyChanged(nameof(task));
      } 
    }
    public DetailViewModel()
    {
    }
  }
}
