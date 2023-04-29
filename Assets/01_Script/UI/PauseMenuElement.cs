using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace ui.template
{ 
    public class PauseMenuElement : AbstractTemplateElement
    {

        public new class UxmlFactory : UxmlFactory<PauseMenuElement, UxmlTraits> { }

        public PauseMenuElement()
        {
        }

        public event Action onGoBack;

        Button goBack = null;


        public void Init()
        {
            this.Q<VisualElement>("PauseMenuBlock").AddToClassList("opacity");
            BindButtons();
        }

        private void BindButtons()
        {
            goBack = this.Q<Button>("BackToGame");

            goBack.clicked += () =>
            {
                onGoBack?.Invoke();
                this.Q().AddToClassList("hide");
                ButtonAudio.Instance.PlayButtonAudio();
            };
        }
    }
}