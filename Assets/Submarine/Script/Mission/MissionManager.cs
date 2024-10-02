using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MissionGames;

public class MissionManager : MonoBehaviour {
	public static MissionManager Instance;
	public AudioClip soundRewarded;
	public AudioClip soundMissionComplete;
	[Header("Mission UI")]
	public Mission_UI[] MissionUI;
	[Header("Mission Task")]
	public Mission[] Missions;

	int missionAssigned= 0;

	void Awake(){
		Instance = this;

		foreach (var UI in MissionUI)
			UI.RewardButton.transform.parent.gameObject.SetActive (false);

		for (int i = 0;i < Missions.Length; i++) {
//			Debug.Log ("i = " + i + "mission cmplete: " + CheckMissionCompleted (i) + "istask" + isTaskStypeExist (i));
//			Debug.Log (i);
			if (!CheckMissionCompleted (i) && !isTaskStypeExist(i)) {
//				Debug.Log (i);
//				Debug.Log (GlobalValue.HighestMission);
				MissionUI [missionAssigned].RewardButton.transform.parent.gameObject.SetActive (true);
				MissionUI [missionAssigned].MissionID = i;
				MissionUI [missionAssigned].missionNumber.text = GlobalValue.HighestMission == 1 ? missionAssigned+1 + "" : GlobalValue.HighestMission + missionAssigned + "";
				MissionUI [missionAssigned].missionNameTxt.text = Missions [i].mission;
				MissionUI [missionAssigned].missionMessageTxt.text = Missions [i].message;
				MissionUI [missionAssigned].missionTargetTxt.text = Missions [i].targetAmount.ToString();
				MissionUI [missionAssigned].missonRewardCoin.text = Missions [i].rewardCoin.ToString();
				MissionUI [missionAssigned].RewardButton.SetActive (false);
				ResetValueForMission (Missions [i].task);
				missionAssigned++;
//				Debug.Log (missionAssigned);
//				Debug.Log (MissionUI.Length);
				if (missionAssigned >= MissionUI.Length)
					break;
			}
		}
	}

//	void OnEnable(){
//		CheckMissions ();
//	}

	private bool isTaskStypeExist(int ID){
		if (missionAssigned > 0) {
			foreach (var mission in MissionUI) {
				if (Missions [mission.MissionID].task == Missions [ID].task && mission.RewardButton.transform.parent.gameObject.activeSelf) {
//					Debug.Log (Missions [mission.MissionID].task);
//					Debug.Log (Missions [ID].task);
//					Debug.Log (mission.MissionID);
					return true;
				}
			}
		}

		return false;
	}

	private void ResetValueForMission(MissionGames.Task task){
		switch (task) {
		case MissionGames.Task.KillShark:
			if (!GlobalValue.isStartSharkKilled) {
				GlobalValue.SharkKilled = 0;
				GlobalValue.isStartSharkKilled = true;
			}
			break;

		case MissionGames.Task.DestroyBomb:
			if (!GlobalValue.isStartBombDestroy) {
				GlobalValue.BombDestroy = 0;
				GlobalValue.isStartBombDestroy = true;
			}
			break;

		case MissionGames.Task.Distance:
			
			break;

		case MissionGames.Task.UseRocket:
			if (!GlobalValue.isStartUseRocket) {
				GlobalValue.UseRocket = 0;
				GlobalValue.isStartUseRocket = true;
			}
			break;

		case MissionGames.Task.UseShield:
			if (!GlobalValue.isStartUseShield) {
				GlobalValue.UseShield = 0;
				GlobalValue.isStartUseShield = true;
			}
			break;

		case MissionGames.Task.CollectShieldPowerUp:
			if (!GlobalValue.isStartCollectShieldPowerUp) {
				GlobalValue.CollectShieldPowerUp = 0;
				GlobalValue.isStartCollectShieldPowerUp = true;
			}
			break;

		case MissionGames.Task.CollectMagnetPowerUp:
			if (!GlobalValue.isStartCollectMagnetPowerUp) {
				GlobalValue.CollectMagnetPowerUp = 0;
				GlobalValue.isStartCollectMagnetPowerUp = true;
			}
			break;

		case MissionGames.Task.CollectBulletPowerUp:
			if (!GlobalValue.isStartCollectBulletPowerUp) {
				GlobalValue.CollectBulletPowerUp = 0;
				GlobalValue.isStartCollectBulletPowerUp = true;
			}
			break;

		case MissionGames.Task.PlayGame:
			
			break;

		default:
			break;
		}
	}


	// Use this for initialization
	void Start () {
		
	}

	//call this when gameover
	public void CheckMissions(){
		int left;
		foreach (var mission in MissionUI) {
			switch (Missions [mission.MissionID].task) {
			case MissionGames.Task.KillShark:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.SharkKilled, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartSharkKilled = false;
					}
				}
				break;
				
			case MissionGames.Task.DestroyBomb:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.BombDestroy, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartBombDestroy = false;
					}
				}
				break;
			case MissionGames.Task.Distance:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp ((Missions [mission.MissionID].targetAmount - (int)GameManager.Instance.distance), 0, int.MaxValue);
					if (left == 0) {
						mission.missionTargetTxt.text = "Mission Complete";
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
					} else
						mission.missionTargetTxt.text = "reached: " + (int)GameManager.Instance.distance;
				}
				break;

			case MissionGames.Task.UseRocket:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.UseRocket, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartUseRocket = false;
					}
				}
				break;

			case MissionGames.Task.UseShield:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.UseShield, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartUseShield = false;
					}
				}
				break;

			case MissionGames.Task.CollectShieldPowerUp:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.CollectShieldPowerUp, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartCollectShieldPowerUp = false;
					}
				}
				break;

			case MissionGames.Task.CollectMagnetPowerUp:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.CollectMagnetPowerUp, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartCollectMagnetPowerUp = false;
					}
				}
				break;

			case MissionGames.Task.CollectBulletPowerUp:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.CollectBulletPowerUp, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
						GlobalValue.isStartCollectBulletPowerUp = false;
					}
				}
				break;

			case MissionGames.Task.PlayGame:
				if (mission.RewardButton.transform.parent.gameObject.activeSelf) {
					left = Mathf.Clamp (Missions [mission.MissionID].targetAmount - GlobalValue.PlayGame, 0, int.MaxValue);
					mission.missionTargetTxt.text = left > 0 ? "Remain: " + left : "Mission Complete";
					if (left == 0) {
						MissionComplete (mission.MissionID);
						mission.RewardButton.SetActive (true);
					}
				}
				break;
			default:
				break;
			}
		}

		if (isAnyTaskCompleted ())
			SoundManager.PlaySfx (soundMissionComplete);
	}




	public bool isAnyTaskCompleted ()
	{
		foreach (var mission in MissionUI) {
			if (mission.RewardButton.activeInHierarchy)
				return true;
		}
		return false;
	}

	//Buttons
	public void MissionButtonI(){
		if (!MissionUI [0].RewardButton.activeInHierarchy)
			return;
		
		GlobalValue.Coin += Missions [MissionUI [0].MissionID].rewardCoin;
		SoundManager.PlaySfx (soundRewarded);
		MissionUI [0].RewardButton.SetActive (false);

		GPGSManager.Instance.OpenSave(true);
	}

	public void MissionButtonII(){
		if (!MissionUI [1].RewardButton.activeInHierarchy)
			return;
		
		GlobalValue.Coin += Missions [MissionUI [1].MissionID].rewardCoin;
		SoundManager.PlaySfx (soundRewarded);
		MissionUI [1].RewardButton.SetActive (false);

		GPGSManager.Instance.OpenSave(true);
	}

	public void MissionButtonIII(){
		if (!MissionUI [2].RewardButton.activeInHierarchy)
			return;
		
		GlobalValue.Coin += Missions [MissionUI [2].MissionID].rewardCoin;
		SoundManager.PlaySfx (soundRewarded);
		MissionUI [2].RewardButton.SetActive (false);

		GPGSManager.Instance.OpenSave(true);
	}





	//Set and Get mission information
	private void MissionComplete(int ID){
		GlobalValue.HighestMission++;
		PlayerPrefs.SetInt (ID.ToString(), 1);
	}

	private bool CheckMissionCompleted(int ID){
		return PlayerPrefs.GetInt (ID.ToString(), 0) == 1;
	}

	public void GetAllRewarded(){
		MissionButtonI ();
		MissionButtonII ();
		MissionButtonIII ();
	}

	//
	[System.Serializable]
	public class Mission_UI{
		public int MissionID{ get; set; }
		public Text missionNumber;
		public Text missionNameTxt;
		public Text missionMessageTxt;
		public Text missionTargetTxt;
		public Text missonRewardCoin;
		public GameObject RewardButton;
	}


}
