using System;
using ScrapySharp.Network;


namespace PageObject
{
    public abstract class BasePage
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

        public abstract BasePage Submit();

        public virtual string ExtractContentOfPage(){
            return page.Content;
        }
    }
}