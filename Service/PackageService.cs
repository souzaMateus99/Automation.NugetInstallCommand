using DTO;
using System.IO;
using System.Xml;
using System.Text;


namespace Service
{
    public class PackageService
    {
        private PackageDTO _packageDto {get;}


        public PackageService(PackageDTO packageDto){
            _packageDto = packageDto;
        }
        

        public void AddProjectReference(string projectPath){
            var projectFileContent = ReadFile(projectPath);

            var xml = new XmlDocument();
            xml.LoadXml(projectFileContent);

            var xmlNodeList = xml.SelectNodes("//ItemGroup //PackageReference");

            var newContent = string.Empty;

            if(xmlNodeList.Count > 0){
                var lastPackageNode = xmlNodeList.Item(xmlNodeList.Count - 1);

                newContent = projectFileContent.Replace(lastPackageNode.OuterXml, string.Concat(lastPackageNode.OuterXml, _packageDto.TextToInstall));
            }else{
                newContent = projectFileContent.Replace("</Project>", string.Concat($"<ItemGroup>{_packageDto.TextToInstall}</ItemGroup>", "</Project>"));
            }

            WriteFile(projectPath, newContent.Replace("</Project>", "\n\n</Project>"));
        }

        private string ReadFile(string projectPath){
            var fileContent = new StringBuilder();
            
            using(var fileStream = OpenFile(projectPath))
            using(var streamReader = new StreamReader(fileStream)){
                fileContent.Append(streamReader.ReadLine());
                
                while(streamReader.Read() != -1){
                    fileContent.Append(streamReader.ReadLine());
                }
            }

            return fileContent.ToString();
        }

        private void WriteFile(string projectPath, string text){
            using(var fileStream = OpenFile(projectPath))
            using(var streamWriter = new StreamWriter(fileStream)){
                streamWriter.Write(text);
                streamWriter.Flush();
            }
        }

        private FileStream OpenFile(string filePath){
            return new FileStream(filePath, FileMode.Open);
        }
    }
}
