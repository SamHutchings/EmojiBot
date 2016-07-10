namespace EmojiBot.Api.Services
{
	using log4net;
	using Models.Facebook;
	using RestSharp;
	using System.Configuration;
	using System.Net;

	public class FacebookGraphService : IFacebookGraphService
	{
		static readonly ILog __log = LogManager.GetLogger(typeof(FacebookGraphService));

		string _url;
		string _pageToken;

		public FacebookGraphService()
		{
			_url = ConfigurationManager.AppSettings["facebook.api-url"];
			_pageToken = ConfigurationManager.AppSettings["facebook.page-token"];
		}

		public bool SendMessage(SendMessageModel model)
		{
			var response = CallFacebookAPI("/me/messages", model, Method.POST);

			if (response.StatusCode == HttpStatusCode.OK)
				return true;

			return false;
		}

		public IRestResponse CallFacebookAPI(string resource, object data, Method method)
		{
			var client = new RestClient(_url);

			var request = new RestRequest(resource, method);

			request.AddQueryParameter("access_token", _pageToken);

			request.RequestFormat = DataFormat.Json;

			request.AddJsonBody(data);

			var response = client.Post(request);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				__log.DebugFormat("Facebook API success: {0}", response.Content);
			}
			else
			{
				__log.ErrorFormat("Facebook API failure: {0}", response.Content);
			}

			return response;
		}
	}
}
