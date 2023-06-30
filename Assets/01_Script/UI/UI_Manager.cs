/*   
    GameJam template for unity project
    Copyright (C) 2023  VladimirChantitch-MarmotteQuantique

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */


using game_manager;
using player;
using savesystem.realm;
using System;
using System.Collections;
using System.Collections.Generic;
using ui.template;
using UI.Connection;
using UI.SignIn;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace ui
{
    [RequireComponent(typeof(UIDocument))]
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField] UIDocument ui_Doc = null;
        VisualElement currentRoot = null;
        AbstractTemplateElement currentTemplate = null;
        [SerializeField] Camera camera;

        public event Action<GameScene> onStartGame;
        public event Action<GameScene> onBackToMain;
        public event Action onBackToGame;
        public event Action<GameState> onLeaderBoard;
        public event Action<string> onPlayerSignIn;
        public event Action<string> onPlayerConnection;
        public GameManager gameManager;

        public void Awake()
        {
            if (ui_Doc == null) GetComponent<UIDocument>();
            if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
        }

        public void ChangeUITemplate()
        {
            VisualTreeAsset visualTreeAsset = ResourcesManager.Instance.GetTemplate();
            if (visualTreeAsset != null)
            {
                ui_Doc.visualTreeAsset = visualTreeAsset;
                currentRoot = ui_Doc.rootVisualElement;
                RebindUI(ResourcesManager.Instance.GameState);
            }
        }

        private void RebindUI(GameState gameState)
        {
            currentTemplate = currentRoot.Q<AbstractTemplateElement>();
            if (currentTemplate != null)
            {
                switch (currentTemplate)
                {
                    case StartMenuElement startMenuElement:
                        InitStartMenu(startMenuElement);
                        break;
                    case WinMenuElement winMenuElement:
                        InitWinMenu(winMenuElement);
                        break;
                    case LooseMenuElement looseMenuElement:
                        InitLooseMenu(looseMenuElement);
                        break;
                    case LeaderBoardElement leaderBoardElement:
                        InitLeaderBoard(leaderBoardElement);
                        break;
                    case SignInElement signInElement:
                        InitSignMenu(signInElement);
                        break;
                    case ConnectionElement connectionElement:
                        InitConnectionMenu(connectionElement);
                        break;
                    case ATHElement aTHElement:
                        InitATHElement(aTHElement);
                        break;
                    case PauseMenuElement pauseMenuElement:
                        InitPauseMenuElement(pauseMenuElement);
                        break;
                }
            }
            else
            {
                Debug.Log($"<color=red> YOU GOT NO aBSTRACT TEMPLATE ELEMENT </color>" +
                    $"<color=green> please make one</color>");
            }
        }

        private void InitLooseMenu(LooseMenuElement looseMenuElement)
        {
            UnityEngine.Cursor.visible = true;
            looseMenuElement.Init();
            looseMenuElement.onBackToMainMenu += () => onBackToMain?.Invoke(GameScene.Start_scene);
        }

        private void InitWinMenu(WinMenuElement winMenuElement)
        {
            UnityEngine.Cursor.visible = true;
            winMenuElement.Init();
            winMenuElement.onBackToMainMenu += () => onBackToMain?.Invoke(GameScene.Start_scene);
        }

        private void InitStartMenu(StartMenuElement startMenuElement)
        {
            UnityEngine.Cursor.visible = true;
            startMenuElement.Init();

            startMenuElement.onStartButton += () =>
            {
                ResourcesManager.Instance.ChangeSubState(GameState.NewUserScreen);
                ChangeUITemplate();
            };

            startMenuElement.onLoadButton += () =>
            {
                ResourcesManager.Instance.ChangeSubState(GameState.ConnectionScreen);
                ChangeUITemplate();
            };
            startMenuElement.onCreditsButton += () =>
            {
                ChangeUITemplate();
            };
        }

        private void InitLeaderBoard(LeaderBoardElement leaderBoardElement)
        {
            UnityEngine.Cursor.visible = true;
            leaderBoardElement.Init();
            leaderBoardElement.onPlayAgain += () => onBackToMain?.Invoke(GameScene.Start_scene);
        
        }

        private void InitSignMenu(SignInElement signInElement)
        {
            UnityEngine.Cursor.visible = true;
            signInElement.Init();
            signInElement.onCancel += () => {
                ResourcesManager.Instance.ChangeSubState(GameState.None);
                ChangeUITemplate();
            };
            signInElement.onNewPlayerSignIn += (player_name) =>
            {
                onPlayerSignIn?.Invoke(player_name);
            };
        }

        private void InitConnectionMenu(ConnectionElement connectionElement)
        {
            UnityEngine.Cursor.visible = true;
            connectionElement.Init(PlayerCrud.Instance.GetAllPlayers());
            connectionElement.onCancel += () => {
                ResourcesManager.Instance.ChangeSubState(GameState.None);
                ChangeUITemplate();
            };
            connectionElement.onPlayerSelected += (playerName) => onPlayerConnection?.Invoke(playerName);
        }

        private void InitATHElement(ATHElement aTHElement)
        {
            UnityEngine.Cursor.visible = false;
            aTHElement.Init(PlayerCrud.Instance.GetPlayer(SeesionCookie.currentPlayerName));
        }

        private void InitPauseMenuElement(PauseMenuElement pauseMenuElement)
        {
            UnityEngine.Cursor.visible = true;
            pauseMenuElement.Init();
            pauseMenuElement.onResume += () =>
            {
                ResourcesManager.Instance.ChangeSubState(GameState.Playing);
                ChangeUITemplate();
                onBackToGame.Invoke();
            };

            pauseMenuElement.onMainMenu += () => onBackToMain?.Invoke(GameScene.Start_scene);

            pauseMenuElement.onQuit += () => Application.Quit();
        }
    }
}

