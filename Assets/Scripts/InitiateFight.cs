using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InitiateFight : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float chanceMinOfFight = 0.5f;
    [SerializeField] private float chanceMaxOfFight = 0.5f;
    [SerializeField] private fade fade;
    private bool onCoolDown = false;
    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.Instance.player.GetComponent<PlayerMovement>().fightCalculated = false;
        StartCoroutine(CoolDown(5));
        GlobalControl.Instance.player.GetComponent<PlayerMovement>().initiateFight = this;
    }

    private IEnumerator CoolDown(float time)
    {
        onCoolDown = true;
        yield return new WaitForSeconds(time);
        onCoolDown = false;
    }

    public IEnumerator CalculateChance()
    {
        if (!GlobalControl.Instance.isloading)
        {
            if (!onCoolDown)
            {
                float random = Random.Range(0f, 1f);

                if (random >= chanceMinOfFight && random <= chanceMaxOfFight)
                {
                    GlobalControl.Instance.fights++;
                    objectToSpawn = GameObject.Find("Speedlines");
                    objectToSpawn.GetComponent<Canvas>().worldCamera = Camera.main;
                    fade = objectToSpawn.GetComponent<fade>();
                    fade.scene = scene;
                    fade.shouldReveal = true;
                    GlobalControl.Instance.previousPos = GlobalControl.Instance.player.transform.position;

                }
                chanceMinOfFight = chanceMinOfFight - 0.05f;
                chanceMaxOfFight = chanceMaxOfFight + 0.05f;
                yield return new WaitForSeconds(1);

                StartCoroutine(CoolDown(2.5f));

            }
        }
        GlobalControl.Instance.player.GetComponent<PlayerMovement>().fightCalculated = false;
    }
}
