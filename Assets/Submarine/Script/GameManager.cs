using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	public enum GameState{Menu,Playing,Pause,GameOver}
	public GameState State{ get; set; }

	public LevelBlocks[] LevelBlock;

	[Header("Floating Text")]
	public GameObject FloatingText;
	private MainMenu menuManager;

	public float Speed{ get; set; }
	public int Score{ get; set; }
	[HideInInspector]
	public float distance = 0;

	public GameObject RocketHolder{ get; set; }
	public GameObject BulletHolder{ get; set; }
	public Player Player{ get; set; }
	public SoundManager SoundManager{ get; set; }

	void Awake(){
		Instance = this;
		menuManager = FindObjectOfType<MainMenu> ();

		RocketHolder = new GameObject ();
		RocketHolder.name = "Rocket Hoder";

		BulletHolder = new GameObject ();
		BulletHolder.name = "Bullet Holder";

		Player = FindObjectOfType<Player> ();
		SoundManager = FindObjectOfType<SoundManager> ();

		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		Speed = LevelBlock [0].levelSpeed;
		SetNewSubmarine ();

		SpawnLevelBlock ();
	}

	void Update(){
		if (State == GameState.Playing) {
			distance += Speed * Time.deltaTime;
		}
	}

	public void Play(){
		State = GameState.Playing;
		Player.Play ();

		if (AdsManager.Instance)
			AdsManager.Instance.ShowAdmobBanner(false);
	}

	public void SetNewSubmarine(){
		var Submarine = CharacterHolder.Instance.GetPickedCharacter ();
		if (Player.gameObject) {
			var pos = new Vector3 (Player.transform.position.x, 0, 0);
			Destroy (Player.gameObject);
			var newSubmarine = Instantiate (Submarine, pos, Quaternion.identity) as GameObject;
			Player = newSubmarine.GetComponent<Player> ();
		}


	}

	public void SpawnLevelBlock(){
		for (int i = LevelBlock.Length - 1; i >= 0; i--) {
			if (distance >= LevelBlock [i].distanceReachLevel) {
				Instantiate (LevelBlock [i].Levels [Random.Range (0, LevelBlock [i].Levels.Length)]);
				Speed = LevelBlock [i].levelSpeed;
				break;
			}
		}


	}
	
	public void GameOver(){
		//check best
		if (Score > GlobalValue.Best)
			GlobalValue.Best = Score;

		MissionManager.Instance.CheckMissions ();
		State = GameState.GameOver;
		MainMenu.Instance.GameOver ();
		Speed = 0;
		Player.Die ();

		GPGSManager.Instance.OpenSave(true);

		if (AdsManager.Instance)
		{
			AdsManager.Instance.ShowAdmobBanner(true);
			AdsManager.Instance.ShowNormalAd();
		}
	}
		
	[System.Serializable]
	public class LevelBlocks{
		public int distanceReachLevel = 0;
		public float levelSpeed = 3;
		public GameObject[] Levels;
	}

	public void ShowFloatingText(string text, Vector2 positon, Color color){
		GameObject floatingText = Instantiate (FloatingText) as GameObject;
//		var _position = Camera.main.WorldToScreenPoint (positon);

		floatingText.transform.SetParent (menuManager.transform,false);
		floatingText.transform.position = positon;

		var _FloatingText = floatingText.GetComponent<FloatingText> ();
		_FloatingText.SetText (text, color);
	}

}


