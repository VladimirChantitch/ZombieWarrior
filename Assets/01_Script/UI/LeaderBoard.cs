using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using ui.template;

namespace ui.template
{
public class LeaderBoardElement : AbstractTemplateElement
{
   public new class UxmlFactory : UxmlFactory<LeaderBoardElement, LeaderBoardElement.UxmlTraits> { }
        private VisualElement medal;
        public LeaderBoardElement() { }

        public void Init()
        {
            var medalImage = (Texture2D) Resources.Load("medal.png");
            medal.style.backgroundImage= medalImage;
            this.Add( medal);
            Debug.Log("info");
        }
    }
}