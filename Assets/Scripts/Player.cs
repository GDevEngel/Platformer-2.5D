using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //handle
    private CharacterController _controller;
    [SerializeField] private UIManager _uIManager;

    //config
    private float _speed = 5f;
    private float _gravity = 0.5f;
    private float _jumpHeight = 25f;

    private float _yVelocity;
    private bool _doubleJumped;

    private int _collectables;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null) { Debug.LogError("player.charactercontroller is null"); }        
    }

    // Update is called once per frame
    void Update()
    {
        //get horizontal input = direction
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0);
        //velocity = direction with speed
        Vector3 velocity = direction * _speed;
        if (_controller.isGrounded == true)
        {
            _doubleJumped = false;
            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            //check for double jump
            if (Input.GetKeyDown(KeyCode.Space) && _doubleJumped == false)
            {
                _yVelocity += _jumpHeight;
                _doubleJumped = true;
            }
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        //move player with velocity
        _controller.Move(velocity * Time.deltaTime);
    }

    public void Collectable()
    {
        _collectables++;
        _uIManager.UpdateCollectableText(_collectables);
    }
}
