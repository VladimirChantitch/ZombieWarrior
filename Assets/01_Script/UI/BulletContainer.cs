using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using messages;

namespace UI
{
    public class BulletContainer : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BulletContainer, VisualElement.UxmlTraits>
        {

        }

        public BulletContainer()
        {

        }

        List<VisualElement> bulletsLeft = new List<VisualElement>();
        BulletTypes currentBulletType = BulletTypes.normal;
        StyleBackground currentSprite;
        StyleSheet styleSheet;

        public void ReceiveMessage(BulletContainerMessage bulletContainerMessage)
        {
            styleSheet = bulletContainerMessage.styleSheet;
            if(bulletContainerMessage.bulletTypes != currentBulletType)
            {
                int bulletDelta = bulletsLeft.Count - bulletContainerMessage.amount;
                ChangeBulletType(bulletContainerMessage);
                AddBullets(bulletDelta);
            }
            else
            {
                AddBullets(bulletContainerMessage.amount);
            }
        }

        private void ChangeBulletType(BulletContainerMessage bulletContainerMessage)
        {
            currentBulletType = bulletContainerMessage.bulletTypes;
            bulletsLeft.ForEach(b =>
            {
                Background back = new Background();
                back.sprite = bulletContainerMessage.bulletSprite;
                currentSprite = new StyleBackground { value = back };
                b.style.backgroundImage = currentSprite;
            });
        }

        private void AddBullets(int amount)
        {
            if (amount < 0)
            {
                int startPos = (bulletsLeft.Count - 1) + amount;
                bulletsLeft.RemoveRange(startPos, -amount);
                for (int i = startPos; i < bulletsLeft.Count; i++)
                {
                    RemoveAt(i);
                }
            }
            else if (amount > 0)
            {
                for(int i = 0; i < amount; i++)
                {
                    VisualElement VE = new VisualElement();
                    VE.styleSheets.Add(styleSheet);
                    VE.AddToClassList("Ammo");
                    VE.style.backgroundImage = currentSprite;
                    bulletsLeft.Add(VE);
                    Add(VE);
                }
            }
        }
    }
}

namespace messages
{
    public class BulletContainerMessage : GameToUIMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Differencial of ammo</param>
        /// <param name="bulletTypes">type of bullets</param>
        public BulletContainerMessage(int amount, BulletTypes bulletTypes)
        {
            this.amount = amount;
            this.bulletTypes = bulletTypes;
        }

        public int amount { get; private set; }
        public BulletTypes bulletTypes { get; private set; }
        public Sprite bulletSprite { get; private set; }

        public void AddBulletImage(Sprite bulletSprite)
        {
            this.bulletSprite = bulletSprite;
        }
    }

    [Serializable]
    public class BulletStyles
    {
        public BulletTypes bulletTypes;
        public Sprite sprite;
    }

    public enum BulletTypes
    {
        normal, special
    }
}


