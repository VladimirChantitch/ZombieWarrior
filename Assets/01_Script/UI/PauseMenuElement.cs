using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace ui.template
{ 
    public class PauseMenuElement : AbstractTemplateElement
    {

        public new class UxmlFactory : UxmlFactory<PauseMenuElement, UxmlTraits> { }

        public PauseMenuElement()
        {
        }

        public event Action onResume;
        public event Action onMainMenu;
        public event Action onSave;
        public event Action onQuit;


        Button btn_resume = null;
        Button btn_mainMenu = null;
        Button btn_save = null;
        Button btn_quit = null;

        public void Init()
        {
            BindButtons();
        }

        private void BindButtons()
        {
            btn_resume = this.Q<Button>("btn_resume");
            btn_mainMenu = this.Q<Button>("btn_mainMenu");
            btn_save = this.Q<Button>("btn_save");
            btn_quit = this.Q<Button>("btn_quit");

            btn_resume.clicked += () =>
            {
                ButtonAudio.Instance.PlayButtonAudio();
                onResume?.Invoke();
            };

            btn_mainMenu.clicked += () =>
            {
                ButtonAudio.Instance.PlayButtonAudio();
                onMainMenu?.Invoke();
            };
            
            btn_save.clicked += () =>
            {
                ButtonAudio.Instance.PlayButtonAudio();
                onSave?.Invoke();
            };

            btn_quit.clicked += () =>
            {
                ButtonAudio.Instance.PlayButtonAudio(); ;
                onQuit?.Invoke();
            };
        }
    }
}