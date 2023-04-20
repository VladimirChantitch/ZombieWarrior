using Realms;
using savesystem.dto;
using savesystem.realm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace savesystem
{
    /// <summary>
    /// This is a simple save and load system in JSON string, but i would most advise you to 
    /// use Mongo Db realm if you wish expand your project after the jam
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        Realm realm;

        private void OnEnable()
        {
            var config = new RealmConfiguration
            {
                ShouldDeleteIfMigrationNeeded = true,
            };
            realm = Realm.GetInstance(config);
            new PlayerCrud(realm);
#if (UNITY_EDITOR)
            PlayerCrud.Instance.CreateNewPlayer("");
            PlayerCrud.Instance.RemovePlayer("");
            PlayerCrud.Instance.CreateNewPlayer("");
#endif
        }

        private void OnDisable()
        {
            realm.Dispose();
        }

#if UNITY_EDITOR
        [SerializeField] bool deleteDB;
        private void Update()
        {
            if (deleteDB)
            {
                deleteDB = false;
                removeAllInDb();
            }
        }

        void removeAllInDb()
        {
            realm.Write(() =>
            {
                realm.RemoveAll();
            });
        }
#endif
    }
}


