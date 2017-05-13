using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BS.Output.MantisBT
{
  partial class Login : Window
  {
    public Login(string url, string userName, string password)
    {
      InitializeComponent();

      Url.Content = url;
      UserTextBox.Text = userName;
      PasswordBox.Password = password;
     
    }

    public string UserName
    {
      get { return UserTextBox.Text; }
    }

    public string Password
    {
      get { return PasswordBox.Password; }
    }
    
    private void OK_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = false;
    }

  }
}
