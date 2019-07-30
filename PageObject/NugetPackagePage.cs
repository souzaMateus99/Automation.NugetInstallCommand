using DTO;
using System;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System.Collections.Generic;


namespace PageObject
{
    public class NugetPackagePage : BasePage
    {
        private WebPage _page { get; }


        public NugetPackagePage(WebPage webPage) : base(webPage.Browser){
            _page = webPage;
        }

        public override BasePage Submit(){
            return this;
        }

        public override PackageDTO GetPackageDTO(){
            return new PackageDTO(GetTextToInstallPackage());
        }

        private string GetTextToInstallPackage(){
            IEnumerable<HtmlNode> packageInstallText = _page.Find("pre", By.Id("package-manager-text"));

            if(packageInstallText.Count() > 0){
                return packageInstallText.FirstOrDefault().InnerHtml;
            }else{
                throw new Exception();
            }
        }
    }
}