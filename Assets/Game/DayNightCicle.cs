using UnityEngine;

public class DayNightCicle : MonoBehaviour {
	[Header("Timeline only")]
	public float sunSpeed;
	public Color fogColor, ambientSkyColor;
	
	// Update is called once per frame
	void Update () {
		RenderSettings.fogColor = fogColor;
		RenderSettings.ambientSkyColor = ambientSkyColor;
	}
}
