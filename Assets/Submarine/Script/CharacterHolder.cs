using UnityEngine;
using System.Collections;

public class CharacterHolder : MonoBehaviour {
	public static CharacterHolder Instance;

	public GameObject[] Characters;

	// Use this for initialization
	void Awake () {
		Instance = this;

		GetPickedCharacter ();
	}

	public GameObject GetPickedCharacter(){
		foreach (var character in Characters) {
			var ID = character.GetInstanceID ();
			if (GlobalValue.CharacterPicked (0, false) == ID) {
				return character;
			}
		}

		//return default character at 1 position
		return Characters [0];
	}
}
