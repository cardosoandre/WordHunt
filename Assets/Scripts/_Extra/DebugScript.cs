using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

	}
}
