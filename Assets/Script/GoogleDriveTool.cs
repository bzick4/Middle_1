using System;
using System.Collections.Generic;
using System.Text;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public static class GoogleDriveTool
{
    public static List<File> FileList()
    {
        List<File> output = new List<File>();
        GoogleDriveFiles.List().Send().OnDone += fileList => { output = fileList.Files; };
        return output;
    }

    public static File Upload(String obj, string parentFolderId, Action onDone)
    {
        var file = new UnityGoogleDrive.Data.File
        {
            Name = "GameData.json", Content = Encoding.ASCII.GetBytes(obj), Parents = new List<string> {"1pkQZ0j2kVGNOZU12Hx-uVoD8cMllX2WT"}
        };
        
        GoogleDriveFiles.Create(file).Send(); 
        
        return file;
    }
    
    public static File Download(String fileId)
    {
        File output = new File();
        GoogleDriveFiles.Download(fileId).Send().OnDone += file => { output = file; };
        return output;
    }
    
}
