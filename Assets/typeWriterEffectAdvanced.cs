using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
// attach to UI Text component (with the full text already there)

public class typeWriterEffectAdvanced : MonoBehaviour
{

	public TextMeshProUGUI txt;
	public float time = 0.125f;
	public Image Arrow;
	public string[] texts;
	public string scene = "PlayerHouseWorld";
	public bool[] Show;
	public bool isCinematic;
	public LocalizeStringEvent LSE;
	public AudioSource AS;
	public Animator animator;
	public GameObject input;
	public loadingScreen loadingscreen;
	private int i = 0;
	private string name;

	void Awake()
	{
		
		txt.text = "";
		if (options.Instance != null)
		{
			AS.volume = options.Instance.sAmount;
		}
		// TODO: add optional delay when to start

	}
    private void Start()
    {
		input.SetActive(false);

	}

    public void StartCo(string story)
    {
		
		StartCoroutine(PlayText(story));
    }
	public IEnumerator PlayText(string story)
	{
		
		txt.text = "";
		if (name != "")
		{
			story = story + " " + name;
		}
		foreach (char c in story)
		{
			txt.text += c;
			if (c != ' ')
			{
				AS.Play();
			}
			yield return new WaitForSeconds(time / story.Length * 10);
			AS.Stop();
		}
		if (isCinematic)
		{
			
				Arrow.enabled = true;
		
			animator.enabled = false;
		}
		
		
	}

	private void Update()
	{
		if (isCinematic)
		{
			if (Arrow.enabled || input.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.Return))
				{
					if (i >= texts.Length - 1)
					{
						loadingscreen.sceneString = scene;
						loadingscreen.startLoading();
					}
					else
					{
						if (Show[i])
						{
							name = dataStatic.Instance.characterName;
						}
						else
						{
							name = "";
						}
						input.SetActive(false);
						Arrow.enabled = false;
						animator.enabled = true;
						i++;

						LSE.StringReference.SetReference("Test", texts[i]);

					}
				}
			}
		}
	}
}