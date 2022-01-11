using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Token管理
public class TokenMgr<T> where T : Token {
    int _size = 0;
    GameObject _prefab = null;
    List<T> _pool = null;
    // Order in Layer
    int _order = 0;
    // 共通処理function型
    public delegate void FuncT(T t);

    // プレハブは"Resources/Prefabs/"に配置
    public TokenMgr(string prefabName, int size = 0) {
        _size = size;
        _prefab = Resources.Load("Prefabs/"+prefabName) as GameObject;
        if (_prefab == null) {
            Debug.LogError("Not Found prefab: " + prefabName);
        }

        _pool = new List<T>();
        if (size > 0) {
            for (int i=0; i<size; i++) {
                GameObject g = GameObject.Instantiate(_prefab, new Vector2(), Quaternion.identity) as GameObject;
                T obj = g.GetComponent<T>();
                obj.VanishCannotOverride();
                _pool.Add(obj);
            }
        }
    }

    // オブジェクトを再利用
    T _Recycle(T obj, float x, float y, float direction, float speed) {
        // 復活
        obj.Revive();
        obj.SetPosition(x, y);
        // if (obj.RigidBody != null) {
        // obj.SetVelocity(direction, speed);
        // }
        return obj;
    }
}