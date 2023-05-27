using UnityEngine;
using Realms;

namespace savesystem.realm
{
    public class ZombiePositionRealm : RealmObject
    {
        [PrimaryKey]
        public long id{ get; set; }
        public float positionX{ get; set; }
        public float positionY{ get; set; }
    }
}
