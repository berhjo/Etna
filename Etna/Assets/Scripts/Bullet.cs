using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private Rigidbody2D myRigibody;
    private Vector2 direction;
	// Use this for initialization
	void Start ()
    {
        myRigibody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        myRigibody.velocity = direction * bulletSpeed;
    }

    public void Initialize(Vector2 direction)//Initialize is used so player script can accesses this function, and vector2 directions is based on player directions
    {
        this.direction = direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
