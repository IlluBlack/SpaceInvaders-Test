using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn() { }

    public void Desactivate()
    {
        this.gameObject.SetActive(false);
    }
}
