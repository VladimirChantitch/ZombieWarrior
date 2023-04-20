
using UnityEngine;
using UnityEngine.UIElements;
using ui.template;
using savesystem.realm;
using Realms;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ui.template
{
public class LeaderBoardElement : AbstractTemplateElement
{
   public new class UxmlFactory : UxmlFactory<LeaderBoardElement, LeaderBoardElement.UxmlTraits> { }
        VisualElement PlayerInformation;
        public LeaderBoardElement() { }

        public event Action onPlayAgain;

        Button btn_try_again;

        public void Init()
        {
            PlayerInformation = new VisualElement();
            PlayerInformation = this.Q<VisualElement>("PlayersInformation");

            btn_try_again = this.Q<Button>("btn_try_again");
            btn_try_again.clicked += () =>
            {
                onPlayAgain?.Invoke();
                ButtonAudio.Instance.PlayButtonAudio();
            };
            IQueryable<PlayerRealm> allPlayer = PlayerCrud.Instance.GetAllPlayers();
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