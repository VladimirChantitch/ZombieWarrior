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
        public event Action onCancel;

        ScrollView scrollView = null;
        Button btn_cancel = null;

        public void Init(IQueryable<PlayerRealm> playerRealms)
        {
            scrollView = this.Q<ScrollView>();
            InstantiateNames(playerRealms);

            btn_cancel = this.Q<Button>("btn_cancel");
            btn_cancel.clicked += () =>
            {
                onCancel?.Invoke();
                StartMenuAudio.Instance.PlayButtonAudio();
            };
        }

        private void InstantiateNames(IQueryable<PlayerRealm> playerRealms)
        {
            foreach(PlayerRealm playerRealm in playerRealms)
            {
                VisualElement ve = ResourcesManager.Instance.GetTemplateFragment("connectionButton").CloneTree();
                ConnectionButtonElement connectionButtonElement = ve.Q<ConnectionButtonElement>();
                connectionButtonElement.Init(playerRealm);
                connectionButtonElement.onSelected += (player_name) =>
                {
                    onPlayerSelected?.Invoke(player_name);
                    StartMenuAudio.Instance.PlayButtonAudio();
                };

                scrollView.Add(ve);
            }
        }
    }
}

