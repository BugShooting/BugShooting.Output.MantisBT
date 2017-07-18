using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using BS.Output.MantisBT.MantisConnect_2_4_0;

namespace BS.Output.MantisBT
{
  partial class Send : Window
  {
 
    public Send(string url, string lastProjectID, string lastIssueID, ProjectData[] projects, string userName, string password, string fileName)
    {
      InitializeComponent();
      this.DataContext = this;

      UrlLabel.Text = url;

      NewIssue.Checked += NewIssue_CheckedChanged;
      NewIssue.Unchecked += NewIssue_CheckedChanged;
      CreateNewIssue = true;

      List<ProjectItem> projectItems = new List<ProjectItem>();
      InitProjects(projectItems, projects, String.Empty);

      Projects.ItemsSource = projectItems;
      Projects.SelectedValue = lastProjectID;
      IssueID = lastIssueID;
      FileName = fileName;
      
    }

    public bool CreateNewIssue { get; set; }
 
    public string ProjectID { get; set; }
  
    public string Summary { get; set; }

    public string Description { get; set; }
   
    public string IssueID { get; set; }
  
    public string FileName { get; set; }

    private void InitProjects(List<ProjectItem> projectItems, ProjectData[] projects, string parentProjectName)
    {
      string fullName = null;

      foreach (ProjectData project in projects)
      {
        if (parentProjectName == string.Empty)
        {
          fullName = project.name;
        }
        else
        {
          fullName = parentProjectName + " - " + project.name;
        }

        projectItems.Add(new ProjectItem(project.id, fullName));

        if ((project.subprojects != null))
        {
          InitProjects(projectItems, project.subprojects, fullName);
        }

      }

    }

    private void NewIssue_CheckedChanged(object sender, EventArgs e)
    {
      ProjectControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
      SummaryControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
      DescriptionControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Visible : Visibility.Collapsed;
      IssueIDControls.Visibility = (NewIssue.IsChecked.Value) ? Visibility.Collapsed : Visibility.Visible;

      if (NewIssue.IsChecked.Value)
      {
        SummaryTextBox.SelectAll();
        SummaryTextBox.Focus();
      }
      else
      {
        IssueIDTextBox.SelectAll();
        IssueIDTextBox.Focus();
      }

      ValidateData(null, null);

    }

    private void IssueID_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
      e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
    }
    
    private void ValidateData(object sender, RoutedEventArgs e)
    {
      OK.IsEnabled = (!NewIssue.IsChecked.Value || (!Validation.GetHasError(Projects) && !Validation.GetHasError(SummaryTextBox) && !Validation.GetHasError(DescriptionTextBox))) && 
                     !Validation.GetHasError(FileNameTextBox);
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }

  }

  internal class ProjectItem
  {
    
    private string projectID;
    private string fullName;

    public ProjectItem(string projectID, string fullName)
    {
      this.projectID = projectID;
      this.fullName = fullName;
    }

    public string ProjectID
    {
      get { return projectID; }
    }

    public override string ToString()
    {
      return fullName;
    }

  }

}
