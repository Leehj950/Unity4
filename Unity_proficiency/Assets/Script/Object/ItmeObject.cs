using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}


public class ItmeObject : MonoBehaviour , IInteractable
{
    public ItemDate itemDate;

    public string GetInteractPrompt()
    {
        string str = $"{itemDate.displayName} \n {itemDate.description}";

        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemDate = itemDate;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }

    
}
