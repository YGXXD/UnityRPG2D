using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject,PlayerInputAction.IGamePlayActions
{
    public static int SkillID = 2;//技能编号
    /// <summary>
    /// 继承了ScriptableObject，可以在项目中直接创建实例，继承了输入接口，可以直接创建输入实例
    /// </summary>
    public event UnityAction<float> onMove = delegate { };
    public event UnityAction onMoveStop = delegate { };
    public event UnityAction onJump = delegate { };
    public event UnityAction<int> onAttack = delegate { };
    public event UnityAction onDash = delegate { };
    public event UnityAction onOpenSkillMenu = delegate { };
    public event UnityAction onCloseSkillMenu = delegate { };
    private PlayerInputAction playinputaction;
    /// <summary>
    /// 脚本启用时调用
    /// </summary>
    private void OnEnable()
    {
        playinputaction = new PlayerInputAction();
        playinputaction.GamePlay.SetCallbacks(this);
    }
    /// <summary>
    /// 脚本禁用时调用
    /// </summary>
    private void OnDisable()
    {
        playinputaction.GamePlay.Disable();
    }
    public void EnableGameplayInput()
    {
        playinputaction.GamePlay.Enable();//GamePlay动作表被启用
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;//鼠标不可见，鼠标锁定
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PlayerAnimator.Instance.PlayerSet("IsMove", true);
            onMove.Invoke(context.ReadValue<float>());
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            PlayerAnimator.Instance.PlayerSet("IsMove", false);
            onMoveStop.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            onJump.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            onAttack.Invoke(1);
        }
    }

    public void OnEffectAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onAttack.Invoke(SkillID);
        }
    }

    public void OnExplorAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onAttack.Invoke(3);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onDash.Invoke();
        }
    }

    public void OnSkillMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onOpenSkillMenu.Invoke();
            Time.timeScale = 0.1f;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onCloseSkillMenu.Invoke();
            Time.timeScale = 1f;
        }
    }
}
