using System;
using System.Web;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Html;
using ScrapySharp.Network;
using PageObject.Interfaces;

namespace PageObject
{
    public class NugetPackagePage : BasePage
    {
        private string _packageInstallText { get; }
        
        public NugetPackagePage(Uri url, WebPage webPage) : base(url, webPage){
            _packageInstallText = GetTextToInstallPackage();
        }


        public override IPage Submit(){
            return this;
        }

        public override string ExtractContentOfPage(){
            return _packageInstallText;
        }

        private string GetTextToInstallPackage(){
            var tag = "pre";
            var tagId = "package-reference-text";
            
            var packageInstallText = page.Find(tag, By.Id(tagId));

            if(packageInstallText.Any()){
                return HttpUtility.HtmlDecode(packageInstallText.FirstOrDefault().InnerHtml);
            }else{
                throw new NodeNotFoundException($"Não foi possível encontrar a tag {tag}");
            }
        }
    }
}