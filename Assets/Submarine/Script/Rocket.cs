using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	public enum RocketType {Rocket,Bullet}
	public RocketType rocketType;
	public int damage = 10;
//	[Tooltip("give score when hit the enemy")]
//	public int scoreHit = 10;

	public bool detectSharkFirst;

	public LayerMask targetLayer;
	public float speed = 1;
	public bool isUseRadar = true;
	public float radarRadius = 3;

	public GameObject ExplosionFX;
	public AudioClip soundExplosion;

	bool isDetect;
	Transform target;


	void OnEnable(){
		gameObject.transform.SetParent (null);
		isDetect = false;
		transform.eulerAngles = Vector3.zero;
	}

	void Start(){
	}
	
	// Update is called once per frame
	void  Update () {
		if (!isDetect && isUseRadar) {
			if (detectSharkFirst) {
				if (FindObjectOfType<Shark> ()) {
					target = FindObjectOfType<Shark> ().transform;
					isDetect = true;
				}
				detectSharkFirst = false;	//just check 1 time
			} else {
				RaycastHit2D hit = Physics2D.CircleCast (transform.position + new Vector3 (radarRadius, 0, 0), radarRadius, Vector2.zero, 0, targetLayer);
				if (hit) {
					isDetect = true;
					target = hit.collider.gameObject.transform;
				}
			}
			transform.Translate (speed * Time.deltaTime, 0, 0);
		} else if (target) {
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);

			//rotate the rocket look to the target
			Vector3 dir = target.position - transform.position;
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			angle = Mathf.Lerp (transform.eulerAngles.z > 180?transform.eulerAngles.z-360:transform.eulerAngles.z, angle, 0.1f);
			transform.rotation = Quaternion.AngleAxis (angle < 0 ? angle - 360 : angle, Vector3.forward);
		} else {
			transform.Translate (speed * Time.deltaTime, 0, 0,Space.Self);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		var Enemy = other.GetComponent<Enemy> ();

		if (Enemy) {
			Enemy.Hit (damage);

			SoundManager.PlaySfx (soundExplosion);
			if (ExplosionFX != null)
				Instantiate (ExplosionFX, other.gameObject.transform.position, Quaternion.identity);

//			GameManager.Instance.ScoreBonus += scoreHit;

			Hide ();
		}
	}

	void OnBecameInvisible(){
		Hide ();
	}

	public virtual void Hide(){
		if (GameManager.Instance.RocketHolder && gameObject.activeInHierarchy) {
			gameObject.transform.SetParent (rocketType == RocketType.Rocket ? GameManager.Instance.RocketHolder.transform : GameManager.Instance.BulletHolder.transform);
			gameObject.SetActive (false);
		} else
			Destroy (gameObject);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position + new Vector3(radarRadius,0,0), radarRadius);
	}

}
