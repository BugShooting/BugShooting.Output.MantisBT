using System;
using System.Drawing;
using System.Windows.Forms;

namespace BS.Output.MantisBT
{
  class OutputAddIn: V2.OutputAddIn<Output, IOutputSendOptions>
  {

    protected override Guid ID
    {
      get
      {
        return new Guid("89E9E6D2-AA01-45db-BBF6-982AF57B4C03");
      }
    }

    protected override string Name
    {
      get
      {
        return "MantisBT";
      }
    }

    protected override OutputCategory Category
    {
      get
      {
        return OutputCategory.BugTracker;
      }
    }

    protected override string GroupName
    {
      get
      {
        return Name;
      }
    }

    protected override Image Image64x32
    {
      get
      {
        return Properties.Resources.mantisbt_64x32;
      }
    }

    protected override Image Image16x16
    {
      get
      {
        return Properties.Resources.mantisbt_16x16;
      }
    }

    protected override bool Editable
    {
      get
      {
        return true;
      }
    }

    protected override bool MultiInstancePossible
    {
      get
      {
        return true;
      }
    }

    protected override string Description
    {
      get
      {
        return  XXXXXXXXXXXXXXXXXXXX;
      }
    }
    
    protected override Output CreateOutput(IWin32Window owner)
    {
      
      Output output = new Output(Name, 
                                 String.Empty, 
                                 false, 
                                 String.Empty, 
                                 8080, 
                                 false, 
                                 String.Empty,
                                 String.Empty, 
                                 LoginType.OnDemand, 
                                 String.Empty, 
                                 String.Empty, 
                                 FileNameType.RandomText, 
                                 String.Empty, 
                                 ImageFormat.Png, 
                                 100, 
                                 true,
                                 String.Empty,
                                 String.Empty, 
                                 1);

      return EditOutput(owner, output);

    }

    protected override Output EditOutput(IWin32Window owner, Output output)
    {

      using (frmOutputEditDefault frmEdit = new frmOutputEditDefault(output, "http://myserver/mantisbt", false))
      {
        if (frmEdit.ShowDialog(owner) == DialogResult.OK)
        {
          return new Output(frmEdit.OutputName, 
                            frmEdit.URL, 
                            frmEdit.UseProxy, 
                            frmEdit.ProxyAddress, 
                            frmEdit.ProxyPort, 
                            frmEdit.ProxyUseAuthentication, 
                            frmEdit.ProxyUserName, 
                            frmEdit.ProxyPassword, 
                            frmEdit.LoginType, 
                            frmEdit.UserName,
                            frmEdit.Password, 
                            frmEdit.FileNameType, 
                            frmEdit.FileNameValue, 
                            frmEdit.ImageFormat, 
                            frmEdit.ImageQuality, 
                            frmEdit.OpenItemInBrowser,
                            output.LastProjectID,
                            output.LastCategory,
                            output.LastIssueID);
        }
        else
        {
          return null;
        }
      }
      
    }

    protected override IOutputSendOptions GetSendOptions(IWin32Window owner, Output output, ImageData imageData, ref bool cancel)
    {
      return null;
    }

    protected override OutputValueCollection SerializeOutput(Output output)
    {

      OutputValueCollection outputValues = new OutputValueCollection();

      outputValues.Add(new OutputValue("Name", output.Name));
      outputValues.Add(new OutputValue("URL", output.Url));
      outputValues.Add(new OutputValue("UseProxy", Convert.ToString(output.UseProxy)));
      outputValues.Add(new OutputValue("ProxyAddress", output.ProxyAddress));
      outputValues.Add(new OutputValue("ProxyPort", Convert.ToString(output.ProxyPort)));
      outputValues.Add(new OutputValue("ProxyUseAuthentication", Convert.ToString(output.ProxyAuthentication)));
      outputValues.Add(new OutputValue("ProxyUserName", output.ProxyUserName));
      outputValues.Add(new OutputValue("ProxyPassword", CryptHelper.Encrypt(output.ProxyPassword)));
      outputValues.Add(new OutputValue("LoginType", Convert.ToString(output.LoginType)));
      outputValues.Add(new OutputValue("UserName", output.UserName));
      outputValues.Add(new OutputValue("Password", CryptHelper.Encrypt(output.Password)));
      outputValues.Add(new OutputValue("OpenIssueInBrowser", Convert.ToString(output.OpenIssueInBrowser)));
      outputValues.Add(new OutputValue("FileNameType", Convert.ToString(output.FileNameType)));
      outputValues.Add(new OutputValue("FileNameValue", output.FileNameValue));
      outputValues.Add(new OutputValue("ImageFormat", Convert.ToString(output.ImageFormat)));
      outputValues.Add(new OutputValue("ImageQuality", Convert.ToString(output.ImageQuality)));
      outputValues.Add(new OutputValue("LastProjectID", output.LastProjectID));
      outputValues.Add(new OutputValue("LastCategory", output.LastCategory));
      outputValues.Add(new OutputValue("LastIssueID", Convert.ToString(output.LastIssueID)));

      return outputValues;
      
    }

    protected override Output DeserializeOutput(OutputValueCollection outputValues)
    {

      return new Output(outputValues["Name", this.Name].Value,
                        outputValues["URL", ""].Value, 
                        Convert.ToBoolean(outputValues["UseProxy", Convert.ToString(false)].Value),
                        outputValues["ProxyAddress", ""].Value, 
                        Convert.ToInt32(outputValues["ProxyPort", Convert.ToString(8080)].Value), 
                        Convert.ToBoolean(outputValues["ProxyUseAuthentication", Convert.ToString(false)].Value),
                        outputValues["ProxyUserName", string.Empty].Value, 
                        CryptHelper.Decrypt(outputValues["ProxyPassword", string.Empty].Value), 
                        (LoginType)(outputValues["LoginType", Convert.ToString(LoginType.OnDemand)].Value),
                        outputValues["UserName", ""].Value,
                        CryptHelper.Decrypt(outputValues["Password", ""].Value), 
                        (FileNameType)outputValues["FileNameType", Convert.ToString(FileNameType.RandomText)].Value,
                        outputValues["FileNameValue", string.Empty].Value, 
                        (ImageFormat)outputValues["ImageFormat", Convert.ToString(ImageFormat.Png)].Value,
                        Convert.ToInt32(outputValues["ImageQuality",Convert.ToString(100)].Value), 
                        Convert.ToBoolean(outputValues["OpenIssueInBrowser", Convert.ToString(true)].Value),
                        outputValues["LastProjectID", string.Empty].Value, 
                        outputValues["LastCategory", string.Empty].Value, 
                        Convert.ToInt32(outputValues["LastIssueID", Convert.ToString(1)].Value));

    }

    protected override void SendAsync(Output output, ImageData imageData, IOutputSendOptions sendOptions, SendResult sendResult)
    {

      XXXXXXXXXXXXX

    }


  }
}
