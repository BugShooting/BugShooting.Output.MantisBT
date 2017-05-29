using System;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel;
using System.Web;
using System.Threading.Tasks;
using BS.Output.MantisBT.MantisConnect_2_4_0;

namespace BS.Output.MantisBT
{
  public class OutputAddIn: V3.OutputAddIn<Output>
  {

    protected override Guid ID
    {
      get  { return new Guid("1A5A1DA0-D629-43F3-943B-C93CAAF4549F"); }
    }

    protected override string Name
    {
      get { return "MantisBT"; }
    }

    protected override Image Image64x32
    {
      get  { return Properties.Resources.logo_64x32; }
    }

    protected override Image Image16x16
    {
      get { return Properties.Resources.logo_16x16 ; }
    }

    protected override bool Editable
    {
      get { return true; }
    }

    protected override string Description
    {
      get { return "Attach screenshots to Mantis BT Issues."; }
    }
    
    protected override Output CreateOutput(IWin32Window Owner)
    {
      
      Output output = new Output(Name, 
                                 String.Empty, 
                                 String.Empty, 
                                 String.Empty, 
                                 "Screenshot",
                                 "png", 
                                 true,
                                 String.Empty,
                                 String.Empty, 
                                 "1");

      return EditOutput(Owner, output);

    }

    protected override Output EditOutput(IWin32Window Owner, Output Output)
    {

      Edit edit = new Edit(Output);

      var ownerHelper = new System.Windows.Interop.WindowInteropHelper(edit);
      ownerHelper.Owner = Owner.Handle;
      
      if (edit.ShowDialog() == true) {

        return new Output(edit.OutputName,
                          edit.Url,
                          edit.UserName,
                          edit.Password,
                          edit.FileName,
                          edit.FileExtention,
                          edit.OpenItemInBrowser,
                          Output.LastProjectID,
                          Output.LastCategory,
                          Output.LastIssueID);
      }
      else
      {
        return null; 
      }

    }

    protected override OutputValueCollection SerializeOutput(Output Output)
    {

      OutputValueCollection outputValues = new OutputValueCollection();

      outputValues.Add(new OutputValue("Name", Output.Name));
      outputValues.Add(new OutputValue("Url", Output.Url));
      outputValues.Add(new OutputValue("UserName", Output.UserName));
      outputValues.Add(new OutputValue("Password",Output.Password, true));
      outputValues.Add(new OutputValue("OpenIssueInBrowser", Convert.ToString(Output.OpenIssueInBrowser)));
      outputValues.Add(new OutputValue("FileName", Output.FileName));
      outputValues.Add(new OutputValue("FileExtention", Output.FileExtention));
      outputValues.Add(new OutputValue("LastProjectID", Output.LastProjectID));
      outputValues.Add(new OutputValue("LastCategory", Output.LastCategory));
      outputValues.Add(new OutputValue("LastIssueID", Convert.ToString(Output.LastIssueID)));

      return outputValues;
      
    }

    protected override Output DeserializeOutput(OutputValueCollection OutputValues)
    {

      return new Output(OutputValues["Name", this.Name].Value,
                        OutputValues["Url", ""].Value, 
                        OutputValues["UserName", ""].Value,
                        OutputValues["Password", ""].Value, 
                        OutputValues["FileName", "Screenshot"].Value, 
                        OutputValues["FileExtention", "png"].Value,
                        Convert.ToBoolean(OutputValues["OpenIssueInBrowser", Convert.ToString(true)].Value),
                        OutputValues["LastProjectID", string.Empty].Value, 
                        OutputValues["LastCategory", string.Empty].Value, 
                        OutputValues["LastIssueID", "1"].Value);

    }

    protected override async Task<V3.SendResult> Send(Output Output, V3.ImageData ImageData)
    {

      try
      {

        HttpBindingBase binding;
        if (Output.Url.StartsWith("https", StringComparison.InvariantCultureIgnoreCase))
        {
          binding = new BasicHttpsBinding();
        }
        else
        {
          binding = new BasicHttpBinding();
        }
        binding.MaxBufferSize = int.MaxValue;
        binding.MaxReceivedMessageSize = int.MaxValue;
        
        MantisConnectPortTypeClient mantisConnect = new MantisConnectPortTypeClient(binding, new EndpointAddress(Output.Url + "/api/soap/mantisconnect.php"));

        string userName = Output.UserName;
        string password = Output.Password;
        bool showLogin = string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password);
        bool rememberCredentials = false;

        while (true)
        {
         
          if (showLogin)
          {

            // Show credentials window
            Credentials credentials = new Credentials(Output.Url, userName, password, rememberCredentials);

            if (credentials.ShowDialog() != true)
            {
              return new V3.SendResult(V3.Result.Canceled);
            }

            userName = credentials.UserName;
            password = credentials.Password;
            rememberCredentials = credentials.Remember;

          }
      
          try
          {

            // Get available projects
            ProjectData[] projects = await Task.Factory.StartNew(() => mantisConnect.mc_projects_get_user_accessible(userName, password));

            // Show send window
            Send send = new Send(Output.Url, Output.LastProjectID, Output.LastCategory, Output.LastIssueID, projects, userName, password);

            if (!send.ShowDialog() == true)
            {
              return new V3.SendResult(V3.Result.Canceled);
            }

            V3.FileData fileData = V3.FileHelper.GetFileData(Output.FileName, Output.FileExtention, ImageData);
                        
            string projectID = null;
            string category = null;
            string issueID = null;

            if (send.CreateNewIssue)
            {
              ObjectRef[] priorities = await Task.Factory.StartNew(() => mantisConnect.mc_enum_priorities(userName, password));
              ObjectRef[] severities = await Task.Factory.StartNew(() => mantisConnect.mc_enum_severities(userName, password));

              ObjectRef projectRef = new ObjectRef();
              projectRef.id = send.ProjectID;
              projectRef.name = send.ProjectName;

              IssueData issue = new IssueData();
              issue.summary = HttpUtility.HtmlEncode(send.Summary);
              issue.description = HttpUtility.HtmlEncode(send.Description);
              issue.priority = priorities[0];
              issue.severity = severities[0];
              issue.status = new ObjectRef();
              issue.date_submitted = DateTime.UtcNow;
              issue.category = send.Category;
              issue.reproducibility = new ObjectRef();
              issue.resolution = new ObjectRef();
              issue.project = projectRef;
              issue.projection = new ObjectRef();
              issue.eta = new ObjectRef();
              issue.view_state = new ObjectRef();

              issueID = await Task.Factory.StartNew(() => mantisConnect.mc_issue_add(userName, password, issue));

              projectID = send.ProjectID;
              category = send.Category;

            }
            else
            {
              issueID = Convert.ToString(send.IssueID);
              projectID = Output.LastProjectID;
              category = Output.LastCategory;


              if (!string.IsNullOrEmpty(send.Note))
              {
                IssueNoteData objNote = new IssueNoteData();
                objNote.text = HttpUtility.HtmlEncode(send.Note);
                objNote.date_submitted = DateTime.UtcNow;
                objNote.view_state = new ObjectRef();

                await Task.Factory.StartNew(() => mantisConnect.mc_issue_note_add(userName, password, issueID, objNote));

              }

            }

            await Task.Factory.StartNew(() => mantisConnect.mc_issue_attachment_add(userName, password, issueID, fileData.FullFileName, fileData.MimeType, fileData.FileBytes));


            // Open issue in browser
            if (Output.OpenIssueInBrowser)
            {
              WebHelper.OpenUrl(Output.Url + "/view.php?id=" + issueID);
            }
                    

            return new V3.SendResult(V3.Result.Success, 
                                     new Output(Output.Name, 
                                                Output.Url,
                                                (rememberCredentials) ? userName : Output.UserName,
                                                (rememberCredentials) ? password : Output.Password,
                                                Output.FileName,
                                                Output.FileExtention,
                                                Output.OpenIssueInBrowser, 
                                                projectID, 
                                                category, 
                                                issueID));

          }
          catch (FaultException ex) when (ex.Reason.ToString() == "Access denied")
          {
            // Login failed
            showLogin = true;            
          }

        }

      }
      catch (Exception ex)
      {
        return new V3.SendResult(V3.Result.Failed, ex.Message);
      }

    }
      
  }
}
