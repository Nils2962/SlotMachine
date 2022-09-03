using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class DataController
    {
        private string applicationDataPath;
        private string folderpath;
        private string filepath;

        public DataController()
        {
            this.applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.folderpath = this.applicationDataPath + "\\Casino";
            this.filepath = this.folderpath + "\\Profiles.prf";

            if (!Directory.Exists(this.folderpath))
            {
                Directory.CreateDirectory(this.folderpath);
            }
        }

        public void SaveProfiles(List<Profile> profiles)
        {
            FileStream fileStream = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            fileStream.Flush();

            SaveObject saveObject = new SaveObject();
            saveObject.Profiles = profiles;

            string json = JsonConvert.SerializeObject(saveObject, Formatting.Indented);

            streamWriter.Write(json);

            streamWriter.Close();
            fileStream.Close();
        }

        public List<Profile> LoadProfiles()
        {
            FileStream fileStream = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            string json = streamReader.ReadToEnd();

            SaveObject saveObject = JsonConvert.DeserializeObject<SaveObject>(json);

            streamReader.Close();
            fileStream.Close();

            if (saveObject == null)
                return null;
            else
                return saveObject.Profiles;
        }
    }
}
