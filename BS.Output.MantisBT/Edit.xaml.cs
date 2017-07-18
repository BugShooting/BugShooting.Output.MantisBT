using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BS.Output.MantisBT
{
  partial class Edit : Window
  {

    public Edit(Output output)
    {
      InitializeComponent();

      this.DataContext = this;
      
      foreach (string fileNameReplacement in V3.FileHelper.GetFileNameReplacements())
      {
        MenuItem item = new MenuItem();
        item.Header = fileNameReplacement;
        item.Click += FileNameReplacementItem_Click;
        FileNameReplacementList.Items.Add(item);
      }

      foreach (string fileFormat in V3.FileHelper.GetFileFormats())
      {
        ComboBoxItem item = new ComboBoxItem();
        item.Content = fileFormat;
        item.Tag = fileFormat;
        FileFormatList.Items.Add(item);
      }

      OutputName = output.Name;
      Url = output.Url;
      UserName = output.UserName;
      FileName = output.FileName;
      FileFormat = output.FileFormat;
      PasswordBox.Password = output.Password;
      OpenItemInBrowser = output.OpenIssueInBrowser;
         
      if (FileFormatList.SelectedValue is null)
        FileFormatList.SelectedIndex = 0;
  
    }
     
    public string OutputName { get; set; }

    public string Url { get; set; }
   
    public string UserName { get; set; }
 
    public string FileName { get; set; }
   
    public string FileFormat { get; set; }
   
    public bool OpenItemInBrowser { get; set; }
   
    public string Password
    {
      get { return PasswordBox.Password; }
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

      FileNameTextBox.Text = FileNameTextBox.Text.Substring(0, FileNameTextBox.SelectionStart) + item.Header.ToString() + FileNameTextBox.Text.Substring(FileNameTextBox.SelectionStart, FileNameTextBox.Text.Length - FileNameTextBox.SelectionStart);

      FileNameTextBox.SelectionStart = selectionStart + item.Header.ToString().Length;
      FileNameTextBox.Focus();

    }

    private void ValidateData(object sender, RoutedEventArgs e)
    {
      OK.IsEnabled = !Validation.GetHasError(NameTextBox) &&
                     !Validation.GetHasError(UrlTextBox) &&
                     !Validation.GetHasError(FileFormatList);
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;     
    }

  }
}
