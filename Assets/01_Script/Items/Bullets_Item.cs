using combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace items
{
    [CreateAssetMenu (menuName = "Item/Bullet")]
    public class Bullets_Item : Items
    {
        [SerializeField] GameObject bulletPrefab;

        public GameObject BulletPrefab { get => bulletPrefab; }
    }
}

