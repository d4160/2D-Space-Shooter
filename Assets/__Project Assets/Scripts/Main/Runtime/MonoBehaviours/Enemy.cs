using d4160.Core;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private void Update()
    {
        DoMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>()?.Damage();

            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void DoMove()
    {
        SingleplayerModeManager manager = SingleplayerModeManager.Instance.As<SingleplayerModeManager>();
        Vector2 xLimits = manager.EnemyXLimits;
        Vector2 yLimits = manager.EnemyYLimits;

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        Vector3 newPos = transform.position;
        if (newPos.y < yLimits.x)
        {
            newPos.y = yLimits.y;
            newPos.x = xLimits.Random();
            transform.position = newPos;
        }
    }
}
