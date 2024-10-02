using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
#endif
using System;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager Instance;

    public string statusTxt;
 
    public string dataSaveGet;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   public bool isSignIn = false;

    // Start is called before the first frame update
    void Start()
    {

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);

    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            isSignIn = true;
            statusTxt = "Sign in successfully!";
            Debug.Log("Sign in successfully!");
            //load the data when open the game first time
            if (GlobalValue.isNewGame)
            {
                OpenSave(false);

            }
            // Continue with Play Games Services
        }
        else
        {
            Debug.Log("Sign in fail!");
            statusTxt = "Sign in fail!";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }


    #region SAVE GAME
    private bool isSaving;

    public void OpenSave(bool saving)
    {


        Debug.Log("Open Save");
        statusTxt = "Open Save";
        if (isSignIn)
        {
            statusTxt = "User is authenticated";
            Debug.Log("User is authenticated");
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, SaveGameOpen);
        }

    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {

     
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving)       //are saving
            {
                Debug.Log("status successfully, attempting save...");
                statusTxt = "status successfully, attempting save...";
                byte[] myData = System.Text.ASCIIEncoding.ASCII.GetBytes(GetSaveString());

                SavedGameMetadataUpdate updateForMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("I have updated my game at: " + DateTime.Now.ToString()).Build();
                // handle reading or writing of saved game.
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updateForMetadata, myData, SaveCallBack);
            }
            else      //loading
            {
                Debug.Log("status successfully, attempting load...");
                statusTxt = "status successfully, attempting load...";
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, LoadCallBack);
            }
        }
        else
        {
            Debug.Log("error, " + status);
            statusTxt = "error, " + status;
        }

    }


    private void LoadCallBack(SavedGameRequestStatus status, byte[] data)
    {
       if(status == SavedGameRequestStatus.Success)
        {
            Debug.Log("load successfully, attempting to read data...");
            statusTxt = "load successfully, attempting to read data...";
            string loadedData = System.Text.ASCIIEncoding.ASCII.GetString(data);

            LoadSavedString(loadedData);
        }
    }

    private void LoadSavedString(string cloudData)
    {
        string[] cloudStringArr = cloudData.Split('|');

        GlobalValue.Coin = int.Parse(cloudStringArr[0]);

        dataSaveGet = "Data get: " + cloudData;

        GlobalValue.isNewGame = false;
    }

    private string GetSaveString()
    {
        string dataToSave = "";

        dataToSave += GlobalValue.Coin;       //coin
        dataToSave += "|test";

        //save skill
        dataSaveGet = "Data save: " + dataToSave;

        return dataToSave;
    }

    private void SaveCallBack(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            statusTxt = "Successful saved on the cloud";
        }
        else
        {
            statusTxt = "Failed to save on the cloud";
        }
    }


#endregion


    //DELETE SAVED GAME
    public void DeleteGameData()
    {

        // Open the file to get the metadata.
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, DeleteSavedGame);

    }

    public void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
            statusTxt = "Sucessfully Delete saved game";
        }
        else
        {
            statusTxt = "Error Delete saved game";
        }
    }

}