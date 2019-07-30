using System;
using ScrapySharp.Network;
using ScrapySharp.Html.Forms;


namespace PageObject
{
    public class NugetInitialPage : BasePage
    {
        private string _contentSearch { get; }
        
        
        public NugetInitialPage(string contentSearch) : base(){
            _contentSearch = contentSearch;
        }

        public override BasePage Submit(){
            PageWebForm pageForm = GetPageWebForm();

            return new NugetSearchPage(pageForm.Submit());
        }

        private PageWebForm GetPageWebForm(){
            PageWebForm pageWebForm = new PageWebForm(page.Html, browser);

            pageWebForm["q"] = _contentSearch;
            pageWebForm.Action = "packages";
            pageWebForm.Method = HttpVerb.Get;

            return pageWebForm;
        }
    }
}
