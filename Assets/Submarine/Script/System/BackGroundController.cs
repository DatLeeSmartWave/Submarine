using UnityEngine;
using System.Collections;

public class BackGroundController : MonoBehaviour {
	public Renderer Background;
	public float speedBG = 0.01f;
	public Renderer Midground;
	public float speedMG = 0.02f;
	public Renderer Forceground;
	public float speedFG = 0.03f;
	public Renderer Fish;
	public float speedFish = 0.015f;

//	[Tooltip("if this target == null, the target will be Main Camera")]
//	public Transform target;
//	float startPosX;
	float x;

	// Use this for initialization
	void Start () {
//		if (target == null)
//			target = Camera.main.transform;
//		
//		startPosX = target.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.State != GameManager.GameState.Playing)
			return;
		
		x += GameManager.Instance.Speed * Time.deltaTime;

		if (Background != null) {
			var offset = (x * speedBG) % 1;
			Background.material.mainTextureOffset = new Vector2 (offset, Background.material.mainTextureOffset.y);
		}
		if (Midground != null) {
			var offset = (x * speedMG) % 1;
			Midground.material.mainTextureOffset = new Vector2 (offset, Midground.material.mainTextureOffset.y);
		}
		if (Forceground != null) {
			var offset = (x * speedFG) % 1;
			Forceground.material.mainTextureOffset = new Vector2 (offset, Forceground.material.mainTextureOffset.y);
		}
		if (Fish != null) {
			var offset = (x * speedFish) % 1;
			Fish.material.mainTextureOffset = new Vector2 (offset, Fish.material.mainTextureOffset.y);
		}
	}
}
