using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class expSystem : MonoBehaviour
{
    private const float BASE_XP_NEED = 100;
    private const float RATIO = 1.2f;
    private float nextXPNeed;
    private float currentXP;
    private float spareXP;
    private int currentLVL;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private Slider expbar;
    // Start is called before the first frame update
    void Start()
    {
       
        currentLVL = 0;
        if(dataStatic.Instance != null)
        {
            nextXPNeed = dataStatic.Instance.nextXp;
            currentLVL = dataStatic.Instance.lvl;
            currentXP = dataStatic.Instance.xp;
            if(nextXPNeed == 0)
            {
                nextXPNeed = BASE_XP_NEED;
            }
            expbar.value = currentXP / nextXPNeed;
            lvlText.text = currentLVL.ToString();
        }
      
    }

    public void AddXp(float amount)
    {
        currentXP += amount;
        expbar.value = currentXP / nextXPNeed;
        if (currentXP >= nextXPNeed)
        {
            levelUp();

        }
        GlobalControl.Instance.xp = currentXP;
       
    }
    private void levelUp()
    {
        spareXP = currentXP - nextXPNeed;
        currentXP = 0;
        nextXPNeed = nextXPNeed * RATIO;
        currentLVL++;
        lvlText.text = currentLVL.ToString();
        AddXp(spareXP);
        GlobalControl.Instance.nextXp = nextXPNeed;
        GlobalControl.Instance.lvl = currentLVL;
    }
}
