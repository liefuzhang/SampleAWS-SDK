using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;

namespace s3.amazon.com.docsamples {
    class GetObject {
        static string bucketName = "projects.liefuzhang.com";
        static string keyName = "testObj";
        static IAmazonS3 client;

        public static void Main(string[] args) {
            try {
                Console.WriteLine("Retrieving (GET) an object");
                string data = ReadObjectData();
                Console.WriteLine(data);
            } catch (AmazonS3Exception s3Exception) {
                Console.WriteLine(s3Exception.Message,
                                  s3Exception.InnerException);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static string ReadObjectData() {
            string responseBody = "";

            using (client = new AmazonS3Client(Amazon.RegionEndpoint.APSoutheast2)) {
                GetObjectRequest request = new GetObjectRequest {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (GetObjectResponse response = client.GetObject(request)) {
                    // write response to a file
                    // response.WriteResponseStreamToFile(destPath);
                    using (Stream responseStream = response.ResponseStream) {
                        using (StreamReader reader = new StreamReader(responseStream)) {
                            responseBody = reader.ReadToEnd();
                        }
                    }
                }
            }
            return responseBody;
        }
    }
}
