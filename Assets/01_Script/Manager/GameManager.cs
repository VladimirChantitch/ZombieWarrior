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
            BindUI_Events();
            BindPlayer_Events();
            BindSound_Events();

            ChangeTemplate();
        }

        #region EventBindings
        private void BindSound_Events()
        {

        }

        private void BindPlayer_Events()
        {

        }

        private void BindUI_Events()
        {
            if (uiManager != null)
            {
                uiManager.onStartGame += scene => LoadScene(scene);
                uiManager.onBackToMain += scene => LoadScene(scene);
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

        /// <summary>
        /// A method to call a save
        /// </summary>
        public void SaveGame()
        {
            saveManager.SaveGame();
        }

        /// <summary>
        /// A method to call a load
        /// </summary>
        public void LoadGame()
        {
            saveManager.LoadGame();
        }
        #endregion
    }
}


