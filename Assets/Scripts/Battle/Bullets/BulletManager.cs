using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace minigame.Battle.Bullets
{
    // Bullet管理クラス
    public class BulletManager : MonoBehaviour
    {
        private int _size = 0;
        GameObject _prefab = null;
        List<BulletEntity> _pool = null;
        private Transform _parent;

        // コンストラクタ
        public BulletManager(Transform parent, int size=0, string prefabName="bullet")
        {
            _parent = parent;
            _size = size;
            _prefab = Resources.Load(prefabName) as GameObject;
            if (_prefab == null) {
                Debug.LogError("NOT FOUND prefab." + prefabName);
            }

            _pool = new List<BulletEntity>();

            if (size > 0 ) {
                // size指定があれば固定アロケーション
                for (int i=0; i < size; i++) {
                    GameObject g = GameObject.Instantiate(_prefab, new Vector3(), Quaternion.identity) as GameObject;
                    BulletEntity bullet = g.GetComponent<BulletEntity>();
                    bullet.VanishCannotOverride();
                    _pool.Add(bullet);
                }
            }
        }

        // インスタンスを取得
        public BulletEntity Add(BulletType type, Vector3 pos, float direction=0.0f, float speed=0.0f)
        {
            foreach (BulletEntity bullet in _pool)
            {
                if (bullet.Exists == false)
                {
                    // 未使用のオブジェクトを見つけた
                    return _Recycle(bullet, type, pos, direction, speed);
                }
            }

            if (_size == 0)
            {
                // 自動で拡張
                GameObject g = GameObject.Instantiate(_prefab, new Vector3(), Quaternion.identity) as GameObject;
                BulletEntity bullet = g.GetComponent<BulletEntity>();
                _pool.Add(bullet);
                return _Recycle(bullet, type, pos, direction, speed);
            }
            return null;
        }

        BulletEntity _Recycle(BulletEntity bullet, BulletType type, Vector3 pos, float direction, float speed)
        {
            bullet.Revive();
            bullet.SetPosition(pos);
            bullet.SetVelocity(direction, speed);
            bullet.SetType(type);

            return bullet;
        }
    }
}
