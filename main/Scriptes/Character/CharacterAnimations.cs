using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { private get; set; }
    public bool IsFlying { private get; set; }
    public bool IsGrounded { private get; set; }
    public bool OnWall { private get; set; }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFlying", IsFlying);
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetBool("OnWall", OnWall);
    }

    public void Hit()
    {
        _animator.SetTrigger("Hit");
    }

    public void Jump()
    {
        if(_animator.GetBool("IsFlying") == false)
        {
            _animator.SetTrigger("Jump");
        }
    }
}
