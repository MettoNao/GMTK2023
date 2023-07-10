using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    player,
    ally,
    enemy,
    playerAlly,
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] SpriteRenderer spriteRenderer;
    private BulletType bulletType;
    private ClampScript clamp;
    private int attackPoint;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        clamp = new ClampScript();
    }

    public void Init(float angle, float speed, Vector2 pos, BulletType type, int _attackPoint)
    {
        gameObject.layer = 6 + (int)type;
        attackPoint = _attackPoint;
        spriteRenderer.color = getColor(type);
        transform.position = pos;
        // 弾の発射角度をベクトルに変換する
        var direction = GetDirection(angle);

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        rig.AddForce(direction * speed);
        bulletType = type;
    }

    private void Update()
    {
        if (clamp.isInScreen(transform.position))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isSameType(other.gameObject)) return;

        var r = other.gameObject.GetComponent<IReciveDamage>();
        if (r == null) return;

        r.ReciveDamage(attackPoint, bulletType);

        gameObject.SetActive(false);
    }

    private bool isSameType(GameObject other)
    {
        return bulletType == BulletType.player && other.CompareTag("Player") ||
        bulletType == BulletType.enemy && other.CompareTag("Enemy") ||
        bulletType == BulletType.playerAlly && other.CompareTag("Player") ||
        bulletType == BulletType.ally && other.CompareTag("Ally");
    }

    private Color getColor(BulletType type)
    {
        switch (type)
        {
            case BulletType.player:
                return Color.cyan;
            case BulletType.ally:
                return Color.green;
            case BulletType.enemy:
                return Color.red;
            case BulletType.playerAlly:
                return Color.yellow;
        }

        return Color.black;
    }

    Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }
}