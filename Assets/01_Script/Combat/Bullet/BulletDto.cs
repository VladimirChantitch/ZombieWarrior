using savesystem.dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace combat.bullet
{
    public class BulletDto : Dto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Differencial of ammo</param>
        /// <param name="bulletTypes">type of bullets</param>
        public BulletDto(int amount, E_BulletType bulletTypes)
        {
            this.amount = amount;
            this.bulletTypes = bulletTypes;
        }

        public int amount { get; private set; }
        public E_BulletType bulletTypes { get; private set; }

        public Sprite bulletSprite { get; private set; }
        public void SetBulletImage(Sprite bulletSprite)
        {
            this.bulletSprite = bulletSprite;
        }

        public StyleSheet styleSheet { get; private set; }
        public void SetStyle(StyleSheet styleSheet)
        {
            this.styleSheet = styleSheet;
        }
    }
}
