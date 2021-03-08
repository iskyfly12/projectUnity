using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private string itemTag;
    public virtual void Collect(PlayerPeace player) { gameObject.SetActive(false); }

    public virtual void PlayEffect() { }

    public string GetItemTag()
    {
        return itemTag;
    }
}
