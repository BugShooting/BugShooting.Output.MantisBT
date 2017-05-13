using System;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel;
using System.Web;
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
      get  { return Properties.Resources.mantisbt_64x32; }
    }

    protected override Image Image16x16
    {
      get { return Properties.Resources.mantisbt_16x16 ; }
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
                                 1);

      return EditOutput(Owner, output);

    }

    protected override Output EditOutput(IWin32Window Owner, Output Output)
    {

      EditOutput editOutput = new EditOutput(Output);

      var ownerHelper = new System.Windows.Interop.WindowInteropHelper(editOutput);
      ownerHelper.Owner = Owner.Handle;
      
      if (editOutput.ShowDialog() == true) {

        return new Output(editOutput.OutputName,
                          editOutput.Url,
                          editOutput.UserName,
                          editOutput.Password,
                          editOutput.FileName,
                          editOutput.FileExtention,
                          editOutput.OpenItemInBrowser,
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
      outputValues.Add(new OutputValue("URL", Output.Url));
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
                        Convert.ToInt32(OutputValues["LastIssueID", Convert.ToString(1)].Value));

    }

    protected override V3.SendResult SendAsync(Output Output, V3.ImageData ImageData)
    {

      try
      {
     
        string userName = Output.UserName;
        string password = Output.Password;

        while (true)
        {
         
          if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
          {

            Login login = new Login(Output.Url, userName, password);
            if (login.ShowDialog() != true)
            {
              return new V3.SendResult(V3.Result.Canceled);
            }

            userName = login.UserName;
            password = login.Password;
            
          }
           

          MantisConnectPortTypeClient mantisConnect = new MantisConnectPortTypeClient();
          mantisConnect.Endpoint.Address= new EndpointAddress(Output.Url + "/api/soap/mantisconnect.php");
      

          try
          {
            // TODO
            //object[] objArgs = new object[6];
            //objArgs(0) = objOutput;
            //objArgs(1) = objImageData;
            //objArgs(2) = mantisConnect;
            //objArgs(3) = objLoginHelper.UserName;
            //objArgs(4) = objLoginHelper.Password;
            //if (!Convert.ToBoolean(MainThreadHelper.InvokeOnMainThread(new GetSendOptionsDelegate(GetSendOptions), objArgs)))
            //{
            //  return new V3.SendResult(V3.Result.Canceled);
            //}
            //clsSendOptions objOptions = (clsSendOptions)objArgs(5);


            V3.FileData fileData = V3.FileHelper.GetFileData(Output.FileName, Output.FileExtention, ImageData);

            string strIssueID = null;
            string strProjectID = null;
            string strCategory = null;

            // TODO
            //if (objOptions.CreateNewIssue)
            //{
            //  ObjectRef[] priorities = mantisConnect.mc_enum_priorities(userName, password);
            //  ObjectRef[] severities = mantisConnect.mc_enum_severities(userName, password);

            //  ObjectRef objProjectRef = new ObjectRef();
            //  objProjectRef.id = objOptions.ProjectID;
            //  objProjectRef.name = objOptions.ProjectName;

            //  IssueData objIssue = new IssueData();
            //  objIssue.summary = HttpUtility.HtmlEncode(objOptions.Summary);
            //  objIssue.description = HttpUtility.HtmlEncode(objOptions.Description);
            //  objIssue.priority = priorities[0];
            //  objIssue.severity = severities[0];
            //  objIssue.status = new ObjectRef();
            //  objIssue.date_submitted = DateTime.UtcNow;
            //  objIssue.category = objOptions.Category;
            //  objIssue.reproducibility = new ObjectRef();
            //  objIssue.resolution = new ObjectRef();
            //  objIssue.project = objProjectRef;
            //  objIssue.projection = new ObjectRef();
            //  objIssue.eta = new ObjectRef();
            //  objIssue.view_state = new ObjectRef();

            //  strIssueID = mantisConnect.mc_issue_add(userName, password, objIssue);

            //  strProjectID = objOptions.ProjectID;
            //  strCategory = objOptions.Category;


            //}
            //else
            //{
            //  strIssueID = Convert.ToString(objOptions.IssueID);
            //  strProjectID = Output.LastProjectID;
            //  strCategory = Output.LastCategory;


            //  if (!string.IsNullOrEmpty(objOptions.Note))
            //  {
            //    IssueNoteData objNote = new IssueNoteData();
            //    objNote.text = HttpUtility.HtmlEncode(objOptions.Note);
            //    objNote.date_submitted = DateTime.UtcNow;
            //    objNote.view_state = new ObjectRef();

            //    mantisConnect.mc_issue_note_add(userName, password, strIssueID, objNote);

            //  }

            //}

            mantisConnect.mc_issue_attachment_add(userName, password, strIssueID, fileData.FullFileName, fileData.MimeType, fileData.FileBytes);


            if (Output.OpenIssueInBrowser)
            {
              WebHelper.OpenUrl(Output.Url + "/view.php?id=" + strIssueID);
            }

            return new V3.SendResult(V3.Result.Success, 
                                     new Output(Output.Name, 
                                                Output.Url,
                                                userName,
                                                password,
                                                Output.FileName,
                                                Output.FileExtention,
                                                Output.OpenIssueInBrowser, 
                                                strProjectID, 
                                                strCategory, 
                                                Convert.ToInt32(strIssueID)));

          }
          catch (System.Net.WebException ex)
          {
            return new V3.SendResult(V3.Result.Failed, ex.Message);
          }
          catch (System.Web.Services.Protocols.SoapException ex)
          {
            return new V3.SendResult(V3.Result.Failed, ex.Message);
          }
          catch
          {
            // Login failed
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
