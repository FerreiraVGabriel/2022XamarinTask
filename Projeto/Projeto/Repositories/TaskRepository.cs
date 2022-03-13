
using Newtonsoft.Json;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Repositories
{
  public class TaskRepository
  {
    HttpClient client = new HttpClient();
    string url = "https://task-api-gabriel.herokuapp.com/task";
    //string url = "http://localhost:5000/task/";
    public async Task<List<Tasks>> GetTasksAsync(DateTime date, string searchWord)
    {
      try
      {
        if (string.IsNullOrEmpty(searchWord))
          searchWord = " ";
        var dateString = date.ToString("yyyy-MM-dd");
        string uri = url + "/" + searchWord + "/" + dateString;
        var response = await client.GetStringAsync(uri);
        var tasks = JsonConvert.DeserializeObject<List<Tasks>>(response);
        return tasks;
      }
      catch(Exception e)
      {
        return null;
      }

    }

    public async Task<bool> AddTasksAsync(Tasks task)
    {
      try
      {
        var uri = new Uri(string.Format(url, task.Id));
        var data = JsonConvert.SerializeObject(task);
        var content = new StringContent(data, Encoding.UTF8, "application/json");
        HttpResponseMessage response = null;
        response = await client.PostAsync(uri, content);
        return true;

        if (!response.IsSuccessStatusCode)
        {
          throw new Exception("Erro ao incluir task");
          return false;
        }
      }
      catch (Exception ex)
      {
        return false;
        throw ex;
      }
    }

    public async Task UpdateTaskAsync(Tasks task)
    {
      var uri = url+"/"+task.Id;
      var data = JsonConvert.SerializeObject(task);
      var content = new StringContent(data, Encoding.UTF8, "application/json");
      HttpResponseMessage response = null;
      response = await client.PutAsync(uri, content);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Erro ao atualizar task");
      }
    }

    public async Task<bool> DeleteTaskAsync(Tasks task)
    {
      try
      {
        var uri = url + "/" + task.Id;
        var response = await client.DeleteAsync(uri);
        return true;

        if (!response.IsSuccessStatusCode)
        {
          return false;
          throw new Exception("Erro ao atualizar task");
        }
      }
      catch(Exception e)
      {
        return false;
      }

    }

  }
}
