using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
// attach to UI Text component (with the full text already there)

public class typeWriterEffectAdvanced : MonoBehaviour
{

	public TextMeshProUGUI txt;
	

	void Awake()
	{
		
		txt.text = "";

		// TODO: add optional delay when to start
		
	}

	public IEnumerator PlayText(string story)
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.125f);
		}
	}

}