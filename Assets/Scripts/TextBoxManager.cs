using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;
	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int curLine;
	public int endLine;


	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = (textFile.text.Split('\n'));
		}

		if (endLine == 0) {
			endLine = textLines.Length - 1;
		}
	}

	void Update() {
		theText.text = textLines [curLine];
		if (Input.GetKeyDown (KeyCode.Mouse0) && endLine > curLine) {
			curLine++;
		}
	}

}
