using DTO;
using System;
using ScrapySharp.Network;


namespace PageObject
{
    public abstract class BasePage
    {
        protected ScrapingBrowser browser;
        protected WebPage page;
        protected string PrincipalUrl { get; }

        protected BasePage(string url){
            browser = new ScrapingBrowser();
            page = browser.NavigateToPage(new Uri(url));
            PrincipalUrl = url;
        }

        protected BasePage(WebPage page){
            this.page = page;
            this.browser = page.Browser;
        }

        protected BasePage(Uri url, WebPage page){
            this.browser = page.Browser;
            this.page = page.Browser.NavigateToPage(url);
        }

        public abstract BasePage Submit();

        public virtual PackageDTO GetPackageDTO(){
            return new PackageDTO();
        }
    }
}