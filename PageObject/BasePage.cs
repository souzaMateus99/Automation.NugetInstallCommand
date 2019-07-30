using DTO;
using ScrapySharp.Network;


namespace PageObject
{
    public abstract class BasePage
    {
        protected ScrapingBrowser browser;
        protected string PrincipalUrl { get; }

        protected BasePage(string url){
            browser = new ScrapingBrowser();
            PrincipalUrl = url;
        }

        protected BasePage(ScrapingBrowser browser){
            this.browser = browser;
        }

        protected BasePage(string url, ScrapingBrowser browser){
            this.browser = browser;
            PrincipalUrl = url;
        }

        public abstract BasePage Submit();

        public virtual PackageDTO GetPackageDTO(){
            return new PackageDTO();
        }
    }
}