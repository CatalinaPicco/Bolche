using UnityEngine;
using System.Collections;

public class SimpleEnemyScript : MonoBehaviour {

	public float velocity = 1f;

    Rigidbody2D rb;
	public Transform sightStart;
	public Transform sightEnd;

	public LayerMask detectWhat;

	public Transform weakness;

	public bool colliding;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D>();
		Physics2D.queriesStartInColliders = true;

	}
	
	// Update is called once per frame
	void FixedUpdate() {

        rb.velocity = new Vector2(transform.localScale.x * velocity, rb.velocity.y);

		colliding = Physics2D.Linecast(sightStart.position, sightEnd.position, detectWhat);

		if (colliding) {
				
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			rb.velocity *= -1;

				} 
	}

 /*     void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;

		Gizmos.DrawLine (sightStart.position, sightEnd.position);
	}  */

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {

			float height= col.contacts[0].point.y - weakness.position.y;

			if(height>0)
			{
				Dies();
				col.rigidbody.AddForce(new Vector2(0,300));


			}


				}
				
		
		}

	void Dies()
	{
		anim.SetBool ("stomped", true);
		Destroy (this.gameObject, 0.5f);
        velocity=0;
		gameObject.tag = "neutralized";
		}

}