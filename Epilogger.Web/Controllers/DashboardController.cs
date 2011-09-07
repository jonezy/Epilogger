﻿using System.Web.Mvc;
using System.Collections;
using Epilogger.Data;
using System.Collections.Generic;

namespace Epilogger.Web.Controllers {
    [RequiresAuthentication(AccessDeniedMessage = "You must be logged in to view your dashboard")]
    public class DashboardController : BaseController {
        TweetService tweetService;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext) {
            if (tweetService == null) tweetService = new TweetService();

            base.Initialize(requestContext);
        }

        public ActionResult Index() {
            IEnumerable<Tweet> tweets = tweetService.FindByUserScreenName(CurrentUserTwitterAuthorization.ServiceUsername);

            return View();
        }

    }
}