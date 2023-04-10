using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using messages;
using System.Threading.Tasks;

namespace UI
{
    public class UIManager : AbstractManager
    {
        [Header("style sheet")]
        [SerializeField] private StyleSheet styleSheet;

        [Header("Sprites refs")]
        [SerializeField] private List<BulletStyles> bulletStyles = new List<BulletStyles>();

        [Header("Monobehaviours")]
        [SerializeField] private UIDocument document = null;

        private VisualElement root = null;
        public BulletContainer bulletContainer { get; private set; }
        public ProgressBar healthBar {get; private set;}
        public ProgressBar armorBar { get; private set; }
        public Label moneyAmount { get; private set; }
        public void Awake()
        {
            if (document == null)
            {
                document = GetComponent<UIDocument>();
            }

            root = document.rootVisualElement;
            bulletContainer = root.Q<BulletContainer>("BulletContainer");
            healthBar = root.Q<ProgressBar>("LifeBar");
            armorBar = root.Q<ProgressBar>("ArmorBar");
            moneyAmount = root.Q<Label>("MoneyAmount");
        }

        public void HandleMessage(GameToUIMessage gameToUIMessage)
        {
            gameToUIMessage.SetStyle(styleSheet);
            switch (gameToUIMessage)
            {
                case BulletContainerMessage bulletContainerMessage:
                    try
                    {
                        bulletContainerMessage.AddBulletImage(bulletStyles.Where(ss => ss.bulletTypes == bulletContainerMessage.bulletTypes).First().sprite);
                    }
                    catch(Exception e){ Debug.Log("<color=yellow> please check your sprites in ThemeStyleSheet UIMAnager </color>"); }

                    bulletContainer.ReceiveMessage(bulletContainerMessage);
                    break;
                case ProgressBarMessage progressBarMessage:
                    HandleProgressBarMessage(progressBarMessage);
                    break;
                case MoneyAmountMessage moneyAmountMessage:
                    moneyAmount.text = moneyAmountMessage.amount.ToString();
                    break;
            }
        }

        public override void SendAMessage(Message message)
        {
            GameManager.Instance.SendAMessage(message);
        }

        private void HandleProgressBarMessage(ProgressBarMessage progressBarMessage)
        {
            switch (progressBarMessage.barType)
            {
                case BarType.Life:
                    healthBar.value = progressBarMessage.value;
                    healthBar.highValue = progressBarMessage.maxValue;
                    break;
                case BarType.Armor:
                    armorBar.value = progressBarMessage.value;
                    armorBar.highValue = progressBarMessage.maxValue;
                    break;
            }
        }
    }
}

namespace messages 
{
    /// <summary>
    /// Message to set up a progress bar
    /// </summary>
    public class ProgressBarMessage : GameToUIMessage
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="value">the current amount between 0 and 100</param>
        /// <param name="barType"></param>
        public ProgressBarMessage(float value, float maxValue, BarType barType)
        {
            this.value = value;
            this.maxValue = maxValue;
            this.barType = barType;
        }

        public float value { get; private set; }
        public float maxValue { get; private set; }
        public BarType barType { get; private set; }
    }

    /// <summary>
    /// A message to transferts an amount of money to the UI 
    /// </summary>
    public class MoneyAmountMessage : GameToUIMessage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Current amount of money to display</param>
        public MoneyAmountMessage(int amount)
        {
            this.amount = amount;
        }

        public int amount { get; private set; }
    }

    public enum BarType { Life, Armor}
}
