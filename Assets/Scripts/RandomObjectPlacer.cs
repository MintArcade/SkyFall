using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectPlacer : MonoBehaviour {

	public Vector2 size;
	[Range(1,20)]
	public int ammountOfItem;
	public GameObject[] objects;
	[Range(0.5f, 10)]
	public float minSize = 1;
	[Range(1, 25f)]
	public float maxSize = 1;
	public bool noRotation;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < ammountOfItem; i++)
		{
			int r = Random.Range(0, objects.Length);
			Instantiate(objects[r], transform);
		}
		Shuffle();
	}

	public void Shuffle()
	{
		foreach (Transform _go in transform)
		{
			_go.localPosition =
				new Vector3(Random.Range(-size.x, size.x), 0,
					Random.Range(-size.y, size.y));
			if (noRotation == false)
			{
				_go.localEulerAngles = new Vector3(0, Random.Range(-360, 360), 0);
			}
			float lScale = Random.Range(minSize, maxSize);
			_go.localScale = new Vector3(lScale, lScale, lScale);
		}
	}
}