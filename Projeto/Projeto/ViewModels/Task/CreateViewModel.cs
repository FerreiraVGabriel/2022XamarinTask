using Projeto.Models;
using Projeto.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Projeto.ViewModels.Task
{
  public class CreateViewModel : BaseViewModel
  {
    public Tasks _task;

    public Tasks task 
    { 
      get { return _task; }
      set
      {
        SetProperty(ref _task, value);
      }
    }
    public ICommand ReturnCommand { get; set; }
    public ICommand CreateTaskCommand { get; set; }

    private readonly TaskRepository _taskRepository = new TaskRepository();

    public CreateViewModel()
    {
      CreateTaskCommand = new Command<Tasks>(CreateTask);
      ReturnCommand = new Command(Return);
      task = new Tasks();
      task.Finalizada = true;
      task.Data = DateTime.Now;
    }

    private async void Return()
    {
      await Shell.Current.Navigation.PopModalAsync();
    }

    private async void CreateTask(Tasks task)
    {
      string dateString = task.Data.ToString("dd/MM/yyyy");
      task.Data = DateTime.ParseExact(dateString, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"));
      bool create = await _taskRepository.AddTasksAsync(task);
      await Shell.Current.Navigation.PopModalAsync();
      Shell.Current.GoToAsync("Tasks");
    }
  }
}
