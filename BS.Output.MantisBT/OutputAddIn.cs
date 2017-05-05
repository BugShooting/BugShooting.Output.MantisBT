using System;
using System.Drawing;
using System.Windows.Forms;

namespace BS.Output.MantisBT
{
  class OutputAddIn: V3.OutputAddIn<Output>
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
      get { return Properties.Resources.mantisbt_64x32; }
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

      EditOutput editOutput = new EditOutput(Output);

      var ownerHelper = new System.Windows.Interop.WindowInteropHelper(editOutput);
      ownerHelper.Owner = Owner.Handle;
      
      if (editOutput.ShowDialog() == true) {

        return null;
        //return new Output(editOutput.OutputName,
        //                  editOutput.Url,
        //                  editOutput.UseProxy,
        //                  editOutput.ProxyAddress,
        //                  editOutput.ProxyPort,
        //                  editOutput.ProxyUseAuthentication,
        //                  editOutput.ProxyUserName,
        //                  editOutput.ProxyPassword,
        //                  editOutput.LoginType,
        //                  editOutput.UserName,
        //                  editOutput.Password,
        //                  editOutput.FileNameType,
        //                  editOutput.FileNameValue,
        //                  editOutput.ImageFormat,
        //                  editOutput.ImageQuality,
        //                  editOutput.OpenItemInBrowser,
        //                  Output.LastProjectID,
        //                  Output.LastCategory,
        //                  Output.LastIssueID);

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
      outputValues.Add(new OutputValue("UseProxy", Convert.ToString(Output.UseProxy)));
      outputValues.Add(new OutputValue("ProxyAddress", Output.ProxyAddress));
      outputValues.Add(new OutputValue("ProxyPort", Convert.ToString(Output.ProxyPort)));
      outputValues.Add(new OutputValue("ProxyAuthentication", Convert.ToString(Output.ProxyAuthentication)));
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
                        OutputValues["Url", ""].Value, 
                        Convert.ToBoolean(OutputValues["UseProxy", Convert.ToString(false)].Value),
                        OutputValues["ProxyAddress", ""].Value, 
                        Convert.ToInt32(OutputValues["ProxyPort", Convert.ToString(8080)].Value), 
                        Convert.ToBoolean(OutputValues["ProxyAuthentication", Convert.ToString(false)].Value),
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

    protected override V3.SendResult SendAsync(Output Output, V3.ImageData ImageData)
    {

      // TODO

      return new V3.SendResult(V3.Result.Success);

    }
    
  }
}
