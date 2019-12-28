using System;
using ScrapySharp.Network;
using PageObject.Interfaces;


namespace PageObject
{
    public abstract class BasePage : IPage
    {
        protected ScrapingBrowser browser;
        protected WebPage page;
        protected const string PRINCIPAL_URL = "https://www.nuget.org";

        protected BasePage(){
            browser = new ScrapingBrowser();
            page = browser.NavigateToPage(new Uri(PRINCIPAL_URL));
        }

        protected BasePage(WebPage page){
            this.page = page;
            this.browser = page.Browser;
        }

        protected BasePage(Uri url, WebPage page){
            this.page = page.Browser.NavigateToPage(url);
            this.browser = this.page.Browser;
        }

        public abstract IPage Submit();
        public abstract string ExtractContentOfPage();
    }
}