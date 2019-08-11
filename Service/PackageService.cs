using DTO;
using System.IO;
using System.Xml;
using System.Text;


namespace Service
{
    public class PackageService
    {
        private PackageDTO _packageDto;


        public PackageService(PackageDTO packageDto){
            _packageDto = packageDto;
        }
        

        public void AddProjectReference(string projectPath){
            string projectFileContent = ReadFile(projectPath);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(projectFileContent);

            XmlNodeList xmlNodeList = xml.SelectNodes("//ItemGroup //PackageReference");

            string newContent = string.Empty;

            if(xmlNodeList.Count > 0){
                XmlNode lastPackageNode = xmlNodeList.Item(xmlNodeList.Count - 1);

                newContent = projectFileContent.Replace(lastPackageNode.OuterXml, string.Concat(lastPackageNode.OuterXml, _packageDto.TextToInstall));
            }else{
                newContent = projectFileContent.Replace("</Project>", string.Concat($"<ItemGroup>{_packageDto.TextToInstall}</ItemGroup>", "</Project>"));
            }

            WriteFile(projectPath, newContent.Replace("</Project>", "\n\n</Project>"));
        }

        private string ReadFile(string projectPath){
            StringBuilder fileContent = new StringBuilder();
            
            using(FileStream fileStream = OpenFile(projectPath))
            using(StreamReader streamReader = new StreamReader(fileStream)){
                fileContent.Append(streamReader.ReadLine());
                
                while(streamReader.Read() != -1){
                    fileContent.Append(streamReader.ReadLine());
                }
            }

            return fileContent.ToString();
        }

        private void WriteFile(string projectPath, string text){
            using(FileStream fileStream = OpenFile(projectPath))
            using(StreamWriter streamWriter = new StreamWriter(fileStream)){
                streamWriter.Write(text);
                streamWriter.Flush();
            }
        }

        private FileStream OpenFile(string filePath){
            return new FileStream(filePath, FileMode.Open);
        }
    }
}
