using UnityEngine;
using System.Collections;

public class MoveToPlayer : MonoBehaviour {
	public float speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, GameManager.Instance.Player.transform.position, speed * Time.deltaTime);
	}
}
