﻿using System;
using Amazon.S3;
using Amazon.S3.Model;

namespace s3.amazon.com.docsamples {
    class UploadObject {
        static string bucketName = "*** bucket name ***";
        static string keyName = "*** key name when object is created ***";
        static string filePath = "*** absolute path to a sample file to upload ***";

        static IAmazonS3 client;

        public static void Main(string[] args) {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1)) {
                Console.WriteLine("Uploading an object");
                WritingAnObject();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void WritingAnObject() {
            try {
                PutObjectRequest putRequest1 = new PutObjectRequest {
                    BucketName = bucketName,
                    Key = keyName,
                    ContentBody = "sample text"
                };

                PutObjectResponse response1 = client.PutObject(putRequest1);

                // 2. Put object-set ContentType and add metadata.
                PutObjectRequest putRequest2 = new PutObjectRequest {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "text/plain"
                };
                putRequest2.Metadata.Add("x-amz-meta-title", "someTitle");

                PutObjectResponse response2 = client.PutObject(putRequest2);

            } catch (AmazonS3Exception amazonS3Exception) {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))) {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                } else {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }
    }
}