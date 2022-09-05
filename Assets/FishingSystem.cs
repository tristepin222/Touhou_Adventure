using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishingSystem : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public GameObject[] lt;
    public bool caughtFish;
    public GameObject ccatch;
    private float fishWeight;
    private float fishSpeed;
    private float random;
    private bool minigame_started = false;
    private const float SPEED = 0.5f;
    private bool boolean;
    private bool ended = false;
    // Start is called before the first frame update
    void Start()
    {
        fishWeight = 20f;
        fishSpeed = 0.1f;
        random = Random.Range(0f, 1f);
        boolean = Random.Range(0, 2) != 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        if (boolean)
        {
            if (Input.GetMouseButton(0))
            {

                slider.value -= fishSpeed / fishWeight / SPEED;

              
            }
            else
            {

                slider.value += fishSpeed / fishWeight;


               
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {

                slider.value += fishSpeed / fishWeight / SPEED;

            }
            else
            {


                slider.value -= fishSpeed / fishWeight;


            }
        }

        if (!minigame_started)
        {
            minigame_started = true;
            StartCoroutine(fishMinigame());
           
        }
        if (!ended)
        {
            ended = true;
            StartCoroutine(catchFish());
        }
        
    }
    IEnumerator fishMinigame()
    {
        boolean = Random.Range(0, 2) != 0;
        random = Random.Range(0.25f, 0.75f);
        yield return new WaitForSeconds(0.5f);
        minigame_started = false;
    }
    IEnumerator catchFish(){

        yield return new WaitForSeconds(10);
        if (slider.value > 0.75 || slider.value > 0.25)
        {
            caughtFish = true;
        }
        Destroy(ccatch);
        ended = false;
    }
}
