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
using inputs;
using savesystem;
using savesystem.dto;
using stats;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] MouvementService mouvementService;
        [SerializeField] TrailRenderer dashTrail;

        [Header("combat")]
        [SerializeField] WeaponManager weaponManager;

        [SerializeField] bool canTakeDamage;


        private void Start()
        {
            SubscriteToInputs();
        }

        private void SubscriteToInputs()
        {
            if (inputManager != null)
            {
                inputManager.onInteractPressed += () => OnInteract();
            }
            else
            {
                Debug.Log($"<color=red> NO INPUT MANAGER IN PLAYER MANAGER</color>");
            }
        }

        private void OnInteract()
        {
            Debug.Log("Interact");
        }

        public Dto Save()
        {
            return new PlayerDto() { Name = Name };
        }

        public void Load(Dto dto)
        {
            if (dto is PlayerDto playerDto)
            {
                Name = playerDto.Name;
            }
        }



        Coroutine autoShootCorroutine;

        float currentFireRate;

        void Awake()
        {
            mouvementService = GetComponent<MouvementService>();
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
            InitEvents();
        }

        private void InitEvents()
        {
            crossAir.CrossAirPositionChanged.AddListener(t => { RotatePlayer(t); });
            playerTakeDamageCollider.onTakeDamage.AddListener(data => { TakeDamage(data); });

            mouvementService.onWalk += () => { animator.StartRunning(); };
            mouvementService.onStop += () => { animator.StopRunnning(); };
            mouvementService.onDashStart += () => { Handledash(true); };
            mouvementService.onDashStop += () => { Handledash(false); };

            inputManager.onMove += direction => MovePlayer(direction);
            inputManager.onDashAction += direction => Dash(direction);
            inputManager.onPrimaryAction += () => Shoot();
            inputManager.onRealeasePrimary += () => ToggleOnOffAutoShoot(false);
            inputManager.onHoldPrimary += () => ToggleOnOffAutoShoot(true);

            weaponManager.onFireRateChanged += (f) => currentFireRate = f;

        }

        #region motion
        private void MovePlayer(Vector2 direction)
        {
            if (direction.x != 0 || direction.y != 0)
            {
                mouvementService.moveCharacter(direction);
            }
            else
            {
                Stop();
            }
        }

        public void Dash(Vector2 direction)
        {
            mouvementService.Dash(direction);
        }

        public void RotatePlayer(Transform transform)
        {
            mouvementService.RotatePlayer(transform);
        }

        public void Stop()
        {
            mouvementService.Stop();
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
                cameraShake.ShakeCamera(2f, 0.1f, 1);
                Debug.Log($"<color=purple> The player took damage {statComponent.GetStatValue(E_Stats.Life)} </color>");
            }
        }
    }
}

