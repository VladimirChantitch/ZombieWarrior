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

        public void Init(PlayerRealm playerRealm)
        {
            clicked += () => onSelected?.Invoke(playerName);
            this.playerName = playerRealm.Name;

            text = new StringBuilder(playerName).Append("                         ::::").Append(playerRealm.highScore).ToString();
        }
    }
}

