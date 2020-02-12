using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Range(0f, 31f)]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _yPositionToDestroy = 8f;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > _yPositionToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
