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
            realm = Realm.GetInstance();
            new PlayerCrud(realm);
        }

        private void OnDisable()
        {
            realm.Dispose();
        }

        internal void LoadGame()
        {

        }

        internal void SaveGame()
        {
            List<UnityEngine.Object> savables = FindObjectsOfType<UnityEngine.Object>().Where(o => o is ISavable).ToList();
            savables.ForEach(s =>
            {
                //s.Save(); //TODO
            });
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


