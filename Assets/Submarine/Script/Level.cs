using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public float spawnPosition = 20;
	public float destroyAtPosition = -20;

	bool isSpawnNew = false;
	// Update is called once per frame
	void Awake(){
		transform.position = new Vector3 (spawnPosition, 0, 0);
	}

	void Update () {
		if (GameManager.Instance.State == GameManager.GameState.Playing)
			transform.Translate (GameManager.Instance.Speed * -1 * Time.deltaTime, 0, 0);

		if (!isSpawnNew && transform.position.x <= 0) {
			isSpawnNew = true;
			GameManager.Instance.SpawnLevelBlock ();
		}

		if (transform.position.x <= destroyAtPosition) {
			Destroy (gameObject);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube (transform.position, new Vector3 (20, 10f, 0));
	}
}
