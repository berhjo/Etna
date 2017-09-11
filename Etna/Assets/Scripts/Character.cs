using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour// abstract is used to prevent Character script to be added to a GameObject, this used so we can only add the Player or other script to the GameObject
{
    
    

    [SerializeField]
    protected Transform bulletpos;
    [SerializeField]//Makes private float moveSpeed viseble in the inspector
    protected float moveSpeed;
    protected bool facingRight;
    [SerializeField]
    protected GameObject bulletPrefab;

    public bool Attack { get; set; }
    public Animator MyAnimator { get; private set; }



    // Use this for initialization
    public virtual void Start ()//virtual is used so we can overrite it in a different class
    {
        facingRight = true;//makes player allways facing right at start of the game
        MyAnimator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeDirections()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ShootBullet(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(bulletPrefab, bulletpos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(bulletPrefab, bulletpos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }
}
