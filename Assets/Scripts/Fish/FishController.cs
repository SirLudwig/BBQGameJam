using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour
{
    [Header("Stats")]
    public FishStatistics _stats;
    private float _speed;
    private float _value;

    [Header("WayPoints")] 
    [SerializeField] private int _currentWaypoint;
    [SerializeField] private float leftWaypoint, rightWaypoint;

    private BoxCollider2D _field;
    private float collistionTimer;

    public bool isInNet = false;

    public void SetField(BoxCollider2D value)
    {
        _field = value;
    }

    private Rigidbody2D _rigidbody;

    private SpriteRenderer _spriteRenderer;

    public void SetStats(FishStatistics stats)
    {
        _stats = stats;
        CustomizateFish();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _currentWaypoint = Random.Range(0, 2);
        
        SetWaypoints();
    }

    private void SetWaypoints()
    {
        leftWaypoint = _field.bounds.min.x;
        rightWaypoint = _field.bounds.max.x;

        if (_currentWaypoint.Equals(1))
        {
            transform.LeanRotateY(0, 0.3f);
        }
    }

    private void Update()
    {
        if(isInNet)
        {
            return;
        }

        Movement();
        
        if (_currentWaypoint == 0 && transform.rotation.y == 0)
        {
            transform.LeanRotateY(180, 0.3f);
        }
    }

    private void CustomizateFish()
    {
        gameObject.name = _stats.Name;
        _speed = _stats.Speed;
        GetComponent<SpriteRenderer>().sprite = _stats.FishSprite;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isInNet)
        {
            return;
        }

        if (collision.gameObject.tag == "Fish")
        {
            collistionTimer = 0;
            ChangeDirection();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(isInNet)
        {
            return;
        }

        if (collision.gameObject.tag == "Fish")
        {
            collistionTimer -= Time.deltaTime;

            if (collistionTimer < 0)
            {
                collistionTimer = 0;
            }
        }
    }

    private void ChangeDirection()
    {
        if (_currentWaypoint == 0)
        {
            _currentWaypoint = 1;
            transform.LeanRotateY(0, 0.3f);
        }
        else
        {
            _currentWaypoint = 0;
        }
    }

    private void Movement()
    {
        Vector2 direction = _currentWaypoint.Equals(0) ? Vector2.left : Vector2.right;
        Vector2 target = _currentWaypoint.Equals(0) ? new  Vector2(leftWaypoint, transform.position.y) : new  Vector2(rightWaypoint, transform.position.y);
        float distanceToTarget = Vector2.Distance(transform.position, target);



        _rigidbody.velocity = direction * _speed;

        if (distanceToTarget < 0.5f)
        {
            ChangeDirection();
        }
        
    }
}
