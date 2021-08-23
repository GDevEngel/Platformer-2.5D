using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //handle
    private CharacterController _controller;

    //config
    private float _speed = 5f;
    private float _gravity = 0.5f;
    private float _jumpHeight = 25f;
    private float _speedWallJump = 10f;

    //global var
    private float _yVelocity;
    private bool _doubleJumped;
    private bool _canWallJump;
    private Vector3 _wallJumpDirection;
    private Vector3 _velocity;
    private Vector3 _direction;

    private int _collectables;
    private int _lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null) { Debug.LogError("player.charactercontroller is null"); }

        UIManager.Instance.UpdateLivesText(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded == true)
        {
            //get horizontal input = direction
            _direction = new Vector3(Input.GetAxis("Horizontal"), 0);
            //velocity = direction with speed
            _velocity = _direction * _speed;
            _doubleJumped = false;
            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump ==true)
            {
                Debug.Log("wall jump");
                _yVelocity += _jumpHeight;
                _velocity = _wallJumpDirection * _speedWallJump;
                _canWallJump = false;
            }
            //check for double jump
            else if (Input.GetKeyDown(KeyCode.Space) && _doubleJumped == false)
            {
                _yVelocity += _jumpHeight;
                _doubleJumped = true;
            }
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        //move player with velocity
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("controller hit:" + hit.gameObject.name);
        if (_controller.isGrounded == false && (hit.gameObject.tag == "Wall") == true)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue, 1f, false);
            _canWallJump = true;
            _wallJumpDirection = hit.normal;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            Death();
        }
    }

    public void Collectable()
    {
        _collectables++;
        UIManager.Instance.UpdateCollectableText(_collectables);
    }

    public void Death()
    {
        _lives--;
        UIManager.Instance.UpdateLivesText(_lives);
        if (_lives <= 0)
        {
            //restart game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("respawn at "+ GameObject.FindGameObjectWithTag("Respawn").transform.position);
            _controller.enabled = false;
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            _controller.enabled = true;
        }
    }

    public int CollectableCollected()
    {
        return _collectables;
    }
}
