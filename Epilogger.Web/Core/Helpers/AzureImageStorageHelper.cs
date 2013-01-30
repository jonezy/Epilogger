using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Epilogger.Web.Core.Helpers
{
    public class AzureImageStorageHelper
    {

        public static Uri StoreProfileImage(string filename, string containerName, Stream memStream)
        {
            try
            {
                // Variables for the cloud storage objects.
                var cloudStorageAccount1 = default(CloudStorageAccount);
                var blobClient = default(CloudBlobClient);
                var blobContainer = default(CloudBlobContainer);
                var containerPermissions = default(BlobContainerPermissions);
                var blob = default(CloudBlob);

                cloudStorageAccount1 =
                    CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureProfileImageConnectionString"]);

                // Create the blob client, which provides
                // authenticated access to the Blob service.
                blobClient = cloudStorageAccount1.CreateCloudBlobClient();

                // Get the container reference.
                blobContainer = blobClient.GetContainerReference(containerName);
                // Create the container if it does not exist.
                blobContainer.CreateIfNotExist();

                // Set permissions on the container.
                containerPermissions = new BlobContainerPermissions();
                // This sample sets the container to have public blobs. Your application
                // needs may be different. See the documentation for BlobContainerPermissions
                // for more information about blob container permissions.
                containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                blobContainer.SetPermissions(containerPermissions);

                // Get a reference to the blob.
                blob = blobContainer.GetBlobReference(filename);
                blob.Properties.ContentType = "image/jpeg";

                // Upload a file from the local system to the blob.
                //blob.UploadFile("c:\TestAzure.jpg")
                blob.UploadFromStream(memStream);

                return blob.Uri;
            }
            catch (StorageClientException e)
            {
                return new Uri("");
            }
            catch (Exception e)
            {
                return new Uri("");
            }
        }

        public static Uri StoreLogoSponsorImage(string filename, string containerName, Stream memStream)
        {
            try
            {
                // Variables for the cloud storage objects.
                var cloudStorageAccount1 = default(CloudStorageAccount);
                var blobClient = default(CloudBlobClient);
                var blobContainer = default(CloudBlobContainer);
                var containerPermissions = default(BlobContainerPermissions);
                var blob = default(CloudBlob);

                cloudStorageAccount1 = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=epiloggerphotos;AccountKey=xbSt0uQAqExzWpc60pcmP6k49Uu7raxPG1BA5+aBhrAAdNxaoiFAZ67jQmG/iiJIeFeemnp74NRuuFAXaaxGJQ==");

                // Create the blob client, which provides
                // authenticated access to the Blob service.
                blobClient = cloudStorageAccount1.CreateCloudBlobClient();

                // Get the container reference.
                blobContainer = blobClient.GetContainerReference(containerName);
                // Create the container if it does not exist.
                blobContainer.CreateIfNotExist();

                // Set permissions on the container.
                containerPermissions = new BlobContainerPermissions();
                // This sample sets the container to have public blobs. Your application
                // needs may be different. See the documentation for BlobContainerPermissions
                // for more information about blob container permissions.
                containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                blobContainer.SetPermissions(containerPermissions);

                // Get a reference to the blob.
                blob = blobContainer.GetBlobReference(filename);
                blob.Properties.ContentType = "image/jpeg";

                // Upload a file from the local system to the blob.
                //blob.UploadFile("c:\TestAzure.jpg")
                blob.UploadFromStream(memStream);

                return blob.Uri;
            }
            catch (StorageClientException e)
            {
                return new Uri("");
            }
            catch (Exception e)
            {
                return new Uri("");
            }
        }
    }
}