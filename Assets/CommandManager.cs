using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
public class CommandManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public TMP_InputField inputField;
    public  TimeClockManager timeClockManager;
    public List<string> scene2 = new List<string>();
    private void Start()
    {
        timeClockManager = GlobalControl.Instance.UI_Time.GetComponent<TimeClockManager>();
        inputField.onSubmit.AddListener(ConfirmCommand);
    }

    public void ConfirmCommand(string value)
    {
        string Value;
        value += " ";
        int FirstWhiteSpace = value.IndexOf(" ");
        
        string Command = value.Substring(0, FirstWhiteSpace);
        int SecondWhiteSpace = value.LastIndexOf(" ");
       

        textMeshProUGUI.text += Command + "\n";

        switch (Command)
        {
            case "SetHour":
                textMeshProUGUI.text = "";
                 Value = value.Substring(FirstWhiteSpace + 1, 2);
                int intValue = int.Parse(Value);
                if (intValue < 0 || intValue > 24)
                {
                    textMeshProUGUI.text += "Please use values between : 0 and 24" + "\n";
                }
                else
                {
                    timeClockManager.SetHour(Value);
                }
               
                break;
            case "SetScene":
                textMeshProUGUI.text = "";
                Value = value.Substring(FirstWhiteSpace + 1, SecondWhiteSpace - FirstWhiteSpace -  1);

                if (scene2.Contains(Value))
                {
                    textMeshProUGUI.text += Value + "\n";

                    SceneManager.LoadScene(Value);
                }
                else
                {
                    textMeshProUGUI.text += "Unkown Scene : please use ListScene to see all scenes" + "\n";

                }
                break;
            case "ListScene":
                textMeshProUGUI.text = "";
                scene2 = new List<string>();
                 var sceneNumber = SceneManager.sceneCountInBuildSettings;
                string[] arrayOfNames;
                arrayOfNames = new string[sceneNumber];
                for (int i = 0; i < sceneNumber; i++)
                {
                    arrayOfNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                    textMeshProUGUI.text += arrayOfNames[i] + ", ";
                    scene2.Add(arrayOfNames[i]);
                }
                break;
            case "TPto":
                textMeshProUGUI.text = "";
                Value = value.Substring(FirstWhiteSpace + 1, SecondWhiteSpace - FirstWhiteSpace - 1);
                GlobalControl.Instance.player.transform.position = GameObject.Find(Value).transform.position;
                break;
            default:
                textMeshProUGUI.text += "Unknown Command" + "\n";
                break;
        }
    }
}
