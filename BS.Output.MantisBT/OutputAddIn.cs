using System;
using System.Drawing;
using System.Windows.Forms;

namespace BS.Output.MantisBT
{
  class OutputAddIn: V3.OutputAddIn<Output>
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

    protected override Image Image
    {
      get
      {
        return Properties.Resources.mantisbt;
      }
    }

    protected override bool Editable
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
    
    protected override Output CreateOutput(IWin32Window Owner)
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

      return EditOutput(Owner, output);

    }

    protected override Output EditOutput(IWin32Window Owner, Output Output)
    {

      using (frmOutputEditDefault frmEdit = new frmOutputEditDefault(Output, "http://myserver/mantisbt", false))
      {
        if (frmEdit.ShowDialog(Owner) == DialogResult.OK)
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
                            Output.LastProjectID,
                            Output.LastCategory,
                            Output.LastIssueID);
        }
        else
        {
          return null;
        }
      }
      
    }

    protected override OutputValueCollection SerializeOutput(Output Output)
    {

      OutputValueCollection outputValues = new OutputValueCollection();

      outputValues.Add(new OutputValue("Name", Output.Name));
      outputValues.Add(new OutputValue("URL", Output.Url));
      outputValues.Add(new OutputValue("UseProxy", Convert.ToString(Output.UseProxy)));
      outputValues.Add(new OutputValue("ProxyAddress", Output.ProxyAddress));
      outputValues.Add(new OutputValue("ProxyPort", Convert.ToString(Output.ProxyPort)));
      outputValues.Add(new OutputValue("ProxyUseAuthentication", Convert.ToString(Output.ProxyAuthentication)));
      outputValues.Add(new OutputValue("ProxyUserName", Output.ProxyUserName));
      outputValues.Add(new OutputValue("ProxyPassword", Output.ProxyPassword, true));
      outputValues.Add(new OutputValue("LoginType", Convert.ToString(Output.LoginType)));
      outputValues.Add(new OutputValue("UserName", Output.UserName));
      outputValues.Add(new OutputValue("Password",Output.Password, true));
      outputValues.Add(new OutputValue("OpenIssueInBrowser", Convert.ToString(Output.OpenIssueInBrowser)));
      outputValues.Add(new OutputValue("FileNameType", Convert.ToString(Output.FileNameType)));
      outputValues.Add(new OutputValue("FileNameValue", Output.FileNameValue));
      outputValues.Add(new OutputValue("ImageFormat", Convert.ToString(Output.ImageFormat)));
      outputValues.Add(new OutputValue("ImageQuality", Convert.ToString(Output.ImageQuality)));
      outputValues.Add(new OutputValue("LastProjectID", Output.LastProjectID));
      outputValues.Add(new OutputValue("LastCategory", Output.LastCategory));
      outputValues.Add(new OutputValue("LastIssueID", Convert.ToString(Output.LastIssueID)));

      return outputValues;
      
    }

    protected override Output DeserializeOutput(OutputValueCollection OutputValues)
    {

      return new Output(OutputValues["Name", this.Name].Value,
                        OutputValues["URL", ""].Value, 
                        Convert.ToBoolean(OutputValues["UseProxy", Convert.ToString(false)].Value),
                        OutputValues["ProxyAddress", ""].Value, 
                        Convert.ToInt32(OutputValues["ProxyPort", Convert.ToString(8080)].Value), 
                        Convert.ToBoolean(OutputValues["ProxyUseAuthentication", Convert.ToString(false)].Value),
                        OutputValues["ProxyUserName", string.Empty].Value, 
                        OutputValues["ProxyPassword", string.Empty].Value,
                        (LoginType)Enum.Parse(typeof(LoginType),OutputValues["LoginType", Convert.ToString(LoginType.OnDemand)].Value),
                        OutputValues["UserName", ""].Value,
                        OutputValues["Password", ""].Value, 
                        (FileNameType)Enum.Parse(typeof(FileNameType), OutputValues["FileNameType", Convert.ToString(FileNameType.RandomText)].Value),
                        OutputValues["FileNameValue", string.Empty].Value, 
                        (ImageFormat)Enum.Parse(typeof(ImageFormat), OutputValues["ImageFormat", Convert.ToString(ImageFormat.Png)].Value),
                        Convert.ToInt32(OutputValues["ImageQuality",Convert.ToString(100)].Value), 
                        Convert.ToBoolean(OutputValues["OpenIssueInBrowser", Convert.ToString(true)].Value),
                        OutputValues["LastProjectID", string.Empty].Value, 
                        OutputValues["LastCategory", string.Empty].Value, 
                        Convert.ToInt32(OutputValues["LastIssueID", Convert.ToString(1)].Value));

    }

    protected override SendResult SendAsync(Output Output, ImageData ImageData)
    {

      XXXXXXXXXXXXX;

    }
    
  }
}
