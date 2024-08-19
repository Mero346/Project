using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvents : MonoBehaviour
{
    private CharacterInfo _info;
    private CharacterMovement _movement;
    private CharacterAnimations _animations;

    [SerializeField] private float _invulnerabilityTime;
    private float _currentInvulnerabilityTime;

    private void Start()
    {
        _info = FindObjectOfType<CharacterInfo>();
        _movement = GetComponent<CharacterMovement>();
        _animations = GetComponent<CharacterAnimations>();
    }

    private void Update()
    {
        if(_currentInvulnerabilityTime > 0)
        {
            _currentInvulnerabilityTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Fruit>(out Fruit fruit))
        {
            fruit.Destroy();
            _info.AddScore(fruit.Points);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Trap>(out Trap trap))
        {
            if (_currentInvulnerabilityTime <= 0)
            {
                Vector2 difference = transform.position - trap.transform.position;
                _movement.Knockback(difference.normalized);
                _animations.Hit();
                _currentInvulnerabilityTime = _invulnerabilityTime;
            }
        }
    }
}
