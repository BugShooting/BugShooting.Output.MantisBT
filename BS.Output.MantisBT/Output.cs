using System;

namespace BS.Output.MantisBT
{

  public enum LoginType
  {
    OnDemand,
    FixedCredentials,
    IntegratedAuthentication,
  }

  public enum FileNameType
  {
    FixedText,
    DateTimeFormat,
    RandomText,
    ImageTitle,
  }

  public enum ImageFormat
  { 
    Png,
    Jpeg,
    Gif,
    Bmp,
  }


  public class Output: IOutput 
  {
    
    String name;
    string url;

    Boolean useProxy;
    string proxyAddress;
    int proxyPort;
    Boolean proxyAuthentication;
    string proxyUserName;
    string proxyPassword;
    
    LoginType loginType;
    string userName;
    string password;

    FileNameType fileNameType;
    String fileNameValue;
    ImageFormat imageFormat;
    int imageQuality;

    Boolean openIssueInBrowser;

    string lastProjectID;
    string lastCategory;
    int lastIssueID;

    public Output(string name, 
                  string url, 
                  bool useProxy, 
                  string proxyAddress,
                  int proxyPort, 
                  bool proxyAuthentication, 
                  string proxyUserName, 
                  string proxyPassword, 
                  LoginType loginType, 
                  string userName,
                  string password, 
                  FileNameType fileNameType, 
                  string fileNameValue, 
                  ImageFormat imageFormat,
                  int imageQuality, 
                  bool openIssueInBrowser, 
                  string lastProjectID, 
                  string lastCategory,
                  int lastIssueID)
    {
      this.name = name;
      this.url = url;
      this.useProxy = useProxy;
      this.proxyAddress = proxyAddress;
      this.proxyPort = proxyPort;
      this.proxyAuthentication = proxyAuthentication;
      this.proxyUserName = proxyUserName;
      this.proxyPassword = proxyPassword;
      this.loginType = loginType;
      this.userName = userName;
      this.password = password;
      this.fileNameType = fileNameType;
      this.fileNameValue = fileNameValue;
      this.imageFormat = imageFormat;
      this.imageQuality = imageQuality;
      this.openIssueInBrowser = openIssueInBrowser;
      this.lastProjectID = lastProjectID;
      this.lastCategory = lastCategory;
      this.lastIssueID = lastIssueID;
    }
    
    public string Name
    {
      get { return name; }
    }

    public string Information
    {
      get { return url; }
    }

    public string Url
    {
      get { return url; }
    }

    public bool UseProxy
    {
      get { return useProxy; }
    }

    public string ProxyAddress
    {
      get { return proxyAddress; }
    }

    public Int32 ProxyPort
    {
      get { return proxyPort; }
    }

    public bool ProxyAuthentication
    {
      get { return proxyAuthentication; }
    }

    public string ProxyUserName
    {
      get { return proxyUserName; }
    }

    public string ProxyPassword
    {
      get { return proxyPassword; }
    }

    public string UserName
    {
      get { return userName; }
    }

    public string Password
    {
      get { return password; }
    }

    public bool CanIntegratedAuthentication
    {
      get { return false; }
    }

    public LoginType LoginType
    {
      get { return loginType; }
    }

    public bool FileNameAvailable
    {
      get { return true; }
    }

    public FileNameType FileNameType
    {
      get { return fileNameType; }
    }

    public string FileNameValue
    {
      get { return fileNameValue; }
    }

    public ImageFormat ImageFormat
    {
      get { return imageFormat; }
    }

    public int ImageQuality
    {
      get { return imageQuality; }
    }
    
    public bool OpenIssueInBrowser
    {
      get { return openIssueInBrowser; }
    }
    
    public string LastProjectID
    {
      get { return lastProjectID; }
    }

    public string LastCategory
    {
      get { return lastCategory; }
    }

    public Int32 LastIssueID
    {
      get { return lastIssueID; }
    }

  }
}
