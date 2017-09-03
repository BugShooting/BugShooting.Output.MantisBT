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

    protected override string Name
    {
      get { return "MantisBT"; }
    }

    protected override Image Image64
    {
      get  { return Properties.Resources.logo_64; }
    }

    protected override Image Image16
    {
      get { return Properties.Resources.logo_16 ; }
    }

    protected override bool Editable
    {
      get { return true; }
    }

    protected override string Description
    {
      get { return "Attach screenshots to Mantis BT issues."; }
    }
    
    protected override Output CreateOutput(IWin32Window Owner)
    {
      
      Output output = new Output(Name, 
                                 String.Empty, 
                                 String.Empty, 
                                 String.Empty, 
                                 "Screenshot",
                                 String.Empty, 
                                 true,
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
                          edit.FileFormat,
                          edit.OpenItemInBrowser,
                          Output.LastProjectID,
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
      outputValues.Add(new OutputValue("OpenItemInBrowser", Convert.ToString(Output.OpenItemInBrowser)));
      outputValues.Add(new OutputValue("FileName", Output.FileName));
      outputValues.Add(new OutputValue("FileFormat", Output.FileFormat));
      outputValues.Add(new OutputValue("LastProjectID", Output.LastProjectID));
      outputValues.Add(new OutputValue("LastIssueID", Output.LastIssueID));

      return outputValues;
      
    }

    protected override Output DeserializeOutput(OutputValueCollection OutputValues)
    {

      return new Output(OutputValues["Name", this.Name].Value,
                        OutputValues["Url", ""].Value, 
                        OutputValues["UserName", ""].Value,
                        OutputValues["Password", ""].Value, 
                        OutputValues["FileName", "Screenshot"].Value, 
                        OutputValues["FileFormat", ""].Value,
                        Convert.ToBoolean(OutputValues["OpenItemInBrowser", Convert.ToString(true)].Value),
                        OutputValues["LastProjectID", string.Empty].Value, 
                        OutputValues["LastIssueID", "1"].Value);

    }

    protected override async Task<V3.SendResult> Send(IWin32Window Owner, Output Output, V3.ImageData ImageData)
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

        string fileName = V3.FileHelper.GetFileName(Output.FileName, Output.FileFormat, ImageData);

        while (true)
        {
         
          if (showLogin)
          {

            // Show credentials window
            Credentials credentials = new Credentials(Output.Url, userName, password, rememberCredentials);

            var ownerHelper = new System.Windows.Interop.WindowInteropHelper(credentials);
            ownerHelper.Owner = Owner.Handle;

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
            Send send = new Send(Output.Url, Output.LastProjectID, Output.LastIssueID, projects, userName, password, fileName);

            var ownerHelper = new System.Windows.Interop.WindowInteropHelper(send);
            ownerHelper.Owner = Owner.Handle;

            if (!send.ShowDialog() == true)
            {
              return new V3.SendResult(V3.Result.Canceled);
            }
            
            string projectID = null;
            string issueID = null;

            if (send.CreateNewIssue)
            {

              projectID = send.ProjectID;

              ObjectRef[] priorities = await Task.Factory.StartNew(() => mantisConnect.mc_enum_priorities(userName, password));
              ObjectRef[] severities = await Task.Factory.StartNew(() => mantisConnect.mc_enum_severities(userName, password));
              string[] categories = await Task.Factory.StartNew(() => mantisConnect.mc_project_get_categories(userName, password, projectID));

              ObjectRef projectRef = new ObjectRef();
              projectRef.id = send.ProjectID;

              IssueData issue = new IssueData();
              issue.summary = HttpUtility.HtmlEncode(send.Summary);
              issue.description = HttpUtility.HtmlEncode(send.Description);
              issue.priority = priorities[0];
              issue.severity = severities[0];
              issue.status = new ObjectRef();
              issue.date_submitted = DateTime.UtcNow;
              issue.category = categories[0];
              issue.reproducibility = new ObjectRef();
              issue.resolution = new ObjectRef();
              issue.project = projectRef;
              issue.projection = new ObjectRef();
              issue.eta = new ObjectRef();
              issue.view_state = new ObjectRef();

              issueID = await Task.Factory.StartNew(() => mantisConnect.mc_issue_add(userName, password, issue));

            }
            else
            {
              issueID = send.IssueID;
              projectID = Output.LastProjectID;
            }

            string fullFileName = String.Format("{0}.{1}", send.FileName, V3.FileHelper.GetFileExtention(Output.FileFormat));
            string fileMimeType = V3.FileHelper.GetMimeType(Output.FileFormat);
            byte[] fileBytes = V3.FileHelper.GetFileBytes(Output.FileFormat, ImageData);

            await Task.Factory.StartNew(() => mantisConnect.mc_issue_attachment_add(userName, password, issueID, fullFileName, fileMimeType, fileBytes));


            // Open issue in browser
            if (Output.OpenItemInBrowser)
            {
              V3.WebHelper.OpenUrl(String.Format("{0}/view.php?id={1}", Output.Url, issueID));
            }
                    

            return new V3.SendResult(V3.Result.Success, 
                                     new Output(Output.Name, 
                                                Output.Url,
                                                (rememberCredentials) ? userName : Output.UserName,
                                                (rememberCredentials) ? password : Output.Password,
                                                Output.FileName,
                                                Output.FileFormat,
                                                Output.OpenItemInBrowser, 
                                                projectID, 
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
