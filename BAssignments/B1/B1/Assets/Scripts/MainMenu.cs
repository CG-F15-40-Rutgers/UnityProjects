using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public int level;

	void OnMouseUp () {
        Application.LoadLevel(level);
	}

}
