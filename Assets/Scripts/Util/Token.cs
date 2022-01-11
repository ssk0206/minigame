using UnityEngine;
using System.Collections;

public class Token : MonoBehaviour {
    bool _exist = true;
    public bool Exists {
        get { return _exist; }
        set { _exist = value; }
    }

    // レンダラ
    SpriteRenderer _renderer = null;
    public SpriteRenderer Renderer {
        get { return _renderer ?? (_renderer = gameObject.GetComponent<SpriteRenderer>()); }
    }

    // 描画フラグ
    public bool Visible {
        get { return Renderer.enabled; }
        set { Renderer.enabled = value; }
    }

    public float X {
        set {
            Vector2 pos = transform.position;
            pos.x  = value;
            transform.position = pos;
        }
        get { return transform.position.x; }
    }

    public float Y {
        set {
            Vector2 pos = transform.position;
            pos.y  = value;
            transform.position = pos;
        }
        get { return transform.position.y; }
    }

    public void AddPosition(float dx, float dy) 
    {
        X += dx;
        Y += dy;
    }

    public void SetPosition(float x, float y)
    {
        Vector2 pos = transform.position;
        pos.Set(x, y);
        transform.position = pos;
    }

    public virtual void Revive()
    {
        gameObject.SetActive(true);
        Exists = true;
        Visible = true;
    }

    // 消滅（オーバーライド禁止）
    public void VanishCannotOverride()
    {
        gameObject.SetActive(false);
    }
}