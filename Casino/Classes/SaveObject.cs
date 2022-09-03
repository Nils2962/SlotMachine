using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class SaveObject
    {
        public List<Profile> Profiles { get => profiles; set => profiles = value; }

        private List<Profile> profiles;

        public SaveObject()
        {
            Profiles = new List<Profile>();
        }

    }
}
