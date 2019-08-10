using System;

namespace DTO
{
    public class PackageDTO
    {
        public string Package { get; }
        public string TextToInstall { get; set; }


        public PackageDTO(string package){
            Package = package;
        }
    }
}
