using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    // public static SceneNameType currentScene = SceneNameType.Level_00_MainlHallNoPlayerScene;
	public SceneNameType sceneName;

	private GameObject player;
    private Mutex switchSceneLock = new Mutex();

    string GetSceneRealName(SceneNameType sceneName)
    {
        return levelNames[(int)sceneName];
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="PlayerCapsule"){
			player = GameObject.FindGameObjectWithTag("Player");
            //StartCoroutine(SwitchScene(levelNames[(int)sceneName]));

            // TODO: ?
            // SceneNameType nextScene = other.GetComponent<SceneTransition>().sceneName;

            /*
            SceneNameType nextScene = sceneName;
            string nextSceneName = getSceneRealName(nextScene);

            if (!SceneManager.GetSceneByName(getSceneRealName(sceneName)).IsValid())
            {
                Scene currentScene = SceneManager.GetActiveScene();

                SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));

                SceneManager.UnloadSceneAsync(currentScene);
                // Debug.Log(SceneManager.GetActiveScene());
            }
            */

            StartCoroutine(SwitchScene(levelNames[(int)sceneName]));
        }
    }

    private void Start()
    {
        Debug.Assert(SceneManager.GetActiveScene().name == GetSceneRealName(SceneNameType.Level_00_MainlHallScene));
    }

    // dont destroy : play must loaded-stuff
    // only switch lv00 lv01 lv02 lv03
    
    IEnumerator SwitchScene(string sceneName){
        switchSceneLock.WaitOne();

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != sceneName)
        {
            if (!SceneManager.GetSceneByName(sceneName).IsValid())
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

                // while (!asyncLoad.isDone) { yield return null; }
                while (!asyncLoad.isDone) { yield return new WaitForSeconds(0.05f); }

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            }

            // SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
            SceneManager.UnloadSceneAsync(currentScene);
        }

        switchSceneLock.ReleaseMutex();
    }
    
}
