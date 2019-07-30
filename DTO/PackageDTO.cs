using System;

namespace DTO
{
    public class PackageDTO
    {
        public string Text { get; }

        
        public PackageDTO(){}
        public PackageDTO(string text){
            Text = text;
        }
    }
}
