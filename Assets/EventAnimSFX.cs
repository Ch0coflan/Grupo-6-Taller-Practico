using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimSFX : MonoBehaviour
{
    public void TriggerEvent(string sfx)
    {
        AudioManager.Instance.PlaySFX(sfx);
    }
}
