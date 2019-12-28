using System;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Html;
using ScrapySharp.Network;
using PageObject.Interfaces;

namespace PageObject
{
    public class NugetSearchPage : BasePage
    {
        private string packageLink { get; }
        

        public NugetSearchPage(WebPage webPage) : base(webPage){
            packageLink = string.Concat(browser.Referer, GetNugetEnterPackageLink());
        }
        

        public override IPage Submit(){            
            return new NugetPackagePage(new Uri(packageLink), page);
        }

        private string GetNugetEnterPackageLink(){
            var tag = "a";
            var tagClass = "package-title";

            var links = page.Find(tag, By.Class(tagClass));

            if(links.Any()){
                return links
                        .Select(node => ClearLinkText(node.Attributes["href"].Value))
                        .FirstOrDefault();
            }else{
                throw new NodeNotFoundException($"Não foi possível encontrar a tag {tag}");
            }
        }

        private string ClearLinkText(string link){
            return link.Replace("/packages", string.Empty);
        }

        public override string ExtractContentOfPage(){
            return page.Content;
        }
    }
}