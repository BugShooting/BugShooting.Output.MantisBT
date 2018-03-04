using BS.Plugin.V3.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BugShooting.Output.MantisBT
{
  partial class Edit : Window
  {

    public Edit(Output output)
    {
      InitializeComponent();

      foreach (string fileNameReplacement in AttributeHelper.GetAttributeReplacements())
      {
        MenuItem item = new MenuItem();
        item.Header = new TextBlock() { Text = fileNameReplacement };
        item.Tag = fileNameReplacement;
        item.Click += FileNameReplacementItem_Click;
        FileNameReplacementList.Items.Add(item);
      }

      NameTextBox.Text = output.Name;
      UrlTextBox.Text = output.Url;
      UserNameTextBox.Text = output.UserName;
      PasswordBox.Password = output.Password;
      FileNameTextBox.Text = output.FileName;

      FileFormatComboBox.ItemsSource = FileHelper.GetFileFormats();
      FileFormatComboBox.SelectedValue = output.FileFormatID;
                  
      OpenItemInBrowserCheckBox.IsChecked = output.OpenItemInBrowser;

      NameTextBox.TextChanged += ValidateData;
      UrlTextBox.TextChanged += ValidateData;
      FileFormatComboBox.SelectionChanged += ValidateData;
      ValidateData(null, null);

      UrlTextBox.Focus();

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
      get { return UserNameTextBox.Text; }
    }

    public string Password
    {
      get { return PasswordBox.Password; }
    }

    public string FileName
    {
      get { return FileNameTextBox.Text; }
    }

    public Guid FileFormatID
    {
      get { return (Guid)FileFormatComboBox.SelectedValue; }
    }

    public bool OpenItemInBrowser
    {
      get { return OpenItemInBrowserCheckBox.IsChecked.Value; }
    }
   
    
    private void FileNameReplacement_Click(object sender, RoutedEventArgs e)
    {
      FileNameReplacement.ContextMenu.IsEnabled = true;
      FileNameReplacement.ContextMenu.PlacementTarget = FileNameReplacement;
      FileNameReplacement.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
      FileNameReplacement.ContextMenu.IsOpen = true;
    }

    private void FileNameReplacementItem_Click(object sender, RoutedEventArgs e)
    {

      MenuItem item = (MenuItem)sender;

      int selectionStart = FileNameTextBox.SelectionStart;

      FileNameTextBox.Text = FileNameTextBox.Text.Substring(0, FileNameTextBox.SelectionStart) + item.Tag.ToString() + FileNameTextBox.Text.Substring(FileNameTextBox.SelectionStart, FileNameTextBox.Text.Length - FileNameTextBox.SelectionStart);

      FileNameTextBox.SelectionStart = selectionStart + item.Tag.ToString().Length;
      FileNameTextBox.Focus();

    }

    private void ValidateData(object sender, EventArgs e)
    {
      OK.IsEnabled = Validation.IsValid(NameTextBox) &&
                     Validation.IsValid(UrlTextBox) &&
                     Validation.IsValid(FileFormatComboBox);
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;     
    }

  }
}
