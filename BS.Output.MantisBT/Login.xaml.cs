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

    public Login()
    {
      InitializeComponent();
    }

    public string Url
    {
      set { UrlLabel.Text = value; }
    }

    public string UserName
    {
      get { return UserTextBox.Text; }
      set { UserTextBox.Text = value; }
    }

    public string Password
    {
      get { return PasswordBox.Password; }
      set { PasswordBox.Password = value; }
    }

    public bool Remember
    {
      get { return RememberCheckBox.IsChecked.Value; }
      set { RememberCheckBox.IsChecked = value; }
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = false;
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
      base.OnPreviewKeyDown(e);

      switch (e.Key)
      {
        case Key.Enter:
          OK_Click(this, e);
          break;
        case Key.Escape:
          Cancel_Click(this, e);
          break;
      }

    }

  }
}
