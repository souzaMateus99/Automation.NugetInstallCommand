using DTO;
using System;
using Service;
using PageObject;
using PageObject.Interfaces;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escreva o pacote que deseja buscar:");
            
            var packageDto = new PackageDTO(Console.ReadLine());

            Console.WriteLine("Escreva o caminho do projeto que deseja colocar o pacote:");
            Console.WriteLine("(Caso não escreva nada, o texto para instalação do pacote será exibido na tela)");
            
            string projectPath = Console.ReadLine();

            try{
                IPage initialPage = new NugetInitialPage(packageDto.Package);
                
                IPage searchPage = initialPage.Submit();

                IPage packagePage = searchPage.Submit();

                packageDto.TextToInstall = packagePage.ExtractContentOfPage();

                if(string.IsNullOrEmpty(projectPath)){
                    Console.WriteLine(packageDto.TextToInstall);
                }else{
                    new PackageService(packageDto).AddProjectReference(projectPath);
                }
            }catch(HtmlAgilityPack.NodeNotFoundException e){
                Console.WriteLine("StackTrace:");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Mensagem:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
