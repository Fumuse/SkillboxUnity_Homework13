using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _speed = 4f;
    [SerializeField, Range(0, 10)] private float _jumpForce = 4f;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private ParticleSystem _playerDeath;
    [SerializeField] private GameObject _playerBody;

    private Rigidbody _playerRB;
    private Animator _playerAnimator;
    private PlayerInputs _playerMovement;

    private bool _isGrounded = true;
    private bool _dead = false;

    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        _playerAnimator.SetFloat("Velocity", _playerRB.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Перемещение игрока
    /// </summary>
    private void Move()
    {
        if (_dead) return;
        _playerRB.AddForce(_playerMovement.Movement * _speed);
    }

    /// <summary>
    /// Прыжок игрока
    /// </summary>
    public void Jump()
    {
        if (_dead) return;
        if (!_isGrounded) return;
        
        Vector3 movement = _playerMovement.Movement;
        movement.y = Vector3.up.y * _jumpForce;
        _playerRB.AddForce(movement, ForceMode.Impulse);
        _isGrounded = false;
    }

    /// <summary>
    /// Смерть игрока
    /// </summary>
    public void Death()
    {
        if (_dead) return;
        
        _dead = true;
        _playerRB.isKinematic = true;
        _playerBody.SetActive(false);
        _playerDeath.Play();
    }

    public float PlayerDeathTime()
    {
        var main = _playerDeath.main;
        return main.duration + main.startLifetimeMultiplier;
    }

    /// <summary>
    /// Определение земли под ногами игрока
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & _ground) != 0)
            _isGrounded = true;
    }

#if UNITY_EDITOR
    [ContextMenu("Reset values")]
    public void ResetValues()
    {
        _speed = 4f;
        _jumpForce = 4f;
    }
#endif
}