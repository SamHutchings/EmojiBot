namespace EmojiBot.Api.Services
{
	using log4net;
	using Models.Facebook.Inbound;
	using Models.Facebook.Outbound;
	using Newtonsoft.Json;
	using RestSharp;
	using System.Collections.Generic;
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
			var response = CallFacebookAPI("/me/messages", model, null, Method.POST);

			if (response.StatusCode == HttpStatusCode.OK)
				return true;

			return false;
		}

		public UserDetails GetUserDetails(string id)
		{
			var response = CallFacebookAPI(
				"/1437149932977618",
				null,
				new Dictionary<string, string>
				{
					{ "fields","first_name,last_name,locale" },
				},
				Method.POST);

			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			return JsonConvert.DeserializeObject<UserDetails>(response.Content);
		}

		public IRestResponse CallFacebookAPI(string resource, object body, IDictionary<string, string> queryStringParams, Method method)
		{
			var client = new RestClient(_url);
			var request = new RestRequest(resource, method);

			request.RequestFormat = DataFormat.Json;

			request.AddQueryParameter("access_token", _pageToken);

			if (queryStringParams != null)
			{
				foreach (var item in queryStringParams)
				{
					request.AddQueryParameter(item.Key, item.Value);
				}
			}

			request.AddJsonBody(body);

			var response = client.Execute(request);

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
