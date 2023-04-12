using savesystem.realm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Connection
{
    public class ConnectionButtonElement : Button
    {
        public new class UxmlFactory : UxmlFactory<ConnectionButtonElement, ConnectionButtonElement.UxmlTraits> { }

        public event Action<string> onSelected;

        string playerName;

        Label l_playerName;
        Label l_highScore;

        public void Init(PlayerRealm playerRealm)
        {
            l_highScore = this.Q<Label>("l_highScore");
            l_playerName = this.Q<Label>("l_playerName");

            clicked += () => onSelected?.Invoke(playerName);
            this.playerName = playerRealm.Name;

            l_playerName.text = playerName;
            l_highScore.text = playerRealm.highScore.ToString();
        }
    }
}

