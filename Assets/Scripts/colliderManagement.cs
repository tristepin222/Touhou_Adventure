using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colliderManagement : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private GameObject canva;



    [SerializeField] PlayerManagament player;
    [SerializeField] private bool invert;
    [SerializeField] private Transform textException;
    [SerializeField] private ItemScriptableObject itemScriptableObject;
    [SerializeField] private int amount;
    [SerializeField] private int maxAmount;
    [SerializeField] private int amountToAdd;
    [SerializeField] private bool deleteWhenEmpty;
    [SerializeField] private Collider2D collider2DC;
    private Inventory selfIventory;
    private KeyCode e = KeyCode.E;
    private GameObject Interact;
    private Item item;
    private bool entered;
    private Canvas interactC;

    private void Awake()
    {
      
        if (player == null)
        {
            if (GlobalControl.Instance != null)
            {
                player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();
            }
        }
        if (textException == null)
        {
            if (GlobalControl.Instance != null)
            {
                textException = GlobalControl.Instance.ui_inventory.transform.Find("FullException");
            }
        }
        
        selfIventory = new Inventory();

        
    }

    void Start()
    {
       
      
        selfIventory = new Inventory();

        if (!invert)
        {
            for (int i = 0; i < amountToAdd; i++)
            {
                item = new Item { itemScriptableObject = this.itemScriptableObject, item = itemScriptableObject.itemType, amount = this.maxAmount, maxAmount = this.maxAmount, maxedOut = false };
                selfIventory.AddInventory(item);
            }
            
        }
    
        

    }


    private void OnTriggerEnter2D(Collider2D other) {
        
        if (player == null)
        {
            player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();
        }
        if (textException == null)
        {
            textException = GlobalControl.Instance.ui_inventory.transform.Find("FullException");
        }
        textException.gameObject.SetActive(false);

        if (Interact == null)
        {
            Interact = GlobalControl.Instance.ui_interact;

        }
        if (interactC == null)
        {
            interactC = Interact.GetComponent<Canvas>();
            interactC.enabled = false;
        }
        if (other.tag == "Player")
        {
            entered = true;
            interactC.enabled = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
        {
            entered = false;
            interactC.enabled = false;
        }
    }

   

    private void Update()
    {
       
        if (Input.GetKeyDown(e) && entered)
        {
            if (textException == null)
            {
                textException = GlobalControl.Instance.ui_inventory.transform.Find("FullException");
            }
            if (player == null)
            {
                player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();
            }
            if (Interact == null)
            {
                Interact = GlobalControl.Instance.ui_interact;
                interactC = Interact.GetComponent<Canvas>();
                interactC.enabled = false;
            }
            try
            {


                if (selfIventory.empty)
                {
                    throw new InventoryEmpty();
                }
                else
                {
                   
                    item = new Item { itemScriptableObject = this.itemScriptableObject, itemNumber = this.itemScriptableObject.itemNumber, item = itemScriptableObject.itemType, category = itemScriptableObject.category, name = itemScriptableObject.itemName, amount = this.amount, maxAmount = itemScriptableObject.maxAmount, maxedOut = false };
                    textException.gameObject.SetActive(false);
                    if(player.GetCollider2D().IsTouching(collider2DC)){
                 if(this.invert){
                        
                    player.GetInventory().RemoveInventory(item);
                    selfIventory.AddInventory(item);
                    this.invert = true;
                    }else{
                            if (selfIventory.empty != true)
                            {
                                selfIventory.RemoveInventory(item);
                                player.GetInventory().AddInventory(item);
                                this.invert = false;
                                if(deleteWhenEmpty == true)
                                {
                                    GameObject.Destroy(this.gameObject);
                                }
                            }

                    }
                        
                    }

                }
                

            }
            catch (InventoryFullException)
            {
                textException.gameObject.SetActive(true);

            }
            catch (InventoryEmpty)
            {

            }


        }
    }


}
