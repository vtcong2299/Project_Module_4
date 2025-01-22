using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DOtween

public class PlayerCtrl : Singleton<PlayerCtrl>, IOnGamePause, IOnGameRunning, ITransformGettable
{
    [SerializeField] Joystick joystick;
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerAnim playerAnim;
    public PlayerReceiveDame playerReceiveDame;
    [SerializeField] DataPlayer playerData;
    float vertical;
    float horizontal;
    [SerializeField] float moveSpeed;

    public Transform _transform => transform;

    bool gamePause;
    public Action onGamePauseAction => () => gamePause = true;
    public Action onGameRunningAction => () => gamePause = false;

    private void Update()
    {
        if (gamePause || playerAnim.IsSpawning() || playerAnim.IsAttacking())
        {
            return;
        }
        InputCtrl();
        PlayerMove();
        RotatePlayer();
    }

    void PlayerMove()
    {
        moveSpeed = playerData.moveSpeedMax;
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        rb.velocity = moveDirection * moveSpeed;
        if (rb.velocity != Vector3.zero)
        {
            playerAnim.SetRun(true);
        }
        else
        {
            playerAnim.SetRun(false);
        }
    }

    void RotatePlayer()
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        if (moveDirection.magnitude >= 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.DORotateQuaternion(targetRotation, 0.3f); 
        }
    }

    void InputCtrl()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
    }
}
