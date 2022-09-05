using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
public class ObjectiveManager : MonoBehaviour
{

    public Camera camera;
    public List<Objective> Objectives = new List<Objective>();
    public List<GameObject> GameObjects = new List<GameObject>();
    public Text name;
    public Text desc;
    public RectTransform arrow;
    private Vector3 Objective;
    float bordersize = 100f;
    float bordersize2 = 100f;
    public Sprite sprite1;
    public Sprite sprite2;
    private int i = 0;
    private int i2 = 0;
    int i3 = 0;
    bool canCollide = true;
    private bool found = false;
    GameObject gm;
    private int i4;
    private int i5;
    private bool init = false;
    private void Start()
    {
        
        set();
    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        foreach( Item item in GlobalControl.Instance.inventory.getItemList())
        {
            if (item != null)
            {
                if (item.itemScriptableObject.itemType == Objectives[i - 1].itemToCheck.itemType && Objectives[i - 1].checkforitem)
                {
                    StartCoroutine(next());
                    GlobalControl.Instance.inventory.OnItemListChanged -= Inventory_OnItemListChanged;
                }
            }
        }
        
    }

    public void set(List<Objective> sObjectives = null, bool doCheck = true)
    {
        canCollide = true;
        i = 0;
        i3 = 0;
        i2 = 0;
        i4 = 0;
        i5 = GlobalControl.Instance.objindex5;
          camera = Camera.main;
        RectTransform canva = GlobalControl.Instance.UI_Arrow.GetComponent<RectTransform>().GetChild(0).GetChild(0).GetComponent<RectTransform>();
        arrow = canva.GetComponent<RectTransform>();
        Collider2D arrowC = canva.GetComponent<Collider2D>();
        ColliderBrige cb = arrowC.gameObject.AddComponent<ColliderBrige>();
        cb.Initialize(this);
        arrow.gameObject.SetActive(false);
        ObjectiveList ob;
        
        
        found = GameObject.FindGameObjectWithTag("ObjectiveList").TryGetComponent<ObjectiveList>(out ob);
        if (sObjectives != null)
        {
            found = true;
            Objectives = sObjectives;
        }
        else
        {
            if (found)
            {
                Objectives = ob.Objectives;

            }
            else
            {
                if (!init)
                {
                    init = true;
                    Objectives.Add(Resources.Load<Objective>("ScriptableObjects/objectives/" + GlobalControl.Instance.currentobj));
                    found = true;
                }
            }
            
        }
        if (Objectives.Count != 0)
        {
            if (Objectives[0] == null)
            {
                found = false;
            }
            if (found)
            {
                if (GlobalControl.Instance.obtosv.Count == 0)
                {
                    i3 = 0;
                    i = 0;
                    i4 = 0;
                }
                else
                {
                    if (doCheck && Objectives.Count != 0)
                    {
                        for (i2 = 0; GlobalControl.Instance.obtosv.Count > i2; i2++)
                        {
                            if (GlobalControl.Instance.obtosv[i2] == Objectives[i4].ID)
                            {
                                break;
                            }

                        }
                        for (i3 = i2; GlobalControl.Instance.obtosv.Count > i3; i3++)
                        {
                            if (GlobalControl.Instance.obtosv[i3] != Objectives[i4].ID)
                            {
                                break;
                            }
                            i4++;
                        }
                    }
                }
                if (i < Objectives.Count)
                {
                    desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = Objectives[i].desc;
                    name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = Objectives[i].objname;
                    desc.GetComponent<dynamicLocale>().textt = Objectives[i].desc;
                    name.GetComponent<dynamicLocale>().textt = Objectives[i].objname;
                    desc.GetComponent<dynamicLocale>().StringReference.RefreshString();
                    name.GetComponent<dynamicLocale>().StringReference.RefreshString();
                    if (Objectives[i].InMap)
                    {
                        if (Objectives[i].useString)
                        {
                            gm = GameObject.Find(Objectives[i].PrefabNames[0]);
                            if (gm != null)
                            {
                                arrow.gameObject.SetActive(true);

                                Objective = gm.transform.position;
                            }
                        }
                        else
                        {
                            gm = Objectives[i].Prefabs[0];
                            if (gm != null)
                            {
                                arrow.gameObject.SetActive(true);
                                Objective = Objectives[i].Prefabs[0].transform.position;
                            }
                        }
                    }
                    else
                    {
                        arrow.gameObject.SetActive(false);
                    }
                    if (Objectives[i].checkforitem)
                    {
                        GlobalControl.Instance.inventory.OnItemListChanged += Inventory_OnItemListChanged;
                    }
                    else
                    {
                        GlobalControl.Instance.inventory.OnItemListChanged -= Inventory_OnItemListChanged;
                    }

                }

                if (Objectives[i].useString)
                {
                    if (Objectives[i].PrefabNames.Count > i5)
                    {
                        gm = GameObject.Find(Objectives[i].PrefabNames[i5]);
                        if (gm != null)
                        {
                            arrow.gameObject.SetActive(true);

                            Objective = gm.transform.position;
                        }


                    }
                }
                else
                {
                    i++;
                    arrow.gameObject.SetActive(false);
                }
                if (Objectives.Count != 0)
                {
                    if (i < Objectives.Count)
                    {

                        GlobalControl.Instance.currentobj = Objectives[i].name;
                    }
                }
            }
            else
            {
                desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
                name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
            }
        }
        else
        {
            desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
            name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
        }
    }

    private void Update()
    {
        if (gm != null && SceneManager.GetActiveScene().name != "MainMenu")
        {
            Vector3 toPosition = Objective;
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
            

            Vector3 targetPositionSP = Camera.main.WorldToScreenPoint(Objective);
            bool isOffScreen = targetPositionSP.x <= 0 || targetPositionSP.x >= Screen.width - bordersize || targetPositionSP.y <= 0 || targetPositionSP.y >= Screen.height - bordersize;

            if (isOffScreen)
            {
                arrow.localEulerAngles = new Vector3(0, 0, angle);
                arrow.GetComponent<Image>().sprite = sprite1;
                Vector3 CappedTargetP = targetPositionSP;
                if (CappedTargetP.x <= bordersize2) CappedTargetP.x = bordersize2;
                if (CappedTargetP.x >= Screen.width - bordersize) CappedTargetP.x = Screen.width - bordersize;
                if (CappedTargetP.y <= bordersize2) CappedTargetP.y = bordersize2;
                if (CappedTargetP.y >= Screen.height - bordersize) CappedTargetP.y = Screen.height - bordersize;

                Vector3 Pwp = Camera.main.ScreenToWorldPoint(CappedTargetP);
                arrow.position = Pwp;
                arrow.localPosition = new Vector3(arrow.localPosition.x, arrow.localPosition.y, 0f);
            }
            else
            {
                arrow.GetComponent<Image>().sprite = sprite2;
                Vector3 Pwp = Objective;
                arrow.position = Pwp;
                arrow.localEulerAngles = new Vector3(0, 0, 0);
                arrow.localPosition = new Vector3(arrow.localPosition.x, arrow.localPosition.y, 0f);
            }
        }
    }
    public void check()
    {
        if ( Objectives.Count != 0) {

            
        }
        else
        {
            return;
            if (Objectives[0] == null)
            {
                Objectives.Clear();
                return;
            }
        }

        if (Objectives.Count > i)
        {
            GlobalControl.Instance.currentobj = Objectives[i].name;
            found = true;
            desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = Objectives[i].desc;
            name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = Objectives[i].objname;
            desc.GetComponent<dynamicLocale>().textt = Objectives[0].desc;
            name.GetComponent<dynamicLocale>().textt = Objectives[0].name;
            desc.GetComponent<dynamicLocale>().StringReference.RefreshString();
            name.GetComponent<dynamicLocale>().StringReference.RefreshString();
            if (Objectives[i].InMap)
            {
                if (Objectives[i].useString)
                {
                    gm = GameObject.Find(Objectives[i].PrefabNames[i5]);
                    if (gm != null)
                    {
                        arrow.gameObject.SetActive(true);

                        Objective = gm.transform.position;
                    }
                }
                else
                {
                    gm = Objectives[i].Prefabs[i5];
                    if (gm != null)
                    {
                        arrow.gameObject.SetActive(true);
                        Objective = Objectives[i].Prefabs[i5].transform.position;
                    }
                }
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
            if (Objectives[i].checkforitem)
            {
                GlobalControl.Instance.inventory.OnItemListChanged += Inventory_OnItemListChanged;
            }
            else
            {
                GlobalControl.Instance.inventory.OnItemListChanged -= Inventory_OnItemListChanged;
            }
        }
        

        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {


            if (canCollide)
            {
                canCollide = false;
                StartCoroutine(next());
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
       
    }
    public void Remove(Objective objective)
    {
        desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
        name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
        Objectives.Remove(objective);
        
    }
    public IEnumerator next()
   {
        GlobalControl.Instance.currentobj = Objectives[i].name;
        if (Objectives[i].useString)
        {
            i5++;
            GlobalControl.Instance.objindex5 = i5;
            if (Objectives[i].PrefabNames.Count > i5){
                
                gm = GameObject.Find(Objectives[i].PrefabNames[i5]);
                if (gm != null)
                {
                    arrow.gameObject.SetActive(true);

                    Objective = gm.transform.position;
                }
                yield return new WaitForSeconds(1f);
                canCollide = true;
                
                yield break;
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
        i++;
        i5 = 0;
        GlobalControl.Instance.objindex5 = i5;
        Objectives[i-1].IsCompleted = true;
        
        GlobalControl.Instance.obtosv.Add(Objectives[i - 1].ID);
        Objectives.Remove(Objectives[i - 1]);
        
            if (Objectives.Count > i)
            {

           if (dataStatic.Instance.objsaved.GetLength(0) <= i || dataStatic.Instance.objsaved[i] != Objectives[i].ID)
            {
                GlobalControl.Instance.currentobj = Objectives[i].name;
                desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = Objectives[i].desc;
                name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = Objectives[i].objname;
                desc.GetComponent<dynamicLocale>().StringReference.RefreshString();
                name.GetComponent<dynamicLocale>().StringReference.RefreshString();
                if (Objectives[i].InMap)
                {
                    if (Objectives[i].useString)
                    {
                        gm = GameObject.Find(Objectives[i].PrefabNames[i5]);
                        if (gm != null)
                        {
                            arrow.gameObject.SetActive(true);

                            Objective = gm.transform.position;
                        }
                    }
                    else
                    {
                        gm = Objectives[i].Prefabs[i5];
                        if (gm != null)
                        {
                            arrow.gameObject.SetActive(true);
                            Objective = Objectives[i].Prefabs[i5].transform.position;
                        }
                    }
                }
                else
                {
                    arrow.gameObject.SetActive(false);
                }
                if (Objectives[i].checkforitem)
                {
                    GlobalControl.Instance.inventory.OnItemListChanged += Inventory_OnItemListChanged;
                }
                else
                {
                    GlobalControl.Instance.inventory.OnItemListChanged -= Inventory_OnItemListChanged;
                }
            }
            else
            {
                desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
                name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
                arrow.gameObject.SetActive(false);
            }
           

            }
            else
            {
                desc.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
                name.GetComponent<dynamicLocale>().StringReference.TableEntryReference = "none";
            arrow.gameObject.SetActive(false);
            GlobalControl.Instance.currentobj = "";
        }

       

        yield return new WaitForSeconds(1f);
        canCollide = true;
    }
}
