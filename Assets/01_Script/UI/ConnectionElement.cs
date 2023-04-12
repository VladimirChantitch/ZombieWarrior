using savesystem.realm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ui.template;
using UI.SignIn;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Connection
{
    public class ConnectionElement : AbstractTemplateElement
    {
        public new class UxmlFactory : UxmlFactory<ConnectionElement, ConnectionElement.UxmlTraits> { }

        public event Action<string> onPlayerSelected;

        ScrollView scrollView = null;

        public void Init(IQueryable<PlayerRealm> playerRealms)
        {
            scrollView = this.Q<ScrollView>();
            InstantiateNames(playerRealms);
        }

        private void InstantiateNames(IQueryable<PlayerRealm> playerRealms)
        {
            foreach(PlayerRealm playerRealm in playerRealms)
            {
                ConnectionButtonElement connectionButtonElement = new ConnectionButtonElement();
                connectionButtonElement.Init(playerRealm);
                connectionButtonElement.onSelected += (player_name) => onPlayerSelected?.Invoke(player_name); 

                scrollView.Add(connectionButtonElement);
            }
        }
    }
}

