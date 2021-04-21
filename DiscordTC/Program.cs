using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;
using System.Threading;
using Leaf.xNet;
using System.Diagnostics;

namespace DiscordTC
{
    
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.ForegroundColor = Color.White;
            Console.Title = new StringBuilder("Discord TC ")
                 .Append("(github.com/yoboiiiiiii)")
                 .ToString();
            Settings.AutoLoadSettings();
            RETR1:
            Console.Write("Amount of tokens: ");
            try {
                Variables.AmountToCreate = int.Parse(Console.ReadLine());
            }
            catch {
                Console.WriteLine("Error, Parsing Integor!");
                goto RETR1;
            }
            Console.Write("Invite Code (To Spam Servers): ");
            Variables.InviteCodeForTokens = Console.ReadLine();
            Console.Write("Username of Tokens: ");
            Variables.username = Console.ReadLine();
            Variables.proxies = Import.Load("proxies");
            Console.WriteLine();
            new Thread(() => Variables.Title()).Start();
            Function(); 
        

        }

        static void Function()
        {
            for (int i = 0; i < Variables.AmountToCreate; i++) {
                threads.Add(new Thread(() => worker()));
                threads[i].Start();
            }
            for (int i = 0; i < Variables.AmountToCreate; i++)
                threads[i].Join();
        }

        static void worker()
        {
            using (HttpRequest req = new HttpRequest()) {
                string email = GetEmail.getEmail();
           RETRY:
                try {
                    Variables.SetProxySettings(req);

                    var RESP = TwoCap.SolveHcaptcha("f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34", "https://discord.com/api/v8/auth/register");

                    Console.WriteLine("Solved Captcha!");

                    req.AddHeader("cookie", "__cfduid=d154abf3b17e37f73e096a0c6c57c27bd1617381992; locale=en-US; _ga=GA1.2.837933949.1617381999; _gid=GA1.2.2023038443.1617381999");
                    req.AddHeader("origin", "https://discord.com");
                    req.AddHeader("referer", "https://discord.com/register?email=" + email);
                    req.AddHeader("sec-ch-ua", "\"Google Chrome\";v=\"89\", \"Chromium\";v=\"89\", \";Not A Brand\";v=\"99\"");
                    req.AddHeader("sec-ch-ua-mobile", "?0");
                    req.AddHeader("sec-fetch-dest", "empty");
                    req.AddHeader("sec-fetch-mode", "cors");
                    req.AddHeader("sec-fetch-site", "same-origin");
                    req.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36");
                    req.AddHeader("x-fingerprint", "827584826902183957.mWC2v8NUY_O8p4FsA_n0cTR7xR8");
                    req.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6ImVuLVVTIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzg5LjAuNDM4OS4xMTQgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6Ijg5LjAuNDM4OS4xMTQiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6ODEyNzcsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9");

                    var res0 = req.Post("https://discord.com/api/v8/auth/register", "{\"fingerprint\":\"827584826902183957.mWC2v8NUY_O8p4FsA_n0cTR7xR8\",\"email\":\"" + email + "\",\"username\":\"" + Variables.username + "\",\"password\":\"Badboy12$$\",\"invite\":\"" + Variables.InviteCodeForTokens + "\",\"consent\":true,\"date_of_birth\":\"1995-06-06\",\"gift_code_sku_id\":null,\"captcha_key\":\"" + RESP + "\"}", "application/json");
                    string text0 = res0.ToString();

                    if (text0.Contains("token")) {
                        var TOKEN = Functions.JSON(text0, "token").FirstOrDefault();
                        lock (Variables.WriteLock) {
                            File.AppendAllText("Tokens.txt", TOKEN + Environment.NewLine);
                            Console.WriteLine("Token Created: " + TOKEN, Color.Green);
                        }
                        Variables.Created++;
                        Variables.AmountToCreate--;

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
            }
        }

        public static List<Thread> threads = new List<Thread>();
    }
}