using System;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Html;
using ScrapySharp.Network;
using ScrapySharp.Html.Forms;
using System.Collections.Generic;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escreva o pacote que deseja buscar:");

            ScrapySharpTest(Console.ReadLine());
        }

  
        public static void ScrapySharpTest(string packageSearch){
            ScrapingBrowser browser = new ScrapingBrowser();

            WebPage page = browser.NavigateToPage(new Uri("https://www.nuget.org"));

            PageWebForm form = new PageWebForm(page.Html, browser);
            form["q"] = packageSearch;
            form.Action = "packages";
            form.Method = HttpVerb.Get;
            
            WebPage searchScrapyPage = form.Submit();

            if(searchScrapyPage != null){
                IEnumerable<HtmlNode> scrapyLink = searchScrapyPage.Find("a", By.Class("package-title"));

                if(scrapyLink.Count() > 0){
                    string scrapySharpLink = scrapyLink.Select(tag => tag.Attributes["href"].Value.Replace("/packages", string.Empty)).FirstOrDefault();

                    WebPage scrapySharpPage = browser.NavigateToPage(new Uri(string.Concat(browser.Referer, scrapySharpLink)));

                    Console.WriteLine(scrapySharpPage.Find("pre", By.Id("package-manager-text")).FirstOrDefault().InnerHtml);
                }else{
                    Console.WriteLine("Não foi possível capturar o Link");
                }
            }else{
                Console.Write($"Instancia nula {nameof(searchScrapyPage)}");
            }
        }
    }
}
