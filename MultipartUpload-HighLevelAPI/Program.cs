using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace s3.amazon.com.docsamples {
    class UploadFileMPUHighLevelAPI {
        static string existingBucketName = "projects.liefuzhang.com";
        static string keyName = "testObj1";
        static string filePath = "C:\\Users\\liefu.zhang\\Desktop\\Hello world1.txt";

        static void Main(string[] args) {
            try {
                TransferUtility fileTransferUtility = new
                    TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.APSoutheast2));

                // 1. Upload a file, file name is used as the object key name.
                fileTransferUtility.Upload(filePath, existingBucketName);
                Console.WriteLine("Upload 1 completed");

                // 2. Specify object key name explicitly.
                fileTransferUtility.Upload(filePath,
                                          existingBucketName, keyName);
                Console.WriteLine("Upload 2 completed");

                // 3. Upload data from a type of System.IO.Stream.
                using (FileStream fileToUpload =
                    new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                    fileTransferUtility.Upload(fileToUpload,
                                               existingBucketName, keyName);
                }
                Console.WriteLine("Upload 3 completed");

                // 4.Specify advanced settings/options.
                TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest {
                    BucketName = existingBucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.ReducedRedundancy,
                    PartSize = 6291456, // 6 MB.
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                fileTransferUtility.Upload(fileTransferUtilityRequest);
                Console.WriteLine("Upload 4 completed");
                Console.Read();
            } catch (AmazonS3Exception s3Exception) {
                Console.WriteLine(s3Exception.Message,
                                  s3Exception.InnerException);
            }
        }
    }
}