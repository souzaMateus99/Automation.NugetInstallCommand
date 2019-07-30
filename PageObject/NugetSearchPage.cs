using System;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System.Collections.Generic;


namespace PageObject
{
    public class NugetSearchPage : BasePage
    {
        private WebPage _page { get; }
        

        public NugetSearchPage(WebPage webPage) : base(webPage.Browser){
            _page = webPage;
        }

        public override BasePage Submit(){
            string finalLink = GetNugetEnterPackageLink();
            string packageLink = string.Concat(browser.Referer, finalLink);
            
            return new NugetPackagePage(browser.NavigateToPage(new Uri(packageLink)));
        }

        private string GetNugetEnterPackageLink(){
            IEnumerable<HtmlNode> links = _page.Find("a", By.Class("package-title"));

            if(links.Count() > 0){
                return links.Select(tag => tag.Attributes["href"].Value.Replace("/packages", string.Empty)).FirstOrDefault();
            }else{
                throw new Exception();
            }
        }
    }
}