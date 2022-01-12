using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeSkillUI : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Button fireSkillButton;
    [SerializeField] private Button shieldSkillButon;
    [SerializeField] private Canvas skillCanvas;
    private event UnityAction<int> changeSkill = delegate {  };

    private void Awake()
    {
        playerInput.onOpenSkillMenu += OpenSkillMenu;
        playerInput.onCloseSkillMenu += CloseSkillMenu;
        skillCanvas.enabled = false;
    }

    private void OnEnable()
    {
        changeSkill += OnClickToChangeSkill;
        fireSkillButton.onClick.AddListener(() => changeSkill.Invoke(2));
        shieldSkillButon.onClick.AddListener(() => changeSkill.Invoke(7));
    }

    private void OnDisable()
    {
        changeSkill -= OnClickToChangeSkill;
        fireSkillButton.onClick.RemoveAllListeners();
        shieldSkillButon.onClick.RemoveAllListeners();
    }

    private void OnClickToChangeSkill(int skillID)
    {
        PlayerSkillSystem.Instance.ChangeSkill(skillID);
    }

    private void OpenSkillMenu()
    {
        skillCanvas.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseSkillMenu()
    {
        skillCanvas.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
