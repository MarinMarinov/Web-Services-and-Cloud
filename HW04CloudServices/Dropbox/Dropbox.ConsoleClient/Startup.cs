namespace Dropbox.ConsoleClient
{
    using System;
    using System.Diagnostics;
    using Spring.IO;

    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using Spring.Social.OAuth1;

    public class Startup
    {
        private const string DropboxAppKey = "Your_App_Key";
        private const string DropboxAppSecret = "Your_App_Secet";

        public static void Main(string[] args)
        {
            // Pleace fill the DropboxAppKey and DropboxAppSecret with your credentionals before you test the app.
            DropboxServiceProvider dropboxServiceProvider =
            new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            var oauthAccessToken = AuthorizeAppOAuth(dropboxServiceProvider);

            IDropbox dropbox = dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

            DropboxProfile profile = dropbox.GetUserProfileAsync().Result;
            Console.WriteLine("Hi " + profile.DisplayName + "!");

            string newFolderName = "Your_New_Images_Folder";
            Entry createFolderEntry = dropbox.CreateFolderAsync(newFolderName).Result;

            var imageAsFileResource = new FileResource("../../Images/image1.jpg");
            var pathToUpload = "/" + newFolderName + "/image1.jpg";

            Entry uploadFileEntry = dropbox.UploadFileAsync(imageAsFileResource, pathToUpload).Result;
            Console.WriteLine("Uploaded a file: {0}", uploadFileEntry.Path);

            DropboxLink sharedUrl = dropbox.GetShareableLinkAsync(uploadFileEntry.Path).Result;
            Process.Start(sharedUrl.Url);
        }

        private static OAuthToken AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        {
            // Authorization without callback url
            Console.Write("Getting request token...");
            OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;
            Console.WriteLine("Done.");

            OAuth1Parameters parameters = new OAuth1Parameters();
            string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(
                oauthToken.Value, parameters);
            Console.WriteLine("Redirect the user for authorization to {0}", authenticateUrl);
            Process.Start(authenticateUrl);
            Console.Write("Press [Enter] when authorization attempt has succeeded.");
            Console.ReadLine();

            Console.Write("Getting access token...");
            AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
            OAuthToken oauthAccessToken =
                dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
            Console.WriteLine("Done.");

            return oauthAccessToken;
        }
    }
}
