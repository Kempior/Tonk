using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
		HideCursor();
    }

	private void HideCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void ShowCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
			ShowCursor();
		else if (Input.GetKeyUp(KeyCode.LeftAlt))
			HideCursor();
	}
}
