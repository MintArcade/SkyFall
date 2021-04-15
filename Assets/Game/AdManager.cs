#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

	Button thisButton, parentButton;

	// Use this for initialization
	void Start () {
		thisButton = GetComponent<Button>();
		parentButton = transform.parent.GetComponent<Button>();
	}
	
	public void ShowRevardedAd()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("rewardedVideo", options);
	}

	void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			Debug.Log("Video completed - Offer a reward to the player");
			// Reward your player here.
			parentButton.interactable = true;
			Destroy(this.gameObject);
		}
		else if (result == ShowResult.Skipped)
		{
			Debug.LogWarning("Video was skipped - Do NOT reward the player");

		}
		else if (result == ShowResult.Failed)
		{
			Debug.LogError("Video failed to show");
		}
	}
}
#endif