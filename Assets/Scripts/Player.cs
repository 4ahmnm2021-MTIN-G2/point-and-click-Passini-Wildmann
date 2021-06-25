using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManagement {
    public Canvas inventoryCanvas{get;set;}
    public Canvas stockCanvas { get; set; }
    public Canvas actionContextCanvas { get; set; }
    public Canvas recipeCanvas{get;set;}

    // Cooker Canvas objects
    public Canvas cookerCanvas{get; set;}
    public Canvas cookerSpaghettiCanvas{get; set;}
}

public class GameObjectManagement {
    public GameObject itemStorageObject{get; set;}
    public GameObject cookerGameObject { get; set; }
    public Text actionContextText { get; set; }
    
    // Cooker Spaghetti Text objects
    public Text cookerSpaghettiFaschiertes {get; set;}
    public Text cookerSpaghettiTomaten {get; set;}
    public Text cookerSpaghettiPasta {get; set;}
    public Text cookerSpaghettiOlivenöl {get; set;}
    public Text cookerSpaghettiFinish {get; set;}

    public SpriteRenderer bologneseCookedObject {get; set;}
}

public class PlayerInventory {
    public List<InventoryObject> inventory{get; set;}
        public PlayerInventory() {
        this.inventory = new List<InventoryObject>();
    }
    public void addInventoryObject(InventoryObject item) {
        this.inventory.Add(item);
    }
}

public class InventoryObject
{
    public string Name {get; set;}
    public int Amount {get; set;}
    public InventoryObject(string name, int amount) {
        Name = name;
        Amount = amount;
    }
}

public class ItemStore {

}

public class Player : MonoBehaviour
{
    public Vector3 mPosition;
    public Vector3 playerPos;
    public float speed = 0.1f;

    bool bologneseCooked = false;

    PlayerInventory playerInventory = new PlayerInventory();

    CanvasManagement canvasManagement;
    GameObjectManagement GameObjectManagement;

    void Start()
    {
        this.canvasManagement = new CanvasManagement();
        this.GameObjectManagement = new GameObjectManagement();

        this.canvasManagement.inventoryCanvas = GameObject.Find("PlayerInventory").GetComponent<Canvas>();
        this.canvasManagement.stockCanvas = GameObject.Find("Stock").GetComponent<Canvas>();
        this.canvasManagement.actionContextCanvas = GameObject.Find("ActionContext").GetComponent<Canvas>();
        this.canvasManagement.recipeCanvas = GameObject.Find("RecipeCanvas").GetComponent<Canvas>();

        this.GameObjectManagement.itemStorageObject = GameObject.Find("item-storage");

        this.GameObjectManagement.cookerGameObject = GameObject.Find("cooker");

        this.canvasManagement.cookerCanvas = GameObject.Find("CookerCanvas").GetComponent<Canvas>();

        // Initialize text objects for spaghetti inside CookerCanvas
        this.canvasManagement.cookerSpaghettiCanvas = GameObject.Find("cooker-spaghetti").GetComponent<Canvas>();

        this.GameObjectManagement.cookerSpaghettiFaschiertes = GameObject.Find("cooker.spaghetti.ingredient.faschiertes").GetComponent<Text>();
        this.GameObjectManagement.cookerSpaghettiTomaten = GameObject.Find("cooker.spaghetti.ingredient.tomaten").GetComponent<Text>();
        this.GameObjectManagement.cookerSpaghettiPasta = GameObject.Find("cooker.spaghetti.ingredient.pasta").GetComponent<Text>();
        this.GameObjectManagement.cookerSpaghettiOlivenöl = GameObject.Find("cooker.spaghetti.ingredient.olivenöl").GetComponent<Text>();

        this.GameObjectManagement.cookerSpaghettiFinish = GameObject.Find("cooker.spaghetti.finish").GetComponent<Text>();
        // this.GameObjectManagement.cookerSpaghettiFinish.enabled = false;

        this.GameObjectManagement.bologneseCookedObject = GameObject.Find("BologneseObject").GetComponent<SpriteRenderer>();
        this.GameObjectManagement.bologneseCookedObject.enabled = false;

        this.GameObjectManagement.actionContextText = GameObject.Find("action-context-text").GetComponent<Text>();

        this.playerPos = this.transform.position;

        // playerInventory.inventory.Add(new InventoryObject("Apple", 1));

        StartCoroutine(this.checkItemStorageAvailable());
        StartCoroutine(this.checkCookerAvailable());
        StartCoroutine(this.checkCookerSpaghettiIngredients());

        GameObject text = new GameObject("player-inventory-item:" + this.playerInventory.inventory[0].Name, typeof(RectTransform));
        var newTextComp = text.AddComponent<Text>();
        // text.text = this.playerInventory.inventory[0].Name;
        // this.canvasManagement.inventoryCanvas.AddComponent(text);

        Debug.Log(this.playerInventory.inventory);
    }
    void Update()
    { 

        /*if(Input.GetKeyDown("e")) {
            if(this.canvasManagement.recipeCanvas.enabled) {
                this.canvasManagement.recipeCanvas.enabled = !this.canvasManagement.recipeCanvas.enabled;
            }
            this.canvasManagement.inventoryCanvas.enabled = !this.canvasManagement.inventoryCanvas.enabled;
        }*/
        if(Input.GetKeyDown(KeyCode.Escape)) {
            this.canvasManagement.inventoryCanvas.enabled = false;
            this.canvasManagement.stockCanvas.enabled = false;
            this.canvasManagement.cookerCanvas.enabled = false;
        }
        if(Input.GetKeyDown("i")) {
            if(this.canvasManagement.inventoryCanvas.enabled) {
                this.canvasManagement.inventoryCanvas.enabled = !this.canvasManagement.inventoryCanvas.enabled;
            }
            this.canvasManagement.recipeCanvas.enabled = !this.canvasManagement.recipeCanvas.enabled;
        }
        if (Input.GetKeyDown("w"))
        {
            if (Vector3.Distance(this.transform.position, GameObjectManagement.itemStorageObject.transform.position) < 1.7)
            {
                this.canvasManagement.stockCanvas.enabled = !this.canvasManagement.stockCanvas.enabled;
            }
        }
        if (Input.GetKeyDown("c"))
        {
            if (Vector3.Distance(this.transform.position, GameObjectManagement.cookerGameObject.transform.position) < 1.7)
            {
                this.canvasManagement.cookerCanvas.enabled = !this.canvasManagement.cookerCanvas.enabled;
            }
        }
        // cook bolognese listener
        if(Input.GetKeyDown("f")) {
            if(this.canvasManagement.cookerCanvas.enabled) {
                this.cookBolognese();
            }
        }
        // Stock to inventory
        if (Input.GetKeyDown("1")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Apfel", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("2")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Butter", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("3")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Eier", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("4")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Faschiertes", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("5")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Fleisch", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("6")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Kartoffel", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("7")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Mehl", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("8")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Milch", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("9")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Olivenöl", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("a")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Parmesan", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("b")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Pasta", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("c")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Semmelbrösel", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("d")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Birne", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("e")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Tomaten", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("f")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Wiener Schnitzel", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }
        if (Input.GetKeyDown("g")) {
            if(this.canvasManagement.stockCanvas.enabled) {
                playerInventory.inventory.Add(new InventoryObject("Zucker", 1));
                Debug.Log(this.playerInventory.inventory);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mPosition = Input.mousePosition;
            
            Ray ray = Camera.main.ScreenPointToRay(mPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                playerPos = hit.point;
                playerPos.y = 1.467f;
            }
        }
        if (Input.GetMouseButton(0))
        {

        }
            ;
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed);
	    
    }

    void cookBolognese() {
        List<string> keywords = new List<string>();
        foreach(InventoryObject invObject in this.playerInventory.inventory) {
            if(invObject.Name.Equals("Faschiertes")) {
                keywords.Add("Faschiertes");
            }
            if(invObject.Name.Equals("Tomaten")) {
                keywords.Add("Tomaten");
            }
            if(invObject.Name.Equals("Pasta")) {
                keywords.Add("Pasta");
            }
            if(invObject.Name.Equals("Olivenöl")) {
                keywords.Add("Olivenöl");
            }               
        }
        if(keywords.Contains("Faschiertes") &&
            keywords.Contains("Tomaten") &&
            keywords.Contains("Pasta") &&
            keywords.Contains("Olivenöl")) {
                this.canvasManagement.cookerSpaghettiCanvas.enabled = false;
                this.canvasManagement.cookerCanvas.enabled = false;
                this.bologneseCooked = true;
                this.GameObjectManagement.bologneseCookedObject.enabled = true;
            }
    }

    IEnumerator checkCookerSpaghettiIngredients() {
        while(true) {
            foreach(InventoryObject invObject in this.playerInventory.inventory) {
                if(invObject.Name.Equals("Faschiertes")) {
                    this.GameObjectManagement.cookerSpaghettiFaschiertes.enabled = false;
                }
                if(invObject.Name.Equals("Tomaten")) {
                    this.GameObjectManagement.cookerSpaghettiTomaten.enabled = false;
                }
                if(invObject.Name.Equals("Pasta")) {
                    this.GameObjectManagement.cookerSpaghettiPasta.enabled = false;
                }
                if(invObject.Name.Equals("Olivenöl")) {
                    this.GameObjectManagement.cookerSpaghettiOlivenöl.enabled = false;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator checkItemStorageAvailable() {
            while(true) {
                var playerDistanceToItemStorage = Vector3.Distance(this.transform.position, GameObjectManagement.itemStorageObject.transform.position);
                if(playerDistanceToItemStorage < 1.7)
            {
                this.canvasManagement.actionContextCanvas.enabled = true;
                this.GameObjectManagement.actionContextText.text = "Press [W] to open stock";
            } else
            {
                this.canvasManagement.actionContextCanvas.enabled = false;
            }
                Debug.Log(playerDistanceToItemStorage);
                yield return new WaitForSeconds(1f);
            }
        }

    IEnumerator checkCookerAvailable()
    {
        while (true)
        {
            var playerDistanceToCooker = Vector3.Distance(this.transform.position, GameObjectManagement.cookerGameObject.transform.position);
            if (playerDistanceToCooker < 1.7)
            {
                this.canvasManagement.actionContextCanvas.enabled = true;
                this.GameObjectManagement.actionContextText.text = "Press [C] to open cooker";
            }
            else
            {
                if(Vector3.Distance(this.transform.position, GameObjectManagement.itemStorageObject.transform.position) > 1.7) {
                    this.canvasManagement.actionContextCanvas.enabled = false;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
