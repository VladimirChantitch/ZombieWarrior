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
            if (currentPlayerRealm == null || currentPlayerRealm.Name != PlayerName)
            {
                realm.Write(() =>
                {
                    currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
                    currentPlayerRealm.highScore = highScore;
                });
            }
            else
            {
                realm.Write(() =>
                {
                    currentPlayerRealm.highScore = highScore;
                });
            }
        }

        public void SetPlayerHealth(string PlayerName, float health)
        {
           
            if (currentPlayerRealm == null || currentPlayerRealm.Name != PlayerName)
            {
                realm.Write(() =>
                {
                    currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
                    currentPlayerRealm.health = health;
                });
            }
            else
            {
                realm.Write(() =>
                {
                    currentPlayerRealm.health = health;
                });
            }
        }

        public void SetPlayerMaxHealth(string PlayerName, float maxHealth)
        {
            if (currentPlayerRealm == null || currentPlayerRealm.Name != PlayerName)
            {
                realm.Write(() =>
                {
                    currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
                    currentPlayerRealm.maxHealth = maxHealth;
                });
            }
            else
            {
                realm.Write(() =>
                {
                    currentPlayerRealm.maxHealth = maxHealth;
                });
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
                realm.Write(() =>
                {
                    currentPlayerRealm.highScore += amount;
                });
            }
        }

        public PlayerRealm GetPlayer(string PlayerName)
        {
            currentPlayerRealm = realm.Find<PlayerRealm>(PlayerName);
            return currentPlayerRealm;
        }

        public void CreateNewPlayer(string PlayerName)
        {
            PlayerRealm playerRealm = GetPlayer(PlayerName);
            if (playerRealm == null)
            {
                realm.Write(() =>
                {
                    currentPlayerRealm = new PlayerRealm { Name = PlayerName };
                    realm.Add(currentPlayerRealm);
                });
            }
            
        }

        public void RemovePlayer(string PlayerName)
        {
            realm.Write(() =>
            {
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

