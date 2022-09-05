using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using Cinemachine;
using UnityEngine.SceneManagement;
public class reactionManager : MonoBehaviour
{
    int current = 0;
    public int ID;
    public TypeWriterEffect twe;
    public Image image;
    public Text text;
    public simplierlocal sp;
    public string[] answers;
    public Text[] text2;
    public GameObject[] gbutoons;
    public GameObject panel;
    public int index = 0;
    public int index2 = 0;
    public int index3 = 0;
    public bool leave = false;
    public List<Dialogue> welcomeDialogue = new List<Dialogue>();
    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<Dialogue> dialogues1 = new List<Dialogue>();
    public List<Dialogue> dialogues2 = new List<Dialogue>();
    public bool block1;
    public bool block2;
    public bool block3;
    public ScoreScreen scoreScreen;
    private int[] answerpool;
    private int[] answerpool2;
    public int bad_string = 3;
    public int bad_reaction = 2;
    public bool canClick = false;
    public int goodbye;
    public string goodbyeString;
    public Sprite goodbyeSprite;
    public bool seller = false;
    public GameObject shop;
    public StringTable table;
    public CinemachineVirtualCamera cinemachine;
    public loadingScreen loadingScreen;
    public bool initiated;
    public bool save = true;
    public GameObject buttonShop;
    public bool isShop;
    private bool canLoad = false;
    private bool disableplayer = false;
    private bool canShow = false;
    private int moneyToWait;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }
    private void init()
    {
        if (isShop)
        {
            buttonShop.SetActive(true);
        }
        else
        {
            buttonShop.SetActive(false);
        }

        if (!canClick)
        {
            cinemachine = GlobalControl.Instance.vc.GetComponent<CinemachineVirtualCamera>();
            text.text = "";
            if (GlobalControl.Instance.DialogueInformations[ID] == null)
            {
                index = 0;
            }
            else
            {
                index = GlobalControl.Instance.DialogueInformations[ID].index;
            }
            if (GlobalControl.Instance.DialogueInformations[ID] == null)
            {
                index2 = 0;
            }
            else
            {
                index2 = GlobalControl.Instance.DialogueInformations[ID].index2;
            }
            if (GlobalControl.Instance.DialogueInformations[ID] == null)
            {
                index3 = 0;
            }
            else
            {
                index3 = GlobalControl.Instance.DialogueInformations[ID].index3;
            }
            if (save)
            {
                if (GlobalControl.Instance.DialogueInformations[ID] != null)
                {
                    if (index - 1 < 0)
                    {
                        index = 0;
                    }
                    else
                    {
                        index = GlobalControl.Instance.DialogueInformations[ID].index;

                    }
                    if (index2 - 1 < 0)
                    {
                        index2 = 0;
                    }
                    else
                    {
                        index2 = GlobalControl.Instance.DialogueInformations[ID].index2;

                    }
                    if (index3 - 1 < 0)
                    {
                        index3 = 0;
                    }
                    else
                    {
                        index3 = GlobalControl.Instance.DialogueInformations[ID].index3;

                    }


                }
                else
                {
                    index = 0;
                    index2 = 0;
                    index3 = 0;
                }
            }
            else
            {
                index = 0;
                index2 = 0;
                index3 = 0;
            }
            if (!initiated)
            {
                initiated = true;
                current = 0;

                answerpool = new int[4];
                answerpool2 = new int[4];
                Canvas c = this.GetComponent<Canvas>();
                c.worldCamera = Camera.main;
                
                Dialogue dialogue = welcomeDialogue[Random.Range(0, welcomeDialogue.Count - 1)];
                text2[0].text = dialogue.DialogueLine;
                if (dialogue.Answers.Length > 0)
                {
                    gbutoons[0].SetActive(true);
                    if (dialogue.Answers[0] == "player")
                    {


                        gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().text.text = dataStatic.Instance.characterName;
                    }
                    else
                    {
                        gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[0];
                        gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();

                    }


                }
                else
                {
                    gbutoons[0].SetActive(false);
                }
                if (dialogue.Answers.Length > 1)
                {
                    gbutoons[1].SetActive(true);
                    gbutoons[1].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[1];
                    gbutoons[1].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
                }
                else
                {
                    gbutoons[1].SetActive(false);
                }
                if (dialogue.Answers.Length > 2)
                {
                    gbutoons[2].SetActive(true);
                    gbutoons[2].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[2];
                    gbutoons[2].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
                }
                else
                {
                    gbutoons[2].SetActive(false);
                }
                gbutoons[3].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = "Leave";
                gbutoons[3].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();

                foreach (actions action in dialogue.action)
                {
                    switch (action.action)
                    {
                        case actions.actionType.None:
                            break;
                        case actions.actionType.ShowObject:
                            cinemachine.m_Lens.OrthographicSize = 15;
                            cinemachine.Follow = action.showObject.transform;
                            break;
                        case actions.actionType.TPToScene:
                            if (!SceneManager.GetActiveScene().name.Contains("Fight"))
                            {
                                GlobalControl.Instance.previousPos = GlobalControl.Instance.player.transform.position;
                            }
                            loadingScreen.sceneString = action.scene;
                            canLoad = true;
                            break;
                        case actions.actionType.WaitForEnemyDeath:

                            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
                            disableplayer = true;
                            foreach (GameObject gbutton in gbutoons)
                            {
                                gbutton.SetActive(false);
                            }
                            break;
                        case actions.actionType.WaitForItem:
                            break;
                        case actions.actionType.WaitForLevel:
                            break;
                        case actions.actionType.WaitForBossDeath:
                            BossManagerFightScene bossManagerFightScene = FindObjectOfType<BossManagerFightScene>();
                            if (bossManagerFightScene != null)
                            {
                                bossManagerFightScene.enabled = false;
                            }
                            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
                            disableplayer = true;
                            break;
                        case actions.actionType.ActivateScoreScreen:
                            canShow = true;
                            break;
                        case actions.actionType.GiveObjective:
                            List<Objective> sObjectives = new List<Objective>();
                            sObjectives.Add(action.objective);
                            GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().set(sObjectives);
                            break;
                        case actions.actionType.waitForObjective:
                            GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().Remove(action.objective);
                            break;
                    }
                }
                sp.myString.TableEntryReference = dialogue.DialogueLine;
                sp.RefreshString();
                sp.myString.StringChanged += sp.UpdateString;
                if (dialogue.portrait != null)
                {
                    image.sprite = dialogue.portrait;
                }
                if (save)
                {

                    DialogueInformation dialogueInformation = new DialogueInformation { ID = this.ID, index = this.index, index2 = this.index2, index3 = this.index3 };
                    GlobalControl.Instance.DialogueInformations[ID] = dialogueInformation;
                }

            }
            initiated = false;
        }
    }


    private void next(int i)
    {
        this.GetComponent<Canvas>().enabled = true;
        cinemachine.m_Lens.OrthographicSize = 5;
        GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = true;
        cinemachine.Follow = GlobalControl.Instance.player.transform;
        gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().text.text = "";
        gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
        bool tryleave = false;
       
        Dialogue dialogue = new Dialogue();

        if (i == 0)
        {
            if (!block1)
            {
                if (index >= dialogues.Count)
                {
                    tryleave = true;
                }
                else
                {
                    if (dialogues[index].action[0].action == actions.actionType.None)
                    {


                        dialogue = dialogues[index];

                    }
                    else
                    {
                        if (index < dialogues.Count)
                        {
                            dialogue = dialogues[index];
                        }
                        else
                        {
                            index = dialogues.Count - 1;
                        }
                    }
                }

                index++;
                if (save)
                {
                    DialogueInformation dialogueInformation = new DialogueInformation { ID = this.ID, index = this.index, index2 = this.index2, index3 = this.index3 };

                    GlobalControl.Instance.DialogueInformations[ID] = dialogueInformation;
                }
            }
        }
        else if (i == 1)
        {

            if (!block2)
            {
                if (index2 >= dialogues1.Count)
                {
                    tryleave = true;
                }
                else
                {

                    if (dialogues1[index2].action[0].action == actions.actionType.None)
                    {


                        dialogue = dialogues1[index2];

                    }
                    else
                    {
                        if (index2 < dialogues1.Count)
                        {
                            dialogue = dialogues1[index2];
                        }
                        else
                        {
                            index2 = dialogues1.Count - 1;
                        }
                    }
                }

                index2++;
                if (save)
                {

                    DialogueInformation dialogueInformation = new DialogueInformation { ID = this.ID, index = this.index, index2 = this.index2, index3 = this.index3 };
                    GlobalControl.Instance.DialogueInformations[ID] = dialogueInformation;
                }
            }
        }
        else if (i == 2)
        {

            if (!block3)
            {
                if (index3 >= dialogues2.Count)
                {
                    tryleave = true;
                }
                else
                {
                    if (dialogues2[index3].action[0].action == actions.actionType.None)
                    {

                        dialogue = dialogues2[index3];


                    }
                    else
                    {
                        if (index3 < dialogues2.Count)
                        {
                            dialogue = dialogues2[index3];
                        }
                        else
                        {
                            index3 = dialogues2.Count - 1;
                        }
                    }
                }
                index3++;

                if (save)
                {

                    DialogueInformation dialogueInformation = new DialogueInformation { ID = this.ID, index = this.index, index2 = this.index2, index3 = this.index3 };
                    GlobalControl.Instance.DialogueInformations[ID] = dialogueInformation;
                }
            }
        }
        
        if (tryleave)
        {
            Leave();
        }
        else
        {
            
            if (dialogue.Answers.Length > 0)
            {

                gbutoons[0].SetActive(true);
                if (dialogue.Answers[0] == "")
                {

                    gbutoons[0].SetActive(false);
                }
                gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[0];
                gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
            }
            else
            {
                gbutoons[0].SetActive(false);
            }
            if (dialogue.Answers.Length > 1)
            {
                gbutoons[1].SetActive(true);
                if (dialogue.Answers[1] == "")
                {

                    gbutoons[1].SetActive(false);
                }
                gbutoons[1].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[1];
                gbutoons[1].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
            }
            else
            {
                gbutoons[1].SetActive(false);
            }
            if (dialogue.Answers.Length > 2)
            {
                gbutoons[2].SetActive(true);

                if (dialogue.Answers[2] == "")
                {

                    gbutoons[2].SetActive(false);

                }
                gbutoons[2].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[2];
                gbutoons[2].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
            }
            else
            {
                gbutoons[2].SetActive(false);
            }


            sp.myString.TableEntryReference = dialogue.DialogueLine;
            sp.RefreshString();
            sp.myString.StringChanged += sp.UpdateString;
            if (dialogue.portrait != null)
            {
                image.sprite = dialogue.portrait;
            }
        }
        if (!((block1 && i == 0) || (block1 && i == 1) || (block1 && i == 2)))
        {
            if (dialogue.action == null)
            {
                Leave();
            }
            else
            {
                foreach (actions action in dialogue.action)
                {
                    switch (action.action)
                    {
                        case actions.actionType.None:
                            break;
                        case actions.actionType.ShowObject:
                            cinemachine.m_Lens.OrthographicSize = 15;
                            cinemachine.Follow = action.showObject.transform;
                            break;
                        case actions.actionType.TPToScene:
                            if (!SceneManager.GetActiveScene().name.Contains("Fight"))
                            {
                                GlobalControl.Instance.previousPos = GlobalControl.Instance.player.transform.position;
                            }
                            loadingScreen.sceneString = action.scene;
                            canLoad = true;
                            break;
                        case actions.actionType.WaitForEnemyDeath:

                            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
                            disableplayer = true;
                            foreach (GameObject gbutton in gbutoons)
                            {
                                gbutton.SetActive(false);
                            }
                            break;
                        case actions.actionType.WaitForItem:
                            break;
                        case actions.actionType.WaitForLevel:
                            break;
                        case actions.actionType.WaitForBossDeath:
                            BossManagerFightScene bossManagerFightScene = FindObjectOfType<BossManagerFightScene>();
                            if (bossManagerFightScene != null)
                            {
                                bossManagerFightScene.enabled = false;
                            }
                            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
                            disableplayer = true;
                            break;
                        case actions.actionType.ActivateScoreScreen:
                            canShow = true;
                            break;
                        case actions.actionType.GiveObjective:
                            List<Objective> sObjectives = new List<Objective>();
                            sObjectives.Add(action.objective);
                            GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().set(sObjectives);
                            break;
                        case actions.actionType.waitForObjective:
                            GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().Remove(action.objective);

                            break;
                        case actions.actionType.blockForWaitingObjective:
                            switch (i)
                            {
                                case 0:
                                    block1 = true;
                                    index--;
                                    break;
                                case 1:
                                    block2 = true;
                                    index2--;
                                    break;
                                case 2:
                                    block3 = true;
                                    index3--;
                                    break;
                            }
                            break;
                        case actions.actionType.WaitForMoney:
                            moneyToWait = action.moneyAmount;
                            switch (i)
                            {
                                case 0:
                                    block1 = true;
                                    index--;
                                    break;
                                case 1:
                                    block2 = true;
                                    index2--;
                                    break;
                                case 2:
                                    block3 = true;
                                    index3--;
                                    break;
                            }
                            break;

                    }
                }
            }

            if (dialogue.checkforObjective)
            {
                if (GlobalControl.Instance.currentobj == dialogue.objective.name)
                {
                    StartCoroutine(GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().next());
                }
            }
        }


    }
    private void Leave()
    {
        if (seller)
        {
            shop.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            sp.myString.TableEntryReference = goodbyeString;
            if (goodbyeSprite != null)
            {
                image.sprite = goodbyeSprite;
            }


        }
        sp.RefreshString();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(twe.exit());
        }
    }
    // Update is called once per frame
    public void option1()
    {
        if (!canClick)
        {
            next(0);
        }
    }
    public void option2()
    {
        if (!canClick)
        {
            next(1);
        }
    }
    public void option3()
    {
        if (!canClick)
        {
            next(2);
        }
    }
    public void option4()
    {


        Leave();


    }
    public void showShop()
    {
        shop.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (!seller)
        {
            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
        }
        else
        {
            if (shop.activeInHierarchy != true)
            {
                GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
            }
        }
    }
    private void OnEnable()
    {
       
        if (!disableplayer)
        {


            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = true;
            disableplayer = false;
        }
        if (!canClick)
        {
            init();
        }
    }
    public void OnTargetDeath()
    {
        this.GetComponent<Canvas>().enabled = true;
        next(0);
    }
    public void OnItem()
    {
        next(0);
    }
    public void OnLevel()
    {
        next(0);
    }
    public void OnFinish()
    {
        BossManagerFightScene bossManagerFightScene = FindObjectOfType<BossManagerFightScene>();
        if(bossManagerFightScene != null)
        {
            bossManagerFightScene.enabled = true;
            this.GetComponent<Canvas>().enabled = false;
        }
       
        if (canLoad)
        {
            loadingScreen.startLoading();
            canLoad = false;
        }
        if (canShow)
        {
            scoreScreen.show();
        }
    }
    public void onClick()
    {
        if (canClick)
        {
            canClick = false;
            loadingScreen.startLoading();
        }
    }
    public void CustomDialogue(Dialogue dialogue)
    {

        if (dialogue.Answers.Length > 0)
        {

            gbutoons[0].SetActive(true);
            if (dialogue.Answers[0] == "")
            {

                gbutoons[0].SetActive(false);
            }
            gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[0];
            gbutoons[0].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
        }
        else
        {
            gbutoons[0].SetActive(false);
        }
        if (dialogue.Answers.Length > 1)
        {
            gbutoons[1].SetActive(true);
            if (dialogue.Answers[1] == "")
            {

                gbutoons[1].SetActive(false);
            }
            gbutoons[1].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[1];
            gbutoons[1].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
        }
        else
        {
            gbutoons[1].SetActive(false);
        }
        if (dialogue.Answers.Length > 2)
        {
            gbutoons[2].SetActive(true);

            if (dialogue.Answers[2] == "")
            {

                gbutoons[2].SetActive(false);

            }
            gbutoons[2].gameObject.GetComponentInChildren<simplierlocal>().myString.TableEntryReference = dialogue.Answers[2];
            gbutoons[2].gameObject.GetComponentInChildren<simplierlocal>().RefreshString();
        }
        else
        {
            gbutoons[2].SetActive(false);
        }


        sp.myString.TableEntryReference = dialogue.DialogueLine;
        sp.RefreshString();
        sp.myString.StringChanged += sp.UpdateString;
        if (dialogue.portrait != null)
        {
            image.sprite = dialogue.portrait;
        }
        switch (dialogue.action[0].action)
        {
            case actions.actionType.TPToScene:
                if (!SceneManager.GetActiveScene().name.Contains("Fight"))
                {
                    GlobalControl.Instance.previousPos = GlobalControl.Instance.player.transform.position;
                }
                loadingScreen.sceneString = dialogue.action[0].scene;
                canClick = true;
                break;
        }
    
    }
}
