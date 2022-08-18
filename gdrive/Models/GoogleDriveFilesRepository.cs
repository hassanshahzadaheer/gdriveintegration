using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace gdrive.Models
{
    public class GoogleDriveFilesRepository
    {
        // create an scope for files
        static string[] Scopes = { DriveService.Scope.DriveFile };
        static string ApplicationName = "gdrive";

        private static DriveService GetService()
        {
            UserCredential credential;
            // Load client secrets.
            using (var stream =
                   new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                /* The file token.json stores the user's access and refresh tokens, and is created
                 automatically when the authorization flow completes for the first time. */
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });


            return service;
        }

        public static void CreateFolder(string FolderName)
        {
            // get the services which allow us to create meta information
            DriveService service = GetService();

            // for GDrive creating folder is just like a file. So here we can set all the meta information
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";

            // create a request
            Google.Apis.Drive.v3.FilesResource.CreateRequest request;

            // adding request to services
            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            // execute request
            var file = request.Execute();
            // just for testing purpose getting the id of a folder
            Console.WriteLine("Folder ID: " + file.Id);
        }


    }
}

