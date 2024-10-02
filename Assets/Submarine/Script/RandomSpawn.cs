using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour {
	public GameObject[] Items;

	public int timeMin = 5;
	public int timeMax = 15;

	public Vector2 spawnZone;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnTheShark ());
	}

	IEnumerator SpawnTheShark(){
		yield return new WaitForSeconds (Random.Range (timeMin, timeMax));

		if (GameManager.Instance.State == GameManager.GameState.Playing)
			Instantiate (Items [Random.Range (0, Items.Length)], new Vector2 (Random.Range (transform.position.x - spawnZone.x/2, transform.position.x + spawnZone.x/2),
				Random.Range (transform.position.y - spawnZone.y/2, transform.position.y + spawnZone.y/2)), Quaternion.identity);

		StartCoroutine (SpawnTheShark ());
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (1, 1, 1, 0.1f);
		Gizmos.DrawCube (transform.position, spawnZone);
	}
}
