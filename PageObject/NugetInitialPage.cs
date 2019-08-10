using System;
using ScrapySharp.Network;
using ScrapySharp.Html.Forms;


namespace PageObject
{
    public class NugetInitialPage : BasePage
    {
        private PageWebForm _pageWebForm { get; }
        
        
        public NugetInitialPage(string contentSearch) : base(){
            _pageWebForm = GetPageWebForm(contentSearch);
        }
        

        public override BasePage Submit(){
            return new NugetSearchPage(_pageWebForm.Submit());
        }

        private PageWebForm GetPageWebForm(string contentSearch){
            PageWebForm pageWebForm = new PageWebForm(page.Html, browser);

            pageWebForm["q"] = contentSearch;
            pageWebForm.Action = "packages";
            pageWebForm.Method = HttpVerb.Get;

            return pageWebForm;
        }
    }
}
