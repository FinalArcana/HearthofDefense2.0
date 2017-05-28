using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 15f;
	public Transform target;
	public float damage = 5f;
	public float radius = 0;
	public float slow = 0f;
	public bool isSlow;
	public string element;
	GameObject boom;

	public GameObject explodeEffect;
	public GameObject boomEffect;
	public GameObject slowEffect;

	// Use this for initialization
	void Start () {
		if(isSlow) slow = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null) {
			// Our enemy went away!
			Destroy(gameObject);
			return;
		}


		Vector3 dir = target.position - this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if(dir.magnitude <= distThisFrame) {
			// We reached the node
			DoBulletHit();
		}
		else {
			// TODO: Consider ways to smooth this motion.

			// Move towards node
			transform.Translate( dir.normalized * distThisFrame, Space.World );
			Quaternion targetRotation = Quaternion.LookRotation( dir );
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
		}
	}

	void DoBulletHit() {
		// TODO:  What if it's an exploding bullet with an area of effect?

		if(radius == 0) {
			target.GetComponent<EnemyController>().TakeDamage(damage);
			GameObject boom = Instantiate(explodeEffect, transform.position, transform.rotation);
		}
		else {
			
			Collider[] cols = Physics.OverlapSphere(transform.position, radius);
			if(isSlow){
				boom = Instantiate(slowEffect, transform.position, transform.rotation);
			} else{
				boom = Instantiate(boomEffect, transform.position, transform.rotation);
			}
			foreach(Collider c in cols) {
				EnemyController e = c.GetComponent<EnemyController>();
				if(e != null) {
					// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
					
					SetDamageAccordingToElement (e);
					e.GetComponent<EnemyController>().TakeDamage(damage);
					e.GetComponent<EnemyController>().Slow(25f,5f);
					Destroy (boom,2f);
				}
			}
		}

		// TODO: Maybe spawn a cool "explosion" object here?

		Destroy(gameObject);
	}
	void SetDamageAccordingToElement(EnemyController e) {
		if (e.element.Equals ("Ice") && this.element.Equals ("Fire")) {
			damage *= 0.5f;
		} else if (e.element.Equals ("Fire") && this.element.Equals ("Ice")) {
			damage *= 1.5f;
		} else if (e.element.Equals ("Fire") && this.element.Equals ("Earth")) {
			damage *= 0.5f;
		} else if (e.element.Equals ("Earth") && this.element.Equals ("Fire")) {
			damage *= 1.5f;
		} else if (e.element.Equals ("Earth") && this.element.Equals ("Ice")) {
			damage *= 0.5f;
		} else if (e.element.Equals ("Ice") && this.element.Equals ("Earth")) {
			damage *= 1.5f;
		}
	
	}
}
