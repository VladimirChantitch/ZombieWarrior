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
        public static PlayerCrud Instance { get; private set; }

        public PlayerCrud(Realm realm)
        {
            Instance = this;
            this.realm = realm;
        }

        Realm realm;
        PlayerRealm currentPlayerRealm;

        public void SetHighScore(string PlayerName, int highScore)
        {
            if (currentPlayerRealm == null && currentPlayerRealm.Name != PlayerName)
            {
                realm.Write(() =>
                {
                    currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
                    currentPlayerRealm.highScore = highScore;
                });
            }
            else
            {
                currentPlayerRealm.highScore = highScore;
            }
        }

        public void IncreaseHighScore(string PlayerName, int amount)
        {
            if (currentPlayerRealm == null || currentPlayerRealm.Name != PlayerName)
            {
                realm.Write(() =>
                {
                    currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
                    currentPlayerRealm.highScore += amount;
                });
            }
            else
            {
                currentPlayerRealm.highScore += amount;
            }
        }

        public PlayerRealm GetPlayer(string PlayerName)
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
                //For avoiding null object
                currentPlayerRealm = GetPlayer(PlayerName);
                realm.Remove(currentPlayerRealm);
                currentPlayerRealm = null;
            });
        }

        public IQueryable<PlayerRealm> GetAllPlayers()
        {
            return realm.All<PlayerRealm>().OrderByDescending(x => x.highScore);
        }
        
    }
}

