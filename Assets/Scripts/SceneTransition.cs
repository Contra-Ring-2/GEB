using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	//Level_00_MainlHallScene
	//Level_01_MirroredRoomScene
	//Level_02_TimeRoomScene
	//Level_03_MasterRoomScene
	public enum SceneNameType : int{
		Level_00_MainlHallScene = 0,
		Level_00_MainlHallNoPlayerScene = 1,
		Level_01_MirroredRoomScene = 2,
		Level_02_TimeRoomScene = 3,
		Level_03_MasterRoomScene = 4
	};

	private string[] levelNames = {
		"Level_00_MainlHallScene",
		"Level_00_MainlHallNoPlayerScene",
		"Level_01_MirroredRoomScene",
		"Level_02_TimeRoomScene",
		"Level_03_MasterRoomScene"
	};

	public SceneNameType sceneName;

	private GameObject player;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="PlayerCapsule"){
			player = GameObject.FindGameObjectWithTag("Player");
			StartCoroutine(SwitchScene(levelNames[(int)sceneName]));
		}
	}

	IEnumerator SwitchScene(string sceneName){
		Scene currentScene = SceneManager.GetActiveScene();
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		while (!asyncLoad.isDone){yield return null;}
		SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
		SceneManager.UnloadSceneAsync(currentScene);
	}

}
