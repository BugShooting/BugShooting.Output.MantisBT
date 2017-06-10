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

      NameTextBox.Text = output.Name;
      UrlTextBox.Text = output.Url;
      UserTextBox.Text = output.UserName;
      PasswordBox.Password = output.Password;
      FileNameTextBox.Text = output.FileName;

      FileFormatList.SelectedValue = output.FileFormat;
      if (FileFormatList.SelectedValue is null)
      {
        FileFormatList.SelectedIndex = 0;
      }

      OpenItemInBrowserCheckBox.IsChecked = output.OpenIssueInBrowser;

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

    public string FileFormat
    {
      get { return FileFormatList.SelectedValue.ToString(); }
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

      FileNameTextBox.Text = FileNameTextBox.Text.Substring(0, FileNameTextBox.SelectionStart) + item.Header.ToString() + FileNameTextBox.Text.Substring(FileNameTextBox.SelectionStart, FileNameTextBox.Text.Length - FileNameTextBox.SelectionStart);

      FileNameTextBox.SelectionStart = selectionStart + item.Header.ToString().Length;
      FileNameTextBox.Focus();

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
