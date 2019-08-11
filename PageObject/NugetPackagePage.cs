using System;
using System.Web;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System.Collections.Generic;

namespace PageObject
{
    public class NugetPackagePage : BasePage
    {
        private string _packageInstallText { get; }
        
        public NugetPackagePage(Uri url, WebPage webPage) : base(url, webPage){
            _packageInstallText = GetTextToInstallPackage();
        }


        public override BasePage Submit(){
            return this;
        }

        public override string ExtractInformationOfPage(){
            return _packageInstallText;
        }

        private string GetTextToInstallPackage(){
            string tag = "pre";
            
            IEnumerable<HtmlNode> packageInstallText = page.Find(tag, By.Id("package-reference-text"));

            if(packageInstallText.Count() > 0){
                return HttpUtility.HtmlDecode(packageInstallText.FirstOrDefault().InnerHtml);
            }else{
                throw new NodeNotFoundException($"Não foi possível encontrar a tag {tag}");
            }
        }
    }
}