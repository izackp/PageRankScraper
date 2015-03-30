using System;
using System.Collections.Generic;
using TAlex.SEOStats.Alexa;
using TAlex.SEOStats.Google;

namespace pageRankScraper
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			args = Sample ();
			if (args.Length != 1) {
				ShowError ("Invalid Amount of Parameters: Must have 1 parameters. 1: url of page to rank");
				return;
			}

			string url = args [0];
			url = CleanUrl (url);

			GoogleResult googleStatistics = new GoogleStats().GetStats(url);
			var alexaStatistics = new AlexaStats().GetStats(url);

			List<string> entries = new List<string> ();
			entries.Add (JSONEntry("googlePageSuccess", googleStatistics.Success));
			if (googleStatistics.Success)
				entries.Add (JSONEntry("googlePageRank", googleStatistics.PageRank));

			entries.Add (JSONEntry("alexaSuccess", alexaStatistics.Success));
			if (alexaStatistics.Success) {
				entries.Add (JSONEntry("claimedDate", alexaStatistics.ClaimedDate.ToString()));
				entries.Add (JSONEntry("linksIn", alexaStatistics.LinksIn));
				entries.Add (JSONEntry("rankDelta", alexaStatistics.RankDelta));
				entries.Add (JSONEntry("reachRank", alexaStatistics.ReachRank));
				entries.Add (JSONEntry("trafficRank", alexaStatistics.TrafficRank));
			}

			string jsonContent = string.Join (JSONDelimiter, entries);

			//build json
			Console.WriteLine ("{\n" + jsonContent +"\n}");
		}

		static string JSONEntry(string key, int value) {
			return "\"" + key + "\": " + value;
		}

		static string JSONEntry(string key, bool value) {
			return "\"" + key + "\": " + value;
		}

		static string JSONEntry(string key, string value) {
			return "\"" + key + "\": \"" + value + "\"";
		}

		const string JSONDelimiter = ",\n";

		public static string CleanUrl(string url) {
			if (url == null)
				return null;

			if (url.Length >= 8) {
			
				string httpProtocol = url.Substring (0, 7);
				string httpsProtocol = url.Substring (0, 8);
				if (httpProtocol != "http://" && httpsProtocol != "https://")
					url = "http://" + url;
			} else {
				url = "http://" + url;
			}
			return url;
		}

		public static string[] Sample() {
			const string rankingSite = "http://google.com";
			return new string[] { rankingSite};
		}

		public static void ShowError(string error) {
			Console.WriteLine ("{\"error\": \"" + error +"\"}");
		}
	}
}
