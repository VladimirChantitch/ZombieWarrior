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
        public PlayerCrud playerCrud { get; private set; }

        private void OnEnable()
        {
            realm = Realm.GetInstance();
            playerCrud = new PlayerCrud(realm);
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

        }
    }
}


