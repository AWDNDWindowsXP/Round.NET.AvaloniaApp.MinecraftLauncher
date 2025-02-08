﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using BedrockPlug.View.Pages;
using Newtonsoft.Json.Linq;
using Round.NET.AvaloniaApp.MinecraftLauncher.Modules;

namespace BedrockPlug
{
    class Main
    {
        public static void InitPlug()
        {
            Core.API.RegisterDownloadPage(new Core.API.NavigationRouteConfig()
            {
                Title = "基岩版下载",
                Route = "BedrockDownload",
                Page = new BedrockDownload()
            });


            /*// 根据特定版本号获取下载链接
            string specificVersion = "1.16.200.51"; // 示例版本号
            var versionInfo = versions.Find(v => v.Version == specificVersion);
            if (versionInfo != null)
            {
                string downloadUrl = await GetDownloadUrl(versionInfo.UUID, versionInfo.Revision);
                if (downloadUrl != null)
                {
                    Console.WriteLine($"Download URL for {specificVersion}: {downloadUrl}");
                }
                else
                {
                    Console.WriteLine($"No download URL found for {specificVersion}");
                }
            }
            else
            {
                Console.WriteLine($"Version {specificVersion} not found");
            }*/
        }

        /*private static async Task<List<VersionInfo>> GetVersions(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);
                JArray versionArray = JArray.Parse(json);
                List<VersionInfo> versions = new List<VersionInfo>();

                foreach (JObject versionObj in versionArray)
                {
                    string version = versionObj[0].ToString();
                    string uuid = versionObj[1].ToString();
                    string revision = versionObj[2].ToString();

                    versions.Add(new VersionInfo
                    {
                        Version = version,
                        UUID = uuid,
                        Revision = revision
                    });
                }

                return versions;
            }
        }

        private static async Task<string> GetDownloadUrl(string updateIdentity, string revisionNumber)
        {
            Console.WriteLine("uuid=" + updateIdentity);
            Console.WriteLine("revisionNumber=" + revisionNumber);

            // 模拟PostXmlAsync方法（需要根据实际情况实现）
            XDocument result = await PostXmlAsync("https://example.com/getDownloadUrl", BuildDownloadRequest(updateIdentity, revisionNumber));
            Debug.WriteLine($"GetDownloadUrl() response for updateIdentity {updateIdentity}, revision {revisionNumber}:\n{result.ToString()}");

            foreach (string s in ExtractDownloadResponseUrls(result))
            {
                if (s.StartsWith("http://tlu.dl.delivery.mp.microsoft.com/"))
                    return s;
            }

            return null;
        }

        private static XDocument PostXmlAsync(string url, XDocument request)
        {
            // 模拟HTTP POST请求（需要根据实际情况实现）
            // 这里返回一个示例响应
            return XDocument.Parse("<response><url>http://tlu.dl.delivery.mp.microsoft.com/file.aspx?fileId=123456789</url></response>");
        }

        private static XDocument BuildDownloadRequest(string updateIdentity, string revisionNumber)
        {
            // 构建请求XML
            return new XDocument(
                new XElement("request",
                    new XElement("updateIdentity", updateIdentity),
                    new XElement("revisionNumber", revisionNumber)
                )
            );
        }

        private static List<string> ExtractDownloadResponseUrls(XDocument response)
        {
            // 提取响应中的URL
            List<string> urls = new List<string>();
            foreach (var urlElement in response.Descendants("url"))
            {
                urls.Add(urlElement.Value);
            }
            return urls;
        }*/
    }
}

