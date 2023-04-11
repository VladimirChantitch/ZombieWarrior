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
        PlayerRealm playerRealm;

        private void OnEnable()
        {
            realm = Realm.GetInstance();
        }

        private void OnDisable()
        {
            realm.Dispose();
        }

        internal void LoadGame()
        {

        }

        /// <summary>
        /// Generates one and only one file that contains all the save data of all your ISavable In the scene
        /// </summary>
        internal void SaveGame()
        {
            List<UnityEngine.Object> savables = FindObjectsOfType<UnityEngine.Object>().Where(o => o is ISavable).ToList();

        }

        private void GenerateJsonString()
        {

        }

        public void DestroySaveFile()
        {

        }

        public void SetHighScore(string PlayerName, int highScore)
        {
            if (playerRealm == null) playerRealm = realm.Find<PlayerRealm>(PlayerName);
        }
    }
}


