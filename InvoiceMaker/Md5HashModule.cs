using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker
{
    public class Md5HashModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += HandleBeginRequest;
        }

        private void HandleBeginRequest(object sender, EventArgs e)
        {
            var downcastSender = (InvoiceMakerApplication)sender;

            HttpContext context = downcastSender.Context;
            HttpRequest httpRequest = context.Request;
            Uri siteUri = httpRequest.Url;

            if (downcastSender != null)
            {
                string path = context.Request.FilePath;
                if (siteUri.Segments.Length > 2)
                {
                    string concatSegs = siteUri.Segments[1] + siteUri.Segments[2];
                    if (siteUri.Segments[1] == "api/")
                    {
                        if (siteUri.Segments[2] == "hash/")
                        {
                            string valueToHash = path.Replace(concatSegs, string.Empty);
                            context.RewritePath($"/{valueToHash}.hash");
                        }
                        if (siteUri.Segments[2] == "binhash/")
                        {
                            string valueToHash = path.Replace(concatSegs, string.Empty);
                            context.RewritePath($"/{valueToHash}.binhash");
                        }
                    }                
                }
            }            
        }
    }
}
