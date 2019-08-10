using DTO;
using System;
using Service;
using PageObject;


namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escreva o pacote que deseja buscar:");
            PackageDTO packageDto = new PackageDTO(Console.ReadLine());

            Console.WriteLine("Escreva o caminho do projeto que deseja colocar o pacote: (Se não escrever nada, o pacote será exibido na tela)");
            string projectPath = Console.ReadLine();

            try{
                BasePage initialPage = new NugetInitialPage(packageDto.Package);
                
                BasePage searchPage = initialPage.Submit();

                BasePage packagePage = searchPage.Submit();

                packageDto.TextToInstall = searchPage.ExtractInformationOfPage();

                if(string.IsNullOrEmpty(projectPath)){
                    Console.WriteLine(packageDto.TextToInstall);
                }else{
                    new PackageService().AddProjectReference(packageDto, projectPath);
                }
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
