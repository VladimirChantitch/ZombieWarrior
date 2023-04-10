using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace combat.weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] List<Transform> shootTransforms = new List<Transform>();

        public List<Transform> ShootTransforms => shootTransforms;

        [SerializeField] Transform rightIk;
        [SerializeField] Transform leftIk;

        public Transform RightIk => rightIk;
        public Transform LeftIk => leftIk;
    }
}
