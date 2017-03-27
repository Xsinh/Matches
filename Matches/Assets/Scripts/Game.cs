using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour 
{
	public GameObject[] Matches = new GameObject[45];
	int score = 0;
	public Text Score,CinText,Log;
	public Slider Cin;

	int take_game;

	void Update()
	{
		CinText.text = Cin.value.ToString ();
	}

	void Start () 
	{
		StartCoroutine (StartMatches());
	}
	
	IEnumerator StartMatches()
	{	
		int matches = Random.Range (20,45);
		score = matches;
		Score.text ="На столе " + score.ToString () + "шт. спичек" ;
		for (int i = 0; i < matches; i++) 
		{
			Matches [i].SetActive (true);
			yield return new WaitForSeconds (0.01f);
		}
	}
	public void ClicForScore()
	{	
		score = score - Mathf.RoundToInt( Cin.value );
		Log.text += "\nВы взяли: " + Cin.value.ToString();	

			StartCoroutine (MashineStart ());
			StartCoroutine (ReStartMatches ());
	}

	IEnumerator ReStartMatches()
	{
		for (int i = 0; i < Matches.Length; i++) Matches [i].SetActive (false);

		for (int i = 0; i < score; i++) Matches [i].SetActive (true);
		Score.text ="На столе " + score.ToString () + "шт. спичек" ;

		if (score <= 0) 
		{
			Score.text = "Вы победили";
			Application.Quit ();
		}
		yield return null;
	}

	IEnumerator MashineStart()
	{	
		yield return new WaitForSeconds(1);
		int r =  MachineMove( score);
		score = score - r;
		for (int i = 0; i < Matches.Length; i++) Matches [i].SetActive (false);

		for (int i = 0; i < score; i++) Matches [i].SetActive (true);

		Log.text += "\nКомпьютер взял: " + r.ToString();
		Score.text ="На столе " + score.ToString () + "шт. спичек" ;

		if (score <= 0) 
		{
			Score.text = "Победил Компьютер";
			Application.Quit ();
		}
	}

	int MachineMove(int n)
	{
		int result = n;

		if (Phi (n) == 1)
			result = (int)Cin.value + 1;
		else if (n % Phi (n) == 0)
			result = (int)Cin.value + 1;
		else
			result = (int)Cin.value - 1;
		if ((int)Cin.value == 1)
			result = 1;
		return result;
	}

	//Функция Эйлера
	int Phi(int n)
	{
		int result = n;
		for (int i = 2; i * i <= n; ++i)
			if (n % i == 0)
			{
				while (n % i == 0)
					n /= i;
				result -= result /i;
			}if (n > 1)
				result -= result / n;
		return result;
	}
}
