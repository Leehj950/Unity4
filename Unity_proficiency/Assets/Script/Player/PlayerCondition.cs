using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damagaAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition Health { get { return uiCondition.health; } }

    Condition Stamina { get { return uiCondition.Stamina; } }

    public event Action onTakeDamage;
    public int time = 0;

     

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

        if (CharacterManager.Instance.Player.itemDate != null)
        {
            ItemDate itemDate = CharacterManager.Instance.Player.itemDate;

            if (itemDate.type == ItemType.Consumable)
            {
                // 이부분을 뜯어 고치고싶다.
                for(int i=0; i < itemDate.consumbales.Length; i++) 
                {
                    if(itemDate.consumbales[i] == null)
                    {
                        continue;
                    }

                    if (itemDate.consumbales[i].tyep == ConsumableType.health)
                    {
                        Heal(itemDate.consumbales[i].value);
                        itemDate.consumbales[i] = null;
                    }
                    else if (itemDate.consumbales[i].tyep == ConsumableType.Speed)
                    {
                        CharacterManager.Instance.Player.controller.AddMoveSpeed(itemDate.consumbales[i].value);
                        itemDate.consumbales[i] = null;
                    }

                }
            }

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
        if (Stamina.curValue <= 0)
        {
            return false;
        }
        return true;
    }

    public void TakePhysicalDamage(int damagaAmount)
    {
        Health.Subtract(damagaAmount);
        onTakeDamage?.Invoke();
    }
}

