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

            BasePage searchPage = new NugetInitialPage(packageSearch).Submit();

            BasePage packagePage = searchPage.Submit();

            PackageDTO packageDto = packagePage.GetPackageDTO();

            Console.WriteLine(packageDto.Text);
        }
    }
}
