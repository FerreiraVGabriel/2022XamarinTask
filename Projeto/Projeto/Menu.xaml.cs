using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projeto
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Menu : Shell
  {
    public Menu()
    {
      InitializeComponent();

      Routing.RegisterRoute("Task/Create", typeof(Views.Create));
      Routing.RegisterRoute("Task/Detail", typeof(Views.Detail));
      Routing.RegisterRoute("Tasks", typeof(Views.Tasks));
    }
  }
}