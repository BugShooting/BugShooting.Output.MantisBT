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
  partial class EditOutput : Window
  {
    public EditOutput(Output output)
    {
      InitializeComponent();

    }

    public string OutputName
    {
      get { return NameTextBox.Text; }
    }

    public string Url
    {
      get { return UrlTextBox.Text; }
    }

    public string UserName
    {
      get { return UserTextBox.Text; }
    }

    public string Password
    {
      get { return PasswordBox.Password; }
    }

    public string FileName
    {
      get { return FileNameTextBox.Text; }
    }

    public bool OpenItemInBrowser
    {
      get { return OpenItemInBrowserCheckBox.IsChecked.Value; }
    }

    private void FileNamePattern_Click(object sender, RoutedEventArgs e)
    {
      FileNamePattern.ContextMenu.IsEnabled = true;
      FileNamePattern.ContextMenu.PlacementTarget = FileNamePattern;
      FileNamePattern.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
      FileNamePattern.ContextMenu.IsOpen = true;
    }

    private void FileNamePatternItem_Click(object sender, RoutedEventArgs e)
    {

      MenuItem item = (MenuItem)sender;

      int selectionStart = FileNameTextBox.SelectionStart;

      FileNameTextBox.Text = FileNameTextBox.Text.Substring(0, FileNameTextBox.SelectionStart) + item.Header.ToString() + FileNameTextBox.Text.Substring(FileNameTextBox.SelectionStart, FileNameTextBox.Text.Length - FileNameTextBox.SelectionStart);

      FileNameTextBox.SelectionStart = selectionStart + item.Header.ToString().Length;
      FileNameTextBox.Focus();

    }

  }
}
