using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.SignIn
{
    public class SignInElement : VisualElement
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
            btn_cancel.clicked += () => onCancel.Invoke();
            btn_validate.clicked += () => onNewPlayerSignIn.Invoke(tf_name.text);
        }
    }
}

