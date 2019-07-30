using System;
using ScrapySharp.Network;
using ScrapySharp.Html.Forms;


namespace PageObject
{
    public class NugetInitialPage : BasePage
    {
        private WebPage _page { get; } 
        private string _contentSearch { get; }
        
        
        public NugetInitialPage(string contentSearch) : base("https://www.nuget.org"){
            _page = browser.NavigateToPage(new Uri(PrincipalUrl));
            _contentSearch = contentSearch;
        }

        public override BasePage Submit(){
            PageWebForm pageForm = GetPageWebForm();

            return new NugetSearchPage(pageForm.Submit());
        }

        private PageWebForm GetPageWebForm(){
            PageWebForm pageWebForm = new PageWebForm(_page.Html, browser);

            pageWebForm["q"] = _contentSearch;
            pageWebForm.Action = "packages";
            pageWebForm.Method = HttpVerb.Get;

            return pageWebForm;
        }
    }
}
