using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using combat.bullet;

namespace UI.combat.bullet
{
    public class BulletContainerElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BulletContainerElement, VisualElement.UxmlTraits>
        {

        }

        public BulletContainerElement()
        {

        }

        List<VisualElement> bulletsLeft = new List<VisualElement>();
        StyleBackground currentSprite;
        StyleSheet styleSheet;
        E_BulletType currentBulletType;

        public void NotifyBulletChanged(BulletDto bulletDto)
        {
            styleSheet = bulletDto.styleSheet;
            if(bulletDto.bulletTypes != currentBulletType)
            {
                int bulletDelta = bulletsLeft.Count - bulletDto.amount;
                ChangeBulletType(bulletDto);
                AddBullets(bulletDelta);
            }
            else
            {
                AddBullets(bulletDto.amount);
            }
        }

        private void ChangeBulletType(BulletDto bulletContainerMessage)
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


