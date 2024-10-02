using UnityEngine;
using System.Collections;

public class Shark : Enemy {
	public int health = 50;
	public float speedMin = 2;
	public float speedMax = 4;
	float speed;
	[Range(0,20)]
	public float randomScale = 10f;
	Animator anim;
	bool isDead = false;
	void Start(){
		anim = GetComponent<Animator> ();
		var randomscale = Random.Range (-randomScale, randomScale);
		transform.localScale = new Vector3 (transform.localScale.x + randomscale*0.01f, transform.localScale.y + randomscale*0.01f, transform.localScale.z);
		speed = Random.Range (speedMin, speedMax) + GameManager.Instance.Speed;
	}

	void Update(){
		if (GameManager.Instance.State == GameManager.GameState.Playing)
			transform.Translate (speed * Time.deltaTime * -1, 0, 0);
	}

	public override void Hit (int damage)
	{
		health -= damage;
		if (health <= 0)
			Die ();
		else {
			anim.SetTrigger ("Hurt");
		}
	}

	void Die(){
		anim.SetTrigger ("Die");
		isDead = true;

		GameManager.Instance.Score += score;
		GameManager.Instance.ShowFloatingText (score + "", transform.position, Color.yellow);

		GlobalValue.SharkKilled++;

		GetComponent<BoxCollider2D> ().enabled = false;
	}

	//call by animation event Die
	public void Destroy(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (isDead)
			return;
		
		var Player = other.GetComponent<Player> ();
		if (Player) {
			anim.SetTrigger ("Attack");
			Player.Damage (damage);
		}
	}
}
