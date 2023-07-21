using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using KanKikuchi.AudioManager;

public class PlayerCore : MonoBehaviour, IReciveDamage
{
    [SerializeField] PlaterMover mover;
    [SerializeField] PlayerDirection direction;
    [SerializeField] PlayerShoot shoot;
    [SerializeField] HpUISetter hpUI;
    [SerializeField] PlayerHitEvent playerHitEvent;
    [SerializeField] PlayerSpecialShooter specialShooter;
    [SerializeField] GameObject body, effect;
    [SerializeField] GameManager gameManager;
    [SerializeField] MouseCursorScript mouseCursorScript;
    private Vector3 movement;

    PlayerInput playerInput;

    private InputAction FireAction, FireAction1, Look, PadLook;

    private int maxHp = 3;
    private int hp;
    private int attack = 1;

    private int skillPoint;

    private bool isDeath = true;
    public bool GetIsDeath { get { return isDeath; } set { isDeath = value; } }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        FireAction = playerInput.currentActionMap["Fire"];
        FireAction1 = playerInput.currentActionMap["Fire1"];
        Look = playerInput.currentActionMap["Look"];
        PadLook = playerInput.currentActionMap["PadLook"];

        hp = maxHp;
    }

    public async UniTask PlayerInit(CancellationToken token)
    {
        isDeath = true;
        transform.rotation = Quaternion.identity;
        effect.gameObject.SetActive(true);
        SEManager.Instance.Play(SEPath.RESULT);
        await UniTask.Delay(1000, cancellationToken: token);
        SEManager.Instance.Play(SEPath.MISSION);
        body.gameObject.SetActive(true);
        isDeath = false;
    }

    float timer = 0;
    float interval = 0.5f;
    bool pad;
    Vector2 oldMousePos;
    private void Update()
    {
        if (isDeath == true) return;

        if (FireAction.IsPressed())
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CameraShaker.Instance.ShakeCamera(0.1f, 20, 0.1f);
                shoot.Shoot();
                timer = interval;
            }
        }

        if (FireAction1.WasPressedThisFrame())
        {
            specialShooter.Shoot();
        }

        Vector2 mousePos = Look.ReadValue<Vector2>();
        Vector2 padDir = PadLook.ReadValue<Vector2>();

        if (oldMousePos != mousePos || (oldMousePos == mousePos && padDir.magnitude != 0))
        {
            direction.Direction(oldMousePos != mousePos ? mousePos : padDir, oldMousePos == mousePos);

            var corsorPos = oldMousePos == mousePos ? (Vector2)Camera.main.WorldToScreenPoint((Vector2)transform.position + padDir.normalized * 5) : mousePos;
            mouseCursorScript.setMouseCursorPosition(corsorPos);
        }

        oldMousePos = mousePos;
    }



    private void FixedUpdate()
    {
        if (isDeath == true) return;

        Vector2 movementVector = playerInput.currentActionMap["Move"].ReadValue<Vector2>();
        movement = new Vector2(movementVector.x, movementVector.y);

        mover.Move(movement);
    }

    public void ReciveDamage(int attackPoint, BulletType type)
    {
        if (type == BulletType.player) return;

        hp -= attackPoint;
        playerHitEvent.HitEvent(hp <= 0);

        if (hp <= 0)
        {
            isDeath = true;
            body.SetActive(false);
            gameManager.GameOver();
            return;
        }

        hpUI.SetHpFill((float)hp, (float)maxHp);
    }
}