using sound;
using ui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;
using UnityEngine.SceneManagement;
using scene;
using savesystem;
using savesystem.realm;

namespace game_manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] UI_Manager uiManager = null;
        [SerializeField] SoundManager soundManager = null;
        [SerializeField] PlayerManager playerManager = null;
        [SerializeField] SceneHandler sceneHandler = null;
        [SerializeField] SaveManager saveManager = null;

        GameScene sceneState;
        GameState gameState;

        private void Awake()
        {
            if (uiManager == null) uiManager = GetComponentInChildren<UI_Manager>();
            if (soundManager == null) soundManager = GetComponentInChildren<SoundManager>();
            if (playerManager == null) playerManager = FindObjectOfType<PlayerManager>();
            if (sceneHandler == null) sceneHandler = GetComponentInChildren<SceneHandler>();
            if(saveManager == null) saveManager = GetComponentInChildren<SaveManager>();

            sceneState = ResourcesManager.Instance.SceneState;
            gameState = ResourcesManager.Instance.GameState;
        }

        private void Start()
        {
            HandleCookieData();

            BindUI_Events();
            BindSound_Events();

            ChangeTemplate();
        }

        private void HandleCookieData()
        {
            switch (sceneState)
            {
                case GameScene.None_scene:
                    break;
                case GameScene.Start_scene:
                    break;
                case GameScene.Main_scene:
                    playerManager.Name = SeesionCookie.currentPlayerName;
                    BindPlayer_Events();
                    break;
                case GameScene.End_Scene:
                    break;
            }
        }

        #region EventBindings
        private void BindSound_Events()
        {

        }

        private void BindPlayer_Events()
        {
            playerManager.onPlayerDied += () =>
            {
                ResourcesManager.Instance.ChangeSubState(GameState.Loose);
                uiManager.ChangeUITemplate();
            };
        }

        private void BindUI_Events()
        {
            if (uiManager != null)
            {
                uiManager.onStartGame += scene => LoadScene(scene);
                uiManager.onBackToMain += scene => LoadScene(scene);
                uiManager.onPlayerSignIn += player_name =>
                {
                    PlayerCrud.Instance.CreateNewPlayer(player_name);
                    SeesionCookie.currentPlayerName = player_name;
                    LoadScene(GameScene.Main_scene);
                };
                uiManager.onPlayerConnection += (playerName) =>
                {
                    SeesionCookie.currentPlayerName = playerName;
                    LoadScene(GameScene.Main_scene);
                };
            }
        }
        #endregion

        #region Redistribution Methods
        private void LoadScene(GameScene gameScene)
        {
            sceneHandler.LoadScene(gameScene);
        }

        private void ChangeTemplate()
        {
            uiManager.ChangeUITemplate();
        }
        #endregion
    }
}


