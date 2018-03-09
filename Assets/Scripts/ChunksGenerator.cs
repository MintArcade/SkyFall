using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksGenerator : MonoBehaviour {

	public GameObject cam;
	public Renderer rend;
	public Color[] randomColors;
	public int numberOfLines;
	public GameObject[] lines;
	public int currentLineIndex=0;

	// Use this for initialization
	void Start () {
		numberOfLines = transform.childCount;
		lines = new GameObject[numberOfLines];
		for (int i = 0; i < numberOfLines; i++)
		{
			lines[i] = transform.GetChild(i).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lines[currentLineIndex].transform.position.z < cam.transform.position.z-100)
		{
			//Regenerate
			lines[currentLineIndex].transform.Translate(new Vector3(
				0, 0, 100 * numberOfLines)
				);
			lines[currentLineIndex].transform.position = new Vector3(
				cam.transform.position.x,
				0,
				lines[currentLineIndex].transform.position.z);

			foreach (Transform _go in lines[currentLineIndex].transform)
			{
				if (_go.GetComponent<RandomObjectPlacer>() != null)
				{
					_go.GetComponent<RandomObjectPlacer>().Shuffle();
					RandomColor();
				}
			}
			currentLineIndex++;
			currentLineIndex = (int)Mathf.Repeat(currentLineIndex, numberOfLines);
		}
	}

	void RandomColor()
	{
		if (rend)
		{
			int i = Random.Range(0, randomColors.Length);
			rend.sharedMaterial.SetColor("_EmissionColor", randomColors[i]);
			Debug.Log("Color:" + i);
		}
	}

	public void GameOver()
	{
		GetComponent<Animator>().enabled=true;
	}
}
