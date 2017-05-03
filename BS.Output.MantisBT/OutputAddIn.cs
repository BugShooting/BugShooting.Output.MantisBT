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
    
    protected override Output CreateOutput(IWin32Window Owner)
    {
      
      Output output = new Output(Name, String.Empty, false, String.Empty, 8080, false, String.Empty,String.Empty, LoginType.OnDemand, String.Empty, String.Empty, FileNameType.RandomText, String.Empty, ImageFormat.Png, 100, true, String.Empty, String.Empty, 1);

      return EditOutput(Owner, output);

    }

    protected override Output EditOutput(IWin32Window Owner, Output Output)
    {

      XXXXXXXXXXXXXX

    }

    protected override IOutputSendOptions GetSendOptions(IWin32Window Owner, Output Output, ImageData ImageData, ref bool Cancel)
    {
      
      XXXXXXXXXXXXX

    }

    protected override OutputValueCollection SerializeOutput(Output objOutput)
    {

      XXXXXXXXXXXXX

    }

    protected override Output DeserializeOutput(OutputValueCollection objOutputValues)
    {

      XXXXXXXXXXXXX

    }

    protected override void SendAsync(Output Output, ImageData ImageData, IOutputSendOptions SendOptions, SendResult SendResult)
    {

      XXXXXXXXXXXXX

    }


  }
}
