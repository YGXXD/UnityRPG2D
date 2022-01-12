using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInput playerinput;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask Surface;
    private Rigidbody2D thisRigidbody;
    private Coroutine moveCtrl;
    private BoxCollider2D thisBox;
    private bool isOnSurface;
    private bool canDoubleJump;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        playerAttack = this.GetComponent<PlayerAttack>();
        thisBox = this.GetComponent<BoxCollider2D>();
        thisRigidbody = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerinput.EnableGameplayInput();
        AddInput();
        StartCoroutine("ActionCorouting");
    }
    private void OnDisable()
    {
        DeletInput();
        StopAllCoroutines();
    }
    private void AddInput()
    {
        playerinput.onMove += Move;
        playerinput.onMoveStop += Stop;
        playerinput.onJump += Jump;
        playerinput.onDash += playerAttack.Dash;
        playerinput.onAttack += playerAttack.Attack;
    }
    private void DeletInput()
    {
        playerinput.onMove -= Move;
        playerinput.onMoveStop -= Stop;
        playerinput.onJump -= Jump;
        playerinput.onDash -= playerAttack.Dash;
        playerinput.onAttack -= playerAttack.Attack;
    }
    private void Flip(float input)
    {
        if (input > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (input < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Move(float input)
    {
        if(moveCtrl != null)
            StopCoroutine(moveCtrl);
        moveCtrl = StartCoroutine(MoveCoroutine(input));
    }

    private void Stop()
    {
        if (moveCtrl != null)
            StopCoroutine(moveCtrl);
    }
    private IEnumerator MoveCoroutine(float input)
    {
        while (true)
        {
            while (this.gameObject.activeSelf && !playerAttack.IsDashing)
            {
                Flip(input);
                thisRigidbody.velocity = input * Vector2.right * speed + Vector2.up * thisRigidbody.velocity.y;
                yield return null;
            }
            yield return null;
        }
    }
    private void Jump()
    {
        if(isOnSurface && !playerAttack.IsDashing)
        {
            thisRigidbody.velocity = Vector2.up * jumpSpeed;
            canDoubleJump = true;
        }
        else if(canDoubleJump && !playerAttack.IsDashing)
        {
            thisRigidbody.velocity = Vector2.up * jumpSpeed;
            canDoubleJump = false;
        }      
    }
    private IEnumerator ActionCorouting()
    {
        while(this.gameObject.activeSelf)
        {
            isOnSurface = thisBox.IsTouchingLayers(Surface);
            PlayerAnimator.Instance.PlayerSet("IsIdle", thisRigidbody.velocity == Vector2.zero);
            PlayerAnimator.Instance.PlayerSet("IsToSurface", isOnSurface);
            PlayerAnimator.Instance.PlayerSet("IsJump", thisRigidbody.velocity.y > 0.0f && !isOnSurface);
            PlayerAnimator.Instance.PlayerSet("IsFall", thisRigidbody.velocity.y < 0.0f && !isOnSurface);
            PlayerAnimator.Instance.PlayerSet("IsDash", playerAttack.IsDashing);
            yield return null;
        }
    }
    
}