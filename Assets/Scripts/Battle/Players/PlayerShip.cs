using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

using minigame.Battle.Inputs;
using minigame.Battle.Bullets;

namespace minigame.Battle.Players
{
    public class PlayerShip : MonoBehaviour
    {
        // X, Y 座標の移動可能範囲
        [System.Serializable]
        public class Bounds
        {
            public float xMin, xMax, yMin, yMax;
        }
        [SerializeField] Bounds bounds;

        // 補完の強さ 0f ~ 1f
        [SerializeField, Range(0, 1)] 
        private float followStrength;

        private Vector3 _localPos = new Vector3(0, 0, 0);

        private BulletManager _bulletManager;
        private void Awake() {
            _bulletManager =  new BulletManager(transform, 0);
        }

        protected void Start()
        {

            var inputEvent = GetComponent<IInputEventProvider>();

            Observable
                .Interval(TimeSpan.FromMilliseconds(100))
                .Where(_ => inputEvent.OnClicked.Value)
                .Subscribe(_ => Shot());

            this.FixedUpdateAsObservable()
                .Subscribe(_ => { 
                    var targetPos = Camera.main.ScreenToWorldPoint(inputEvent.MovePosition.Value);
                    targetPos = Move(targetPos);
                    transform.Rotate(0, 0, -2);
                    transform.position = Vector3.Lerp(transform.position, targetPos, followStrength);
                });
        }

        private Vector3 Move(Vector3 targetPos)
        {
            // X, Y, Z 座標の範囲制限
            targetPos.x = Mathf.Clamp(targetPos.x, bounds.xMin, bounds.xMax);
            targetPos.y = Mathf.Clamp(targetPos.y, bounds.yMin, bounds.yMax);
            targetPos.z = 0f;

            return targetPos;
        }

        private void Shot() 
        {
            var bulletType = BulletType.Normal;

            _localPos.y = 0.15f;
            _bulletManager.Add(bulletType, transform.TransformPoint(_localPos), transform.localEulerAngles.z+90, 3);
            _localPos.y = -0.15f;
            _bulletManager.Add(bulletType, transform.TransformPoint(_localPos), transform.localEulerAngles.z-90, 3);
        }
    }
}
