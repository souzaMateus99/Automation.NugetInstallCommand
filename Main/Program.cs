using DTO;
using System;
using PageObject;


namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escreva o pacote que deseja buscar:");
            string packageSearch = Console.ReadLine();

            try{
                BasePage initialPage = new NugetInitialPage(packageSearch);
                
                BasePage searchPage = initialPage.Submit();

                BasePage packagePage = searchPage.Submit();

                PackageDTO packageDto = packagePage.GetPackageDTO();

                Console.WriteLine(packageDto.Text);
            }catch(HtmlAgilityPack.NodeNotFoundException e){
                Console.WriteLine("Ocorreu um erro no programa:");
                Console.WriteLine("StackTrace:");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Mensagem:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
