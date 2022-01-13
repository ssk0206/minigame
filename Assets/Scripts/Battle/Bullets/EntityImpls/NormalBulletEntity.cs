using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using minigame.Battle.Bullets;

namespace minigame.Battle.Bullets.EntityImpl
{
    public class NormalBulletEntity : BulletEntity
    {
        public BulletType Type => BulletType.Normal;

        public ReactiveProperty<bool> IsExplosion = new ReactiveProperty<bool>();
        
        [SerializeField]
        public GameObject bulletPrefab;
        
        [SerializeField]
        public float speed;

        void Start()
        {

        }
    }
}
