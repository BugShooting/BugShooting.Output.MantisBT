using System;

namespace BS.Output.MantisBT
{

  public class Output: IOutput 
  {
    
    string name;
    string url;
    string userName;
    string password;
    string fileName;
    string fileExtention;
    bool openIssueInBrowser;
    string lastProjectID;
    string lastCategory;
    string lastIssueID;

    public Output(string name, 
                  string url, 
                  string userName,
                  string password, 
                  string fileName, 
                  string fileExtention,
                  bool openIssueInBrowser, 
                  string lastProjectID, 
                  string lastCategory,
                  string lastIssueID)
    {
      this.name = name;
      this.url = url;
      this.userName = userName;
      this.password = password;
      this.fileName = fileName;
      this.fileExtention = fileExtention;
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
          
    public string FileName
    {
      get { return fileName; }
    }

    public string FileExtention
    {
      get { return fileExtention; }
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

    public string LastIssueID
    {
      get { return lastIssueID; }
    }

  }
}
