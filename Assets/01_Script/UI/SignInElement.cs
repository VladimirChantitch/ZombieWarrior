using System;
using System.Collections;
using System.Collections.Generic;
using ui.template;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.SignIn
{
    public class SignInElement : AbstractTemplateElement
    {
        public new class UxmlFactory : UxmlFactory<SignInElement, SignInElement.UxmlTraits> { }

        public event Action<string> onNewPlayerSignIn;
        public event Action onCancel;

        TextField tf_name = null;
        Button btn_validate = null;
        Button btn_cancel = null;

        public void Init()
        {
            BindButtons();
        }

        private void BindButtons()
        {
            tf_name = this.Q<TextField>("tf_name");
            btn_validate = this.Q<Button>("btn_validate");
            btn_cancel = this.Q<Button>("btn_cancel");

            btn_cancel.clicked += () =>
            {
                onCancel?.Invoke();
                StartMenuAudio.Instance.PlayButtonAudio();
            };

            btn_validate.clicked += () =>
            {
                onNewPlayerSignIn?.Invoke(tf_name.text);
                StartMenuAudio.Instance.PlayButtonAudio();
            };
        }
    }
}

