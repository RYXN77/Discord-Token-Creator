using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Threading;


namespace DiscordTC
{
    public class Variables
    {
        public static int Created;
        public static string username;
        public static int threads;
        public static int Errors;
        public static int CreatedPerMin;
        public static string Email;
        public static ProxyType proxyType;
        public static string[] proxies = new string[0];
        public static int AmountToCreate;
        public static string TwoCapKey;
        public static string InviteCodeForTokens;
        public static readonly object WriteLock = new object();
        public static Thread Firstthread;
        public static void Title(){
            while (true){
                Console.Title = new StringBuilder("Discord TC (github.com/yoboiiiiiii)")
                    .Append($" - Created: {Created}")
                    .Append($" - Errors: {Errors}")
                    .ToString();
            }
        }

        public static void SetProxySettings(HttpRequest req){
            req.IgnoreProtocolErrors = true;
            req.ConnectTimeout = 8000;
            req.KeepAliveTimeout = 10000;
            req.ReadWriteTimeout = 10000;
            req.SslCertificateValidatorCallback = (RemoteCertificateValidationCallback)Delegate.Combine(req.SslCertificateValidatorCallback,
            new RemoteCertificateValidationCallback((object obj, X509Certificate cert, X509Chain ssl, SslPolicyErrors error) => (cert as X509Certificate2).Verify()));
            string[] proxy = getProxy().Split(':');
            ProxyClient proxyClient = proxyType == ProxyType.Socks5 ? new Socks5ProxyClient(proxy[0], int.Parse(proxy[1])) : proxyType == ProxyType.Socks4 ? new Socks4ProxyClient(proxy[0], int.Parse(proxy[1])) : (ProxyClient)new HttpProxyClient(proxy[0], int.Parse(proxy[1]));
            if (proxy.Length == 4){
                proxyClient.Username = proxy[2];
                proxyClient.Password = proxy[3];
            }
            req.Proxy = proxyClient;
        }
        static string getProxy(){
            return Variables.proxies.ElementAt(new Random().Next(0, Variables.proxies.Length)); ;
        }

    }
}
