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

using camera;
using character.stat;
using combat;
using combat.weapon;
using inputs;
using Realms;
using savesystem;
using savesystem.dto;
using savesystem.realm;
using stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace player
{
    public class PlayerManager : MonoBehaviour, ISavable, ICharacters, IDamageable
    {
        [SerializeField] string _name;
        [SerializeField] string _description;
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }

        [SerializeField] InputManager inputManager;

        [SerializeField] StatComponent statComponent;
        [SerializeField] StatSystem statSystem;

        [SerializeField] AnimationHelper animator;
        [SerializeField] CrossAir crossAir;

        [Header("Camera")]
        [SerializeField] Camera camera;
        [SerializeField] CameraShake cameraShake;

        [Header("colliders")]
        [SerializeField] PlayerTakeDamageCollider playerTakeDamageCollider;

        [Header("Mouvement")]
        [SerializeField] PlayerMotionComponent playerMotionComponent;
        [SerializeField] TrailRenderer dashTrail;

        [Header("combat")]
        [SerializeField] WeaponManager  weaponManager;

        [SerializeField] bool canTakeDamage;


        Coroutine autoShootCorroutine;

        float currentFireRate;

        public event Action onPlayerDied;

        void Awake()
        {
            playerMotionComponent = GetComponent<PlayerMotionComponent>();
            if (crossAir == null)
            {
                crossAir = GetComponent<CrossAir>();
            }
            dashTrail.enabled = false;
            statComponent = new StatComponent(statSystem.stats);

            crossAir.Init(camera);
        }

        private void Start()
        {
            SubscriteToInputs();
            InitEvents();
            PlayerCrud.Instance.SetPlayerHealth(SeesionCookie.currentPlayerName, statComponent.GetStatValue(E_Stats.Life));
            PlayerCrud.Instance.SetPlayerMaxHealth(SeesionCookie.currentPlayerName, statComponent.GetMaxStatValue(E_Stats.Life));
        }

        public static Vector3 CURRENT_POSITION;

        private void FixedUpdate()
        {
            CURRENT_POSITION = transform.position;
        }

        private void SubscriteToInputs()
        {
            if (inputManager != null)
            {
                inputManager.onInteractPressed += () => OnInteract();

                inputManager.onMove += direction => MovePlayer(direction);
                inputManager.onDashAction += direction => Dash(direction);
                inputManager.onPrimaryAction += () => Shoot();
                inputManager.onRealeasePrimary += () => ToggleOnOffAutoShoot(false);
                inputManager.onHoldPrimary += () => ToggleOnOffAutoShoot(true);

                weaponManager.onFireRateChanged += (f) => currentFireRate = f;
            }
            else
            {
                Debug.Log($"<color=red> NO INPUT MANAGER IN PLAYER MANAGER</color>");
            }
        }

        private void InitEvents()
        {
            crossAir.CrossAirPositionChanged += t => { RotatePlayer(t); };
            playerTakeDamageCollider.onTakeDamage += data => { TakeDamage(data); };

            playerMotionComponent.onWalk += () => { animator.StartRunning(); };
            playerMotionComponent.onStop += () => { animator.StopRunnning(); };
            playerMotionComponent.onDashStart += () => { Handledash(true); };
            playerMotionComponent.onDashStop += () => { Handledash(false); };

        }

        private void OnInteract()
        {
            Debug.Log("Interact");
        }

        public void Save()
        {
            //TODO
        }

        public void Load(RealmObject realmObject)
        {
            if(realmObject is PlayerRealm playerRealm)
            {
                this.Name = playerRealm.Name;
            }
        }


        #region motion
        private void MovePlayer(Vector2 direction)
        {
            if (direction.x != 0 || direction.y != 0)
            {
                playerMotionComponent.moveCharacter(direction);
            }
            else
            {
                Stop();
            }
        }

        public void Dash(Vector2 direction)
        {
            playerMotionComponent.Dash(direction);
        }

        public void RotatePlayer(Transform transform)
        {
            playerMotionComponent.RotatePlayer(transform);
        }

        public void Stop()
        {
            playerMotionComponent.Stop();
        }
        #endregion
        #region Attack
        public void Shoot()
        {
            weaponManager.ShootBullet();
            cameraShake.ShakeCamera(1.25f, 0.2f, 1);
        }

        public void ToggleOnOffAutoShoot(bool isOn)
        {
            if (isOn)
            {
                autoShootCorroutine = StartCoroutine(AutoShoot());
            }
            else
            {
                if (autoShootCorroutine != null)
                {
                    StopCoroutine(autoShootCorroutine);
                }
            }
        }

        IEnumerator AutoShoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(currentFireRate);
                Shoot();
            }
        }
        #endregion

        /// <summary>
        /// Tooglles on the dash animation and the invulnerability frames
        /// </summary>
        /// <param name="isDashing"></param>
        private void Handledash(bool isDashing)
        {
            if (isDashing)
            {
                animator.SpecialMovement();
                playerTakeDamageCollider.CloseCollider();
                if (dashTrail != null)
                {
                    dashTrail.enabled = true;
                    canTakeDamage = false;
                }
            }
            else
            {
                animator.StopSpecialMotion();
                playerTakeDamageCollider.OpenCollider();
                if (dashTrail != null)
                {
                    dashTrail.enabled = false;
                    canTakeDamage = true;
                }
            }
        }

        public void TakeDamage(DamageData damageData)
        {
            if (canTakeDamage)
            {
                statComponent.AddOrRemoveStat(E_Stats.Life, damageData.DamageAmount);
                PlayerCrud.Instance.SetPlayerHealth(SeesionCookie.currentPlayerName, statComponent.GetStatValue(E_Stats.Life));
                cameraShake.ShakeCamera(2f, 0.1f, 1);
                Debug.Log($"<color=purple> The player took damage {statComponent.GetStatValue(E_Stats.Life)} </color>");
                if (statComponent.GetStatValue(E_Stats.Life) <= 0)
                {
                    onPlayerDied?.Invoke();
                }
            }
        }
    }
}

