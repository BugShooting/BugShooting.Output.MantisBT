using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS.Output;

namespace BS.Output.MantisBT
{

  public enum LoginType
  {
    OnDemand,
    FixedCredentials,
    IntegratedAuthentication,
  }

  public class Output: IOutput 
  {
    
    String name;
    string url;

    Boolean proxyEnabled;
    string proxyAddress;
    Int32 proxyPort;
    Boolean proxyAuthentication;
    string proxyUsername;
    string proxyPassword;
    
    LoginType loginType;
    string userName;
    string password;

    FileNameType fileNameType;
    String fileNameValue;
    ImageFormat imageFormat;
    Int32 imageQuality;

    Boolean openIssueInBrowser;

    string lastProjectID;
    string lastCategoryID;
    string lastIssueID;
    
    public string Name
    {
      get
      {
        return name;
      }
    }

    public string Information
    {
      get
      {
        return url;
      }
    }


  }
}
