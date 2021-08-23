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

    private float _yVelocity;
    private bool _doubleJumped;

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
