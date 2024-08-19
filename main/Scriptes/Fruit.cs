using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject _destroyEffect;
    [field: SerializeField] public int Points { get; private set; }

    public void Destroy()
    {
        Instantiate(_destroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
