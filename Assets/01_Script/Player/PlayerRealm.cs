
using UnityEngine;
using Realms;

namespace savesystem.realm
{
    public class PlayerRealm : RealmObject
    {
        [PrimaryKey]
        public string Name { get; set; }
        public int highScore { get; set; }  
    }
}

