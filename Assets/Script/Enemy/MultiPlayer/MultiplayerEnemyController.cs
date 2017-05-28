using UnityEngine;
using System.Collections;

public class MultiplayerEnemyController : MonoBehaviour 
{
	private MultiplayerEnemy enemy;

	public float speed;
	public bool dead;
	public AudioSource audioSource;
	public GameObject deadEffect;



	void Start () 
	{
		
		dead = false;
	
//		enemy = new MultiplayerEnemy (this.speed);

		audioSource = FindObjectOfType<AudioSource> ();

		//Let's illustrate inheritance with the 
		//default constructors.

	}
		

	void destroy() {
		if (audioSource == null) {
			GameObject dead = Instantiate (deadEffect, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), transform.rotation);
			transform.position = Vector3.one * -9999999f;
			Destroy (this.gameObject);
			Destroy (dead,2f);
		}
		else {
			GameObject dead = Instantiate (deadEffect, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), transform.rotation);
			audioSource.Play ();
			transform.position = Vector3.one * -9999999f;
			Destroy (this.gameObject,audioSource.clip.length);
			Destroy (dead,2f);
		}

	}

	void Update() {

	}




	void FixedUpdate() {

	}



}