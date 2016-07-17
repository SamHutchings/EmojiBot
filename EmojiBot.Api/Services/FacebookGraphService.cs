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

		public bool SendMessage(string recpientId, string text)
		{
			var model = new SendMessageModel { recipient = new Models.Facebook.User { id = recpientId }, message = new Models.Facebook.Outbound.Message { text = text } };

			var response = CallFacebookAPI("/me/messages", model, null, Method.POST);

			if (response.StatusCode == HttpStatusCode.OK)
				return true;

			return false;
		}

		public UserDetails GetUserDetails(string id)
		{
			var response = CallFacebookAPI(
				"/" + id,
				null,
				new Dictionary<string, string>
				{
					{ "fields","first_name,last_name,locale" },
				},
				Method.GET);

			if (response == null)
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

			if (response.StatusCode != HttpStatusCode.OK)
			{
				__log.ErrorFormat("Facebook API failure: {0}", response.Content);

				return null;
			}

			return response;
		}
	}
}
