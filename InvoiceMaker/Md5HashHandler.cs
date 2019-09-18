using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InvoiceMaker
{
    public class Md5HashHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            Uri siteUri;
            HttpRequest httpRequest;
            string absolutePath;
            string getExtension;
            string getFileName;
            string[] header = context.Request.Headers.AllKeys;

            httpRequest = context.Request;
            siteUri = httpRequest.Url;
            absolutePath = siteUri.AbsolutePath;
            getExtension = Path.GetExtension(absolutePath); // What file type is being requested.
            getFileName = Path.GetFileNameWithoutExtension(absolutePath); // What file content is in the request.

            byte[] hash = CalculateMD5Hash(getFileName);

            if (getExtension == ".hash")
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                context.Response.Write("Hello world! ");
                context.Response.Write(sb);
            }
            if (getExtension == ".binhash")
            {
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(hash);
            }

        }
    
        public byte[] CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            return hash;
        }
    }
}