using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace BS.Output.MantisBT
{
  partial class Send : Window
  {

    public Send()
    {
      InitializeComponent();

      NewIssue.Checked += NewIssue_CheckedChanged;
      NewIssue.Unchecked += NewIssue_CheckedChanged;
      NewIssue_CheckedChanged(null, EventArgs.Empty);

    }

    public string Url
    {
      set { UrlLabel.Text = value; }
    }

    public int IssueID
    {
      get { return Convert.ToInt32(IssueIDTextBox.Text); }
      set { IssueIDTextBox.Text = Convert.ToString(value); }
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

    private void IssueID_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
      e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
    }

    private void NewIssue_CheckedChanged(object sender, EventArgs e)
    {

      ProjectControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible: Visibility.Collapsed;
      CategoryControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
      SummaryControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
      DescriptionControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
      IssueIDControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Collapsed : Visibility.Visible;
      NoteControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Collapsed : Visibility.Visible;
    
    }

  }
}
