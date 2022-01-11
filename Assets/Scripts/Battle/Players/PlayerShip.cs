using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

using minigame.Battle.Inputs;

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

        private Vector3 _position;

        protected void Start()
        {
            var inputEvent = GetComponent<IInputEventProvider>();
            // MovePosition が発行されたら Vector3 に convert する
            inputEvent.MovePosition.Subscribe(x => _position = x);

            this.FixedUpdateAsObservable()
                .Subscribe(_ => { 
                    var targetPos = Camera.main.ScreenToWorldPoint(_position);
                    targetPos = Move(targetPos);
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
    }
}
