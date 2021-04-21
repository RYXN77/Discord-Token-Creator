using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;

namespace DiscordTC
{
    public class GetEmail{
        public static string getEmail(){
            using (HttpRequest req = new HttpRequest()){
                RETRY:
                Variables.SetProxySettings(req);
                try{
                    req.AddHeader("scheme", "https");
                    req.AddHeader("accept", "*/*");
                    req.AddHeader("accept-encoding", "null");
                    req.AddHeader("accept-language", "en-US,en;q=0.9");
                    req.AddHeader("content-length", "114");
                    req.AddHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
                    req.AddHeader("cookie", "__cfduid=dafb0f483afa752f14056bda4bf966ed61617395418; csrf_gmailnator_cookie=be578eaa80e74aa3ed90c0bb0a45342b; ci_session=b587b72689ac7f1c835213001f9fe3ac3fa4c149; _ga=GA1.2.1471325870.1617395419; _gid=GA1.2.2064389942.1617395419; _gat_gtag_UA_123576815_1=1; __gads=ID=9f9840ecd5514adf:T=1617395424:S=ALNI_MY8WPDSYsJYS_s9CU9F_I3ACriwBA");
                    req.AddHeader("origin", "https://www.gmailnator.com");
                    req.AddHeader("referer", "https://www.gmailnator.com/");
                    req.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \";Not A Brand\";v=\"99\"");
                    req.AddHeader("sec-ch-ua-mobile", "?0");
                    req.AddHeader("sec-fetch-dest", "empty");
                    req.AddHeader("sec-fetch-mode", "cors");
                    req.AddHeader("sec-fetch-site", "same-origin");
                    req.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36");
                    req.AddHeader("x-requested-with", "XMLHttpRequest");

                    var res0 = req.Post("https://www.gmailnator.com/index/indexquery", "csrf_gmailnator_token=be578eaa80e74aa3ed90c0bb0a45342b&action=GenerateEmail&data%5B%5D=1&data%5B%5D=2&data%5B%5D=3", "application/x-www-form-urlencoded; charset=UTF-8");
                    string text0 = res0.ToString();

                    if (!text0.Contains("Too Many Requests")) {
                        return text0;
                    }
                    else {
                        Variables.Errors++;
                        goto RETRY;
                    }
                }
                catch {
                    Variables.Errors++;
                    goto RETRY;
                }
                return "";


            }
        }
    }
}

//Too Many Requests