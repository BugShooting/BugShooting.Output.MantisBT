using System;

namespace BS.Output.MantisBT
{

  public enum ImageFormat
  { 
    Png = 0,
    Jpeg = 1,
    Gif = 2,
    Bmp = 3,
  }


  public class Output: IOutput 
  {
    
    String name;
    string url;

    string userName;
    string password;

    String fileName;
    ImageFormat imageFormat;
    int imageQuality;

    Boolean openIssueInBrowser;

    string lastProjectID;
    string lastCategory;
    int lastIssueID;

    public Output(string name, 
                  string url, 
                  string userName,
                  string password, 
                  string fileName, 
                  ImageFormat imageFormat,
                  int imageQuality, 
                  bool openIssueInBrowser, 
                  string lastProjectID, 
                  string lastCategory,
                  int lastIssueID)
    {
      this.name = name;
      this.url = url;
      this.userName = userName;
      this.password = password;
      this.fileName = fileName;
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

    public bool FileNameAvailable
    {
      get { return true; }
    }
    
    public string FileName
    {
      get { return fileName; }
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
