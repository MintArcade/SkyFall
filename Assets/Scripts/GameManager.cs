using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int LevelId;
	public int score, bestScore;
	
	public Text tScore, tBestScore;

	public GameObject player, ringChunks;
	public Animator canvasAnimator;
	public bool[] levelUnlocked;
	public GameObject[] level;
	public SkyFall[] skyFall;
	
	// Use this for initialization
	void Start () {
		
		if (ES2.Exists("bestScore"))
		{
			bestScore = ES2.Load<int>("bestScore");
			tBestScore.text = "" + bestScore;
		}
	}
	
	public void StartGame(int id)
	{
		level[id].SetActive(true);
		player.SetActive(true);
		ringChunks.SetActive(true);
		LevelId = id;
		foreach (var item in skyFall)
		{
			item.gameObject.SetActive(true);
			item.heightTarget = 0;
		}
		
	}

	public void GameOver()
	{
		level[LevelId].GetComponent<ChunksGenerator>().GameOver();

		if (score > bestScore)
		{
			bestScore = score;
			ES2.Save(bestScore, "bestScore");
		}
		canvasAnimator.SetTrigger("Game Over");
		Invoke("Restart", 3f);
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void AddToScore (int i)
	{
		score = score + i;
		tScore.text = "" + score;
		foreach (var item in skyFall)
		{
			item.Up();
		}
		
	}
}
