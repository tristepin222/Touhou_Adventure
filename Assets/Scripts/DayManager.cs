using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class DayManager : MonoBehaviour
{
    [SerializeField] private Gradient lightC;

    public int days;
    public float day;
    private float time = 0;
    private const float REAL_SECONDS_PER_INGAME_DAY = 600f;
   
    private bool canChangeDay = true;
    
    // Start is called before the first frame update
   
    private void OnConnectedToServer()
    {
        time = GlobalControl.Instance.time;
    }
    private void Start()
    {
        StartCoroutine(LateStart(0.2f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TimeClockManager timeClockManager = FindObjectOfType<TimeClockManager>();
        timeClockManager.dayManager = this;
    }
    // Update is called once per frame
    void Update()
    {
       
           
            this.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = lightC.Evaluate(day);
        
    }
}
