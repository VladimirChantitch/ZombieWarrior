using Realms;
using savesystem.dto;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace savesystem.realm
{
    public class PlayerCrud
    {
        public PlayerCrud(Realm realm)
        {
            this.realm = realm;
        }

        Realm realm;
        PlayerRealm currentPlayerRealm;

        public void SetHighScore(string PlayerName, int highScore)
        {
            realm.Write(() =>
            {
                PlayerRealm playerRealm = realm.Find<PlayerRealm>(PlayerName);
                playerRealm.highScore = highScore;
            });
        }
        
        public PlayerRealm GetPlayerData(string PlayerName)
        {
            currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
            return currentPlayerRealm;
        }

        public void CreateNewPlayer(string PlayerName)
        {
            realm.Write(() =>
            {
                currentPlayerRealm = new PlayerRealm { Name = PlayerName };
                realm.Add(currentPlayerRealm);
            });
        }

        public void RemovePlayer(string PlayerName)
        {
            realm.Write(() =>
            {
                realm.Remove(currentPlayerRealm);
                currentPlayerRealm = null;
            });
        }

        public IQueryable<PlayerRealm> GetAllPlayers()
        {
            return realm.All<PlayerRealm>();
        }
    }
}

