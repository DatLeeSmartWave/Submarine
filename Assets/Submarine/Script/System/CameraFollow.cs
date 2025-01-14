﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	[Tooltip("Litmited the camera moving within this box collider")]
	public Collider2D Bounds;
	public float smooth = 10f;
	[Tooltip("if target is null, the camera will follow the player")]
	public Transform target;

	[HideInInspector]
	public Vector2 _min, _max;
	public bool isFollowing{ get; set; }

	void Start() {
		if (target == null)
			target = FindObjectOfType<Player> ().transform;

		if (Bounds == null) {
			Debug.LogError ("Add the Bounds object (BoxCollider2D) to limit the camera", gameObject);
			return;
		}
		
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;
		isFollowing = true;
	}

	void FixedUpdate() {

		if (!isFollowing)
			return;

		var targetPos = target.position;

		var CameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
		targetPos.x = Mathf.Clamp (targetPos.x, _min.x + CameraHalfWidth, _max.x - CameraHalfWidth);
		targetPos.y = Mathf.Clamp (targetPos.y, _min.y + Camera.main.orthographicSize, _max.y - Camera.main.orthographicSize);

		transform.position = Vector3.Lerp (transform.position, new Vector3 (targetPos.x, targetPos.y, transform.position.z), 1f / smooth);

	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (0, 0, 0, 0.1f);
		Gizmos.DrawCube (Bounds.transform.position, Bounds.bounds.size);
	}
}
