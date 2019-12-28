using ScrapySharp.Network;
using PageObject.Interfaces;
using ScrapySharp.Html.Forms;

namespace PageObject
{
    public class NugetInitialPage : BasePage
    {
        private PageWebForm _pageWebForm { get; }
        
        
        public NugetInitialPage(string contentSearch) : base(){
            _pageWebForm = GetPageWebForm(contentSearch);
        }
        

        public override IPage Submit(){
            return new NugetSearchPage(_pageWebForm.Submit());
        }

        private PageWebForm GetPageWebForm(string contentSearch){
            var pageWebForm = new PageWebForm(page.Html, browser);

            pageWebForm["q"] = contentSearch;
            pageWebForm.Action = "packages";
            pageWebForm.Method = HttpVerb.Get;

            return pageWebForm;
        }

        public override string ExtractContentOfPage(){
            return page.Content;
        }
    }
}
