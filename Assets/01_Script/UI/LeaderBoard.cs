using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using ui.template;
using savesystem.realm;
using Realms;
using System.Linq;
using System.Collections.Generic;

namespace ui.template
{
public class LeaderBoardElement : AbstractTemplateElement
{
   public new class UxmlFactory : UxmlFactory<LeaderBoardElement, LeaderBoardElement.UxmlTraits> { }
        private PlayerCrud playerCrud;
        Realm realm;
        VisualElement PlayerInformation;
        public LeaderBoardElement() { }

        public void Init()
        {
            realm = Realm.GetInstance();
            playerCrud = new PlayerCrud(realm);
            PlayerInformation = new VisualElement();
            PlayerInformation = this.Q<VisualElement>("PlayersInformation");

            playerCrud.RemovePlayer("1");
            playerCrud.RemovePlayer("2");

            IQueryable<PlayerRealm> allPlayer = playerCrud.GetAllPlayers();
            int i = 0;
            foreach (var player in allPlayer)
            {
                this.Q<Label>(i.ToString()).text = player.Name;
                i++;
                if(i == 3)
                {
                    break;
                }
            }

            foreach (var player in allPlayer)
            {
                //Creating block for displaying
                VisualElement playerLine= new VisualElement();
                playerLine.name = player.Name;
                playerLine.style.width = 500;
                playerLine.style.height = 50;
                playerLine.style.justifyContent = Justify.SpaceAround;
                playerLine.style.alignItems = Align.Center;
                playerLine.style.marginTop = 13;
                playerLine.style.backgroundColor = new Color(0.3882353f, 0.3882353f, 0.3882353f);
                playerLine.style.flexDirection = FlexDirection.Row;
                playerLine.style.color = Color.white;
                

                //setting conent player on labels
                Label name = new Label();
                Label highScore = new Label();
                name.text = player.Name;
                highScore.text = player.highScore.ToString();
               
                PlayerInformation.Add(playerLine);
                this.Q<VisualElement>(player.Name).Add(name);
                this.Q<VisualElement>(player.Name).Add(highScore);

            }
        }
    }
}