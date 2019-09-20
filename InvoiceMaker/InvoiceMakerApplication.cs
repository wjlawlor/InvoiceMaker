using InvoiceMaker.Data;
using InvoiceMaker.Initialization;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;

namespace InvoiceMaker
{
    public class InvoiceMakerApplication: HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Debug.WriteLine("Application_Start");
            RouteConfiguration.AddRoutes(RouteTable.Routes);

            Database.SetInitializer(new DatabaseInitializer());
        }

        protected void HandleBeginRequest(object sender, EventArgs e) { Debug.WriteLine("HandleBeginRequest"); }
        protected void HandleAuthenticateRequest(object sender, EventArgs e) { Debug.WriteLine("HandleAuthenticateRequest"); }
        protected void HandlePostAuthenticateRequest(object sender, EventArgs e) { Debug.WriteLine("HandlePostAuthenticateRequest"); }
        protected void HandleAuthorizeRequest(object sender, EventArgs e) { Debug.WriteLine("HandleAuthorizeRequest"); }
        protected void HandlePostAuthorizeRequest(object sender, EventArgs e) { Debug.WriteLine("HandlePostAuthorizeRequest"); }
        protected void HandleResolveRequestCache(object sender, EventArgs e) { Debug.WriteLine("HandleResolveRequestCache"); }
        protected void HandlePostResolveRequestCache(object sender, EventArgs e) { Debug.WriteLine("HandlePostResolveRequestCache"); }
        protected void HandleMapRequestHandler(object sender, EventArgs e) { Debug.WriteLine("HandleMapRequestHandler"); }
        protected void HandlePostMapRequestHandler(object sender, EventArgs e) { Debug.WriteLine("HandlePostMapRequestHandler"); }
        protected void HandleAcquireRequestState(object sender, EventArgs e) { Debug.WriteLine("HandleAcquireRequestState"); }
        protected void HandlePostAcquireRequestState(object sender, EventArgs e) { Debug.WriteLine("HandlePostAcquireRequestState"); }
        protected void HandlePreRequestHandlerExecute(object sender, EventArgs e) { Debug.WriteLine("HandlePreRequestHandlerExecute"); }

        protected void HandlePostRequestHandlerExecute(object sender, EventArgs e) { Debug.WriteLine("HandlePostRequestHandlerExecute"); }
        protected void HandleReleaseRequestState(object sender, EventArgs e) { Debug.WriteLine("HandleReleaseRequestState"); }
        protected void HandlePostReleaseRequestState(object sender, EventArgs e) { Debug.WriteLine("HandlePostReleaseRequestState"); }
        protected void HandleUpdateRequestCache(object sender, EventArgs e) { Debug.WriteLine("HandleUpdateRequestCache"); }
        protected void HandlePostUpdateRequestCache(object sender, EventArgs e) { Debug.WriteLine("HandlePostUpdateRequestCache"); }

        protected void HandleLogRequest(object sender, EventArgs e) { Debug.WriteLine("HandleLogRequest"); }
        protected void HandlePostLogRequest(object sender, EventArgs e) { Debug.WriteLine("HandlePostLogRequest"); }
        protected void HandleEndRequest(object sender, EventArgs e) { Debug.WriteLine("HandleEndRequest"); }


        public override void Init()
        {
            base.Init();
            BeginRequest += HandleBeginRequest;
            AuthenticateRequest += HandleAuthenticateRequest;
            PostAuthenticateRequest += HandlePostAuthenticateRequest;
            AuthorizeRequest += HandleAuthorizeRequest;
            PostAuthorizeRequest += HandlePostAuthorizeRequest;
            ResolveRequestCache += HandleResolveRequestCache;
            PostResolveRequestCache += HandlePostResolveRequestCache;
            MapRequestHandler += HandleMapRequestHandler;
            PostMapRequestHandler += HandlePostMapRequestHandler;

            // Uncomment these and subscribe to them like above
            AcquireRequestState += HandleAcquireRequestState;
            PostAcquireRequestState += HandlePostAcquireRequestState;
            PreRequestHandlerExecute += HandlePreRequestHandlerExecute;
            PostRequestHandlerExecute += HandlePostRequestHandlerExecute;
            ReleaseRequestState += HandleReleaseRequestState;
            PostReleaseRequestState += HandlePostReleaseRequestState;
            UpdateRequestCache += HandleUpdateRequestCache;
            PostUpdateRequestCache += HandlePostUpdateRequestCache;
            LogRequest += HandleLogRequest;
            PostLogRequest += HandlePostLogRequest;
            EndRequest += HandleEndRequest;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Debug.WriteLine("Application_End"); // For posterity.
        }
    }
}