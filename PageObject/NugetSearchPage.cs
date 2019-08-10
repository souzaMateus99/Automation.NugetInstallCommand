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
        private string packageLink { get; }
        

        public NugetSearchPage(WebPage webPage) : base(webPage){
            packageLink = string.Concat(browser.Referer, GetNugetEnterPackageLink());
        }
        

        public override BasePage Submit(){            
            return new NugetPackagePage(new Uri(packageLink), page);
        }

        private string GetNugetEnterPackageLink(){
            string tag = "a";
            
            IEnumerable<HtmlNode> links = page.Find(tag, By.Class("package-title"));

            if(links.Count() > 0){
                return links.Select(node => node.Attributes["href"].Value.Replace("/packages", string.Empty)).FirstOrDefault();
            }else{
                throw new NodeNotFoundException($"Não foi possível encontrar a tag {tag}");
            }
        }
    }
}