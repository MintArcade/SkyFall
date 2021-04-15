using UnityEngine;

public class SkyFall : MonoBehaviour {

	public float heigh;
	public float heightTarget;
	public float downForce, upForce;
	public float speedUp, speedDown;

	// Use this for initialization
	void Start () {
		heigh = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		heigh = Mathf.Lerp(heigh, heightTarget, speedDown * Time.deltaTime);
		transform.position = new Vector3(0, heigh, 0);
		heightTarget = heightTarget - downForce * Time.deltaTime;
	}

	public void Up()
	{
		heightTarget = heigh + upForce;
	}
}
