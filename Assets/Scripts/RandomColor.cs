using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

	public Renderer rend;
	public Color[] randomColors;

	[Range(1,10)]
	public float interval = 1;

	private void Reset()
	{
		
	}

	// Use this for initialization
	void Start () {
		rend = GetComponent<MeshRenderer>();
		InvokeRepeating("Randomize",1, interval);
	}

	void Randomize(){
		if (rend)
		{
			int i = Random.Range(0, randomColors.Length);
			rend.sharedMaterial.SetColor("_EmissionColor", randomColors[i]);
			Debug.Log("Color:" + i);
		}
	}

}
