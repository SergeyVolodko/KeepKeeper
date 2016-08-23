using System;
using System.Web.Http;

namespace KeeperApiClient.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        string clientId = "HC0ymXGVu5cmTDosR75qrOQDhUhuTxgV";
        string secret = "e66gys3RpaBzyGX2YJQbROASyrFrbypQ1EXo2xgyPnRskK_MjsaE2cCinV_GyuRv";
        string domain = "sergey-volodko.eu.auth0.com";

        [HttpGet]
        [Route("test")]
        [MyAuthFilter]
        public string Test()
        {
            return "it works";
        }
    }
    
}
