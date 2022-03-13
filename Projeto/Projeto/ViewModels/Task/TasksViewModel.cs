using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Projeto.Repositories;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Projeto.ViewModels
{
  public class TasksViewModel : BaseViewModel
  {
    public String SearchWord { get; set; }
    public String day { get; set; }


    public ICommand SearchCommand { get; set; }
    public ICommand DetailCommand { get; set; }
    public ICommand CreateCommand { get; set; }
    public ICommand OpenCalendarCommand { get; set; }
    public ICommand ChangeStatusCommand { get; set; }
    

    private List<Tasks>_listTasks;
    public List<Tasks> listTasks
    {
      get
      {
        return _listTasks;
      }
      set
      {
        SetProperty(ref _listTasks, value);
      }
    }

    private DateTime _taskDate;
    public DateTime taskDate 
    {
      get
      {
        return _taskDate;
      }
      set
      {
        SetProperty(ref _taskDate, value);
        ChangeDate();
      }
    }

    private readonly TaskRepository _taskRepository = new TaskRepository();
    public TasksViewModel()
    {
      day = DateTime.Now.DayOfWeek.ToString();
      taskDate = DateTime.Now;
      SearchCommand = new Command(Search);
      DetailCommand = new Command<Tasks>(Detail);
      CreateCommand = new Command(Create);
      ChangeStatusCommand = new Command<Tasks>(ChangeStatus);
      GetTasks();
    }

    async void GetTasks()
    {
      listTasks = await _taskRepository.GetTasksAsync(taskDate, SearchWord);
    }

    protected void ChangeDate()
    {
      GetTasks();
    }
    private async void Search()
    {
      listTasks = await _taskRepository.GetTasksAsync(taskDate, SearchWord);
    }


    private void Detail(Tasks task)
    {
      String taskSerialized = JsonConvert.SerializeObject(task);
      Shell.Current.GoToAsync($"Task/Detail?taskSerialized={Uri.EscapeDataString(taskSerialized)}");
    }

    private async void Create()
    {
      Shell.Current.GoToAsync("Task/Create");
    }
    private async void Delete(Tasks task)
    {
      bool delete = await _taskRepository.DeleteTaskAsync(task);
      listTasks = await _taskRepository.GetTasksAsync(taskDate, SearchWord);
    }

    private async void ChangeStatus(Tasks task)
    {
      if (task.Finalizada)
        task.Finalizada = false;
      else
        task.Finalizada = true;

      await _taskRepository.UpdateTaskAsync(task);
      listTasks = await _taskRepository.GetTasksAsync(taskDate, SearchWord);
    }
  }
}
