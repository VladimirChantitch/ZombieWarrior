using Realms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace savesystem.realm
{
    public class ZombiePositionCrud
    {
        public static ZombiePositionCrud Instance { get; private set; }

        public ZombiePositionCrud(Realm realm)
        {
            Instance = this;
            this.realm = realm;
        }

        Realm realm;

        public void CreateZombiePosition(float x,float y) {
            realm.Write(() =>
            {
                IQueryable<ZombiePositionRealm> zombiePositionRealms = realm.All<ZombiePositionRealm>();
                long currentId = zombiePositionRealms.Count() == 0 ? 1 : zombiePositionRealms.Count()+1;
                realm.Add(new ZombiePositionRealm { id = currentId, positionX= x, positionY = y });
            });
            Debug.Log(GetAllZombiesPosition().Count());
        }

        public IQueryable<ZombiePositionRealm> GetAllZombiesPosition()
        {
            return realm.All<ZombiePositionRealm>();
        }
    }
}
