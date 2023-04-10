/*   
    GameJam template for unity project
    Copyright (C) 2023  VladimirChantitch-MarmotteQuantique

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace inputs
{
    public class InputManager : MonoBehaviour
    {
        Inputs inputs = null;

        /// <summary>
        /// An event triggered when the interact input is pressed
        /// NB : You can also use a unity event if you want to 
        ///     add subscribers directly in the scene, but keep in
        ///     mind that unity event are 100 times slower.
        /// </summary>
        public event Action onInteractPressed;

        public event Action onPrimaryAction;
        public event Action onScondaryAction;
        public event Action onForwardAction;
        public event Action onBackwardAction;
        public event Action onInteractionAction;
        public event Action onReloadAction;
        public event Action onInventoryAction;
        public event Action onSkillTreeAction;
        public event Action onMapAction;
        public event Action onUseItemAction;
        public event Action onHoldPrimary;
        public event Action onRealeasePrimary;

        public event Action<Vector2> onDashAction;
        public event Action<Vector2> onMove;


        Vector2 direction;

        private void Awake()
        {
            inputs = new Inputs();
        }

        private void OnEnable()
        {
            inputs.Enable();

            BindInputs();
        }

        private void OnDisable()
        {
            inputs?.Disable();
        }

        private void BindInputs()
        {
            inputs.PlayerMouvement.Mouvement.performed += i => direction = i.ReadValue<Vector2>();

            inputs.MouseActions.PrimaryButton.performed += i => onPrimaryAction?.Invoke();
            inputs.MouseActions.PrimaryHold.performed += i => onHoldPrimary?.Invoke();
            inputs.MouseActions.PrimaryHold.canceled += i => onRealeasePrimary?.Invoke();
            inputs.MouseActions.SecondaryButton.performed += i => onScondaryAction?.Invoke();
            inputs.MouseActions.MouseWheel.performed += i =>
            {
                if (i.ReadValue<float>() > 0)
                {
                    onForwardAction?.Invoke();
                }
                else if (i.ReadValue<float>() < 0)
                {
                    onBackwardAction?.Invoke();
                }
            };

            inputs.KeyboardActions.InteractionAction.performed += i => onInteractionAction?.Invoke();
            inputs.KeyboardActions.RelaodAction.performed += i => onReloadAction?.Invoke();
            inputs.KeyboardActions.InventoryAction.performed += i => onInventoryAction?.Invoke();
            inputs.KeyboardActions.SkillTreeAction.performed += i => onSkillTreeAction?.Invoke();
            inputs.KeyboardActions.MapAction.performed += i => onMapAction?.Invoke();
            inputs.KeyboardActions.UseItemAction.performed += i => onUseItemAction?.Invoke();
            inputs.KeyboardActions.DashAction.performed += i => onDashAction?.Invoke(direction);        
        }

        void FixedUpdate()
        {
            onMove?.Invoke(direction);
        }
    }
}

