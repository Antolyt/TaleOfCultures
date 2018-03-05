using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Targeter targeter;
    public GameObject plants;

    #region move in grid
    public bool moveInGrid;             // true if player should move in grid
    private float moveGridTimeStamp;         // time when player arrives in new cell center
    public float moveGridWaitingTime;   // time player waits until contiues to move to next cell
    private Vector3 desiredGridPos;    // cell where player is currently when arrives in new cell
    #endregion

    #region action
    public float actionWaitingTime;
    public float actionTimeStamp;
    #endregion

    Vector3 viewingDirection;
    public Inventory inventory;
    public QuickSlots quickSlots;
    public GameObject itemInHand;

    // Use this for initialization
    void Start () {
        viewingDirection = Vector3.down;
	}

    private void Update()
    {
        // save
        if (Input.GetKeyDown(KeyCode.J))
        {
            Savegame.Save(plants, inventory, quickSlots);
        }

        // plant an item from quickSLot
        if (Input.GetButtonDown("UseItem"))
        {
            if (!targeter.target && targeter.IsInField())
            {
                InventoryItem inventoryItem = quickSlots.GetSelectedItem();
                if (inventoryItem != null)
                {
                    string itemName = inventoryItem.item.name;
                    GameObject plantToPlace = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + itemName));
                    plantToPlace.name = itemName + Time.time;
                    plantToPlace.transform.parent = plants.transform;

                    plantToPlace.transform.position = targeter.transform.position;
                    plantToPlace.GetComponent<SpriteRenderer>().sortingOrder = -(int)plantToPlace.transform.position.y;

                    quickSlots.RemoveSelectedItem();

                    targeter.AddTarget(plantToPlace);
                    actionTimeStamp = Time.time;
                }
            }
        }

        // remove fruit in target
        if (Input.GetButtonDown("Submit"))
        {
            if (targeter.target)
            {
                Plant targetPlant = targeter.target.GetComponent<Plant>();
                if(targetPlant.yield > 0)
                {
                    Item item = targetPlant.droppedFruit;
                    inventory.AddItem(item, targetPlant.yield);
                    Destroy(targeter.target);
                    actionTimeStamp = Time.time;
                }
            }
        }

        if (Input.GetButtonDown("Inventory"))
        {
            inventory.Open();
        }

        #region QuickSlot
        // selects a quickSlot
        if (Input.GetButtonDown("QuickSlotUp"))
        {
            quickSlots.SelectQuickSlot(0);
        }
        if (Input.GetButtonDown("QuickSlotRight"))
        {
            quickSlots.SelectQuickSlot(1);
        }
        if (Input.GetButtonDown("QuickSlotDown"))
        {
            quickSlots.SelectQuickSlot(2);
        }
        if (Input.GetButtonDown("QuickSlotLeft"))
        {
            quickSlots.SelectQuickSlot(3);
        }
        #endregion

        // handle grid navigation
        if (!moveInGrid)
        {
            // set animation
            if (Input.GetKey(KeyCode.D))
            {
                viewingDirection = Vector3.right;
                animator.SetBool("moveRight", true);
                animator.SetBool("moveLeft", false);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                viewingDirection = Vector3.left;
                animator.SetBool("moveRight", false);
                animator.SetBool("moveLeft", true);
            }
            else
            {
                animator.SetBool("moveRight", false);
                animator.SetBool("moveLeft", false);
            }

            if (Input.GetKey(KeyCode.W))
            {
                viewingDirection = Vector3.up;
                animator.SetBool("moveUp", true);
                animator.SetBool("moveDown", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                viewingDirection = Vector3.down;
                animator.SetBool("moveUp", false);
                animator.SetBool("moveDown", true);
            }
            else
            {
                animator.SetBool("moveUp", false);
                animator.SetBool("moveDown", false);
            }

            // switch to grid based movement
            if (Input.GetButtonDown("GridMovement"))
            {
                moveInGrid = true;
                float roundedX = Mathf.Round(transform.position.x);
                float roundedY = Mathf.Round(transform.position.y);
                desiredGridPos = new Vector3(roundedX, roundedY);

                animator.SetBool("moveUp", false);
                animator.SetBool("moveRight", false);
                animator.SetBool("moveDown", false);
                animator.SetBool("moveLeft", false);
            }
        }
        else
        {
            // go out of grid based movement
            if (Input.GetButtonDown("GridMovement"))
            {
                moveInGrid = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - actionWaitingTime > actionTimeStamp)
        {
            // set position update for grid Movement
            if (moveInGrid)
            {
                if (Time.time - moveGridWaitingTime > moveGridTimeStamp)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        desiredGridPos += Vector3.up;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        desiredGridPos += Vector3.left;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        desiredGridPos += Vector3.down;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        desiredGridPos += Vector3.right;
                    }
                }

                if (desiredGridPos != transform.position)
                {
                    if (Mathf.Abs(desiredGridPos.x - transform.position.x) < 0.02 && Mathf.Abs(desiredGridPos.y - transform.position.y) < 0.02)
                    {
                        transform.position = new Vector3(desiredGridPos.x, desiredGridPos.y);
                    }
                    moveGridTimeStamp = Time.time;
                }
                transform.position = Vector3.Lerp(transform.position, desiredGridPos, speed * 100 * Time.deltaTime);
            }
            // set position for non grid movement
            else
            {
                transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * speed), transform.position.y + (Input.GetAxis("Vertical") * speed));
            }

            // set layer of sprite, so that character will be displayed in front or after object depending on y position
            spriteRenderer.sortingOrder = -(int)Mathf.Round(transform.position.y) - 1;

            // set position of targeter
            targeter.transform.position = new Vector3(Mathf.Round(transform.position.x) + viewingDirection.x, Mathf.Round(transform.position.y) + viewingDirection.y);
        }
    }
}
