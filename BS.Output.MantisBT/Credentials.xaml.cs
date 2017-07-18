using System.Windows;
using System.Windows.Controls;

namespace BS.Output.MantisBT
{
  partial class Credentials : Window
  {

    public Credentials()
    {
      InitializeComponent();
      this.DataContext = this;
    }

    public string Url { get; set; }

    public string UserName { get; set; }
   
    public string Password
    {
      get { return PasswordBox.Password; }
      set { PasswordBox.Password = value; }
    }

    public bool Remember { get; set; }
  
    private void OK_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

  }
}
