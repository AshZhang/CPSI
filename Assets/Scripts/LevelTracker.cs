using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTracker : MonoBehaviour
{

	public Button buttonPrefab;
	public TextAsset levelList;

	private GameObject scrollContent;
	private Button[] buttons;
	private string[] levels;
	private string levelSelection;
	private static LevelTracker instance = null;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			return;
		}
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		scrollContent = GameObject.Find ("Content");
		levelSelection = "CompSci";
		levels = levelList.text.Split ('\n');
		buttons = new Button[levels.Length];
		for (int i = 0; i < levels.Length; i++) {
			buttons [i] = Instantiate (buttonPrefab);
			buttons [i].transform.GetChild (0).GetComponent<Text> ().text = levels [i];
			buttons [i].transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Art/Menu Icons/" + levels [i]);
			buttons [i].transform.SetParent (scrollContent.transform);
			string level = levels [i];
			buttons [i].onClick.AddListener (() => chooseLevel (level));
		}
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public void chooseLevel (string level)
	{
		levelSelection = level;
		SceneManager.LoadScene ("Game");
	}

	public string getLevel ()
	{
		return levelSelection;
	}
}
