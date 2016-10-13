using Microsoft.AspNet.WebHooks;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Api.Provisioner
{
    //https://<host>/api/webhooks/incoming/genericjson/provision?code=6d1b2182fb4891253682a4f25152f585d3a7a3e4f04c609038ea1172627b066c

    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public GenericJsonWebHookHandler()
        {
            this.Receiver = "genericjson";
        }

        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            if (context.Request.Method != HttpMethod.Post)
                return Task.FromResult(true);

            if (string.Compare(context.Id, "provision", true) == 0)
            {
                JObject incoming = context.GetDataOrDefault<JObject>();

                string name = incoming["name"].ToString();

                context.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                context.Response.Content = new StringContent($"Hello {name} !!");
            }


            return Task.FromResult(true);
        }


    }
}