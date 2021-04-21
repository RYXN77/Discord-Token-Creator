using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordTC
{
	public class TwoCap
	{
		public static string APIKey = Variables.TwoCapKey;
		public static string SolveHcaptcha(string googleKey, string pageUrl)
		{
			string requestUriString = "http://2captcha.com/in.php?key=" + APIKey + "&method=hcaptcha&sitekey=" + googleKey + " &pageurl=" + pageUrl;
			try
			{
				WebRequest webRequest = WebRequest.Create(requestUriString);
				using (WebResponse webResponse = webRequest.GetResponse())
				{
					using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
					{
						string text = streamReader.ReadToEnd();
						if (text.Substring(0, 3) == "OK|")
						{
							string str = text.Remove(0, 3);
							for (int i = 0; i < 24; i++)
							{
								WebRequest webRequest2 = WebRequest.Create("http://2captcha.com/res.php?key=" + APIKey + "&action=get&id=" + str);
								using (WebResponse webResponse2 = webRequest2.GetResponse())
								{
									using (StreamReader streamReader2 = new StreamReader(webResponse2.GetResponseStream()))
									{
										string text2 = streamReader2.ReadToEnd();
										if (text2.Length < 3)
										{
											return string.Empty;
										}
										if (text2.Substring(0, 3) == "OK|")
										{
											return text2.Remove(0, 3);
										}
										if (text2 != "CAPCHA_NOT_READY")
										{
											return string.Empty;
										}
									}
								}
								Thread.Sleep(5000);
							}
							return string.Empty;
						}
						return string.Empty;
					}
				}
			}
			catch
			{
			}
			return string.Empty;
		}
	}

}
