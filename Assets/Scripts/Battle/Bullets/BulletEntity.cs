using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace minigame.Battle.Bullets
{
    public abstract class BulletEntity : MonoBehaviour
    {
        public BulletType Type { get; set; }

        int _power;
        public int Power
        {
            get { return _power; }
        }

        public void Init(int power)
        {
            _power = power;
        }

        bool _exist = true;
        public bool Exists {
            get { return _exist; }
            set { _exist = value; }
        }

        public virtual void Revive ()
        {
            gameObject.SetActive(true);
            Exists = true;
        }

        public virtual void Vanish()
        {
            VanishCannotOverride();
        }

        public void VanishCannotOverride() 
        {
            gameObject.SetActive(false);
            Exists = false;
        }

        public void SetType(BulletType type)
        {
            Type = type;
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        Rigidbody2D _rigidbody2D = null;
        public  Rigidbody2D RigidBody {
            get { return _rigidbody2D ?? (_rigidbody2D = gameObject.GetComponent<Rigidbody2D>()); }
        }

        public void SetVelocity (float direction, float speed)
        {
            Vector2 v;
            v.x = CosEx(direction) * speed;
            v.y = SinEx(direction) * speed;
            RigidBody.velocity = v;
        }

        public static float CosEx(float Deg) {
            return Mathf.Cos(Mathf.Deg2Rad * Deg);
        }

        public static float SinEx(float Deg) {
            return Mathf.Sin(Mathf.Deg2Rad * Deg);
        }
    }
}
