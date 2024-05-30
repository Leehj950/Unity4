using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damagaAmount);
}

public class PlayerCondition : MonoBehaviour , IDamagable
{
    public UICondition uiCondition;
    
    Condition Health { get { return uiCondition.health; } }

    Condition Stamina { get { return uiCondition.Stamina; } }

    public event Action onTakeDamage;

    private void Update()
    {
        if (CharacterManager.Instance.Player.controller.IsMove == true)
        {
            Stamina.Subtract(CharacterManager.Instance.Player.controller.useStamina * 2);
        }
        else
        {
            Stamina.Add(Stamina.passiveValue * Time.deltaTime);
        }
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void Useitem(float amount)
    {
        Stamina.Add(amount);    
    }

    public bool StopMove()
    {
        if(Stamina.curValue <= 0)
        {
            return false; 
        }
        return true;
    }

    public  void TakePhysicalDamage(int damagaAmount)
    {
        Health.Subtract(damagaAmount);
        onTakeDamage?.Invoke();
    }
}

