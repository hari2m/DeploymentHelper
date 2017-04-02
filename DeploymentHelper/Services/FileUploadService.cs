using System;
using System.IO;
using System.Web;

namespace DeploymentHelper.Services
{
    public class FileUploadService
    {
        public static bool SaveFile(HttpFileCollection file)
        {
            HttpFileCollection myFileCollection = file;

            //for (int loop1 = 0; loop1 < myFileCollection.Count; loop1++)
            //{
            //    // Create a new file name.
            //    var tempFileName = "C:\\TempFiles\\File_" + loop1;
            //    // Save the file.
            //    myFileCollection[loop1].SaveAs(tempFileName);
            //}
            return true;
        }
    }
}