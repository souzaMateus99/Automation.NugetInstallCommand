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
        public NugetPackagePage(Uri url, WebPage webPage) : base(url, webPage){}

        public override BasePage Submit(){
            return this;
        }

        public override PackageDTO GetPackageDTO(){
            return new PackageDTO(GetTextToInstallPackage());
        }

        private string GetTextToInstallPackage(){
            IEnumerable<HtmlNode> packageInstallText = page.Find("pre", By.Id("package-manager-text"));

            if(packageInstallText.Count() > 0){
                return packageInstallText.FirstOrDefault().InnerHtml;
            }else{
                throw new Exception();
            }
        }
    }
}