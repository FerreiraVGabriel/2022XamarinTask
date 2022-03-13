using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projeto.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Create : ContentPage
  {
    public Create()
    {
      InitializeComponent();
      Shell.SetPresentationMode(this, PresentationMode.ModalAnimated);
    }
  }
}