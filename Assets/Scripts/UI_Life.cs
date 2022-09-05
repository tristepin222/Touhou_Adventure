using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Life : MonoBehaviour
{
    private Transform lifeContainer;
    private Text lifeText;
    private LifeManagament life;
    public Slider slider;
    private  void Awake() {
    lifeContainer = transform.Find("LifeContainer");
    lifeText = lifeContainer.Find("LifeText").GetComponent<Text>();
        
    }

    public void setLifeUI(LifeManagament life){

        lifeContainer = transform.Find("LifeContainer");
        lifeText = lifeContainer.Find("LifeText").GetComponent<Text>();

        this.life = life;

        lifeText.text = life.lifeAmount.ToString() + "/" + life.maxLife.ToString();
        slider.value = life.lifeAmount;
        slider.maxValue = life.maxLife;
        this.life.LifeChanged += LifeManagament_On_LifeChanged;
    }
    private void LifeManagament_On_LifeChanged(object sender, System.EventArgs e){
      RefreshLife();
    }
    public void RefreshLife(){
        lifeText.text = life.lifeAmount.ToString() + "/" + life.maxLife.ToString();
        slider.value = life.lifeAmount;
        slider.maxValue = life.maxLife;
    }
}
