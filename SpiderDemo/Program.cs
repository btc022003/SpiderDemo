using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace SpiderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string url = "http://tech.sina.com.cn/apple/";
                var uri = new Uri(url);
                var browser = new ScrapingBrowser();

                var htmlData = browser.DownloadString(uri);
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(htmlData);
                var html = htmlDocument.DocumentNode;

                ///////////使用css选择器选取节点
                var news_list = html.CssSelect(".news-item");
                foreach (var item_news in news_list)
               {
                   var news_title = item_news.CssSelect("h2 a");
                   var news_img = item_news.CssSelect(".iarticle_pic a img"); ///////读取大图

                   if (news_title.Count() > 0)
                   {
                       Console.WriteLine(news_title.ElementAt(0).InnerText);
                   }
                   if (news_img.Count() > 0)
                   {
                       Console.WriteLine(news_img.ElementAt(0).Attributes["src"].Value);
                   }
                   else
                   {
                       ///////如果大图不存在
                       var news_small_img = item_news.CssSelect(".img a img");
                       if (news_small_img.Count() > 0)
                       {
                           Console.WriteLine(news_small_img.ElementAt(0).Attributes["src"].Value);
                       }
                   }

                   Console.WriteLine("----------------------------------------------------------------------");
               }
               
                
                Console.ReadLine();
            }
            catch (Exception e)
            {

            }
        }
    }
}
