using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerPather : MonoBehaviour {

	public Transform target;
	public float nextWaypointDistance = 1;
	float speed = 5;
	Seeker seeker;
	Path path;
	int currentWaypoint;

	// Use this for initialization
	void Start () {
		this.target = GameObject.Find ("END").transform;
		seeker = GetComponent<Seeker>();
		seeker.StartPath( transform.position, target.position, OnPathComplete);
		speed = this.gameObject.GetComponent<PlayerEnemyController>().speed;
	}
	public void calculatePath() {
		seeker = GetComponent<Seeker>();
		seeker.StartPath( transform.position, target.position, OnPathComplete);
	}
	public void OnPathComplete(Path p) {
		if(!p.error) {
			path = p;
			currentWaypoint = 0;
		}
		else {
			Debug.Log(p.error);
		}
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate() {
		// if(path == null) {
		// 	return;
		// }
		// if(currentWaypoint >= path.vectorPath.Count) {
		// 	return;
		// }
		// transform.position = path.vectorPath[currentWaypoint];

		// currentWaypoint++;

		if (path == null)
		{
			//We have no path to move after yet
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count)
		{
			Debug.Log("End Of Path Reached");
			return;
		}

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		transform.Translate(dir);

		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}
}
