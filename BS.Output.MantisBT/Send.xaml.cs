using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using BS.Output.MantisBT.MantisConnect_2_4_0;

namespace BS.Output.MantisBT
{
  partial class Send : Window
  {

    public Send(string url, string lastProjectID, string lastCategory, string lastIssueID, ProjectData[] projects, string userName, string password)
    {
      InitializeComponent();
      
      NewIssue.Checked += NewIssue_CheckedChanged;
      NewIssue.Unchecked += NewIssue_CheckedChanged;
      NewIssue_CheckedChanged(null, EventArgs.Empty);

      UrlLabel.Text = url;

      List<ProjectItem> projectItems = new List<ProjectItem>();
      InitProjects(projectItems, projects, String.Empty);

      Projects.ItemsSource = projectItems;
      Projects.SelectedValue = lastProjectID;
      Categories.SelectedValue = lastCategory;
      IssueIDTextBox.Text = lastIssueID;

    }

    public bool CreateNewIssue
    {
      get { return NewIssue.IsChecked.Value; }
    }

    public string ProjectID
    {
      get { return Projects.SelectedValue.ToString(); }
    }

    public string ProjectName
    {
      get { return Projects.SelectedValue.ToString(); }
    }

    public string Category
    {
      get { return Categories.SelectedValue.ToString(); }
    }

    public string Summary
    {
      get { return SummaryTextBox.Text; }
    }


    public string Description
    {
      get { return DescriptionTextBox.Text; }
    }

    public string IssueID
    {
      get { return IssueIDTextBox.Text; }
    }

    public string Note
    {
      get { return NoteTextBox.Text; }
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

    private void InitProjects(List<ProjectItem> projectItems, ProjectData[] projects, string parentProjectName)
    {
      string projectName = null;


      foreach (ProjectData project in projects)
      {
        if (parentProjectName == string.Empty)
        {
          projectName = project.name;
        }
        else
        {
          projectName = parentProjectName + " - " + project.name;
        }

        projectItems.Add(new ProjectItem(project.id, project.name, projectName));

        if ((project.subprojects != null))
        {
          InitProjects(projectItems, project.subprojects, projectName);
        }

      }

    }

  }

  internal class ProjectItem
  {
    
    private string projectID;
    private string projectName;
    private string name;

    public ProjectItem(string projectID, string projectName, string name)
    {
      this.projectID = projectID;
      this.projectName = projectName;
      this.name = name;
    }

    public string ProjectID
    {
      get { return projectID; }
    }

    public string ProjectName
    {
      get { return projectName; }
    }

    public override string ToString()
    {
      return name;
    }

  }

}
