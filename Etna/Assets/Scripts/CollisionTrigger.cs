using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    [SerializeField]
    private BoxCollider2D platformCollider;
    [SerializeField]
    private BoxCollider2D platfromTrigger;
	// Use this for initialization
	void Start ()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();//finds collider from gameobject Player and stors it in platformCollider
        Physics2D.IgnoreCollision(platformCollider, platfromTrigger, true);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);//ignore collison between platformCollider and playerCollider when the Player enter the trigger
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }
}
