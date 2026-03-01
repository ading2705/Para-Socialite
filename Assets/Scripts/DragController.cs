using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private LayerMask _draggableLayer; // added this line to try and fix turret dragging
    [SerializeField] public ConfirmationWindow myConfirmationWindow;
    public GameObject deactivateSpawner1;
    public GameObject deactivateSpawner2;
    public GameObject deactivateSpawner3;
    public Draggable LastDraggable => _lastDragged;
    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;
    private StateManager SanityValue;

    // Start is called before the first frame update
    void Start()
    {
         GameObject stateManager = GameObject.FindWithTag("HealthBar");
         SanityValue = stateManager.GetComponent<StateManager>();
    }
    
    void Awake(){
      DragController[] controllers = FindObjectsOfType<DragController>();
      if(controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
             if(_isDragActive)
        {
            if (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
            Drop();
            OpenConfirmationWindow("Are you sure?");
            //play drop SFX
            return;
            }
        }
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                _screenPosition = new Vector2(mousePos.x, mousePos.y);
            }
            else if(Input.touchCount > 0){
              _screenPosition = Input.GetTouch(0).position;
            }
            else{
             return;
            } 

            _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if (_isDragActive)
        {
            Drag();
        }
        else
        {
          Collider2D hit = Physics2D.OverlapPoint(_worldPosition); // aaaaaaaaaaaa im losing it
          if (hit != null)
            {
                //Debug.Log("Hit: " + hit.GetComponent<Collider2D>().name); // debugging not dragging........ 
                // this fuckin DEBUG LINE was the source of some errors fml, tryin a diff one now
                Draggable draggable = hit.GetComponentInParent<Draggable>(); // if the in parent thing fixes this all i stg
                //Debug.Log("Draggable detected: " + draggable.name); // WHY IS DRAGGABLE NULL????????????????
                if(draggable != null)
                {
                    Debug.Log("Draggable detected: " + draggable.name); // sobbing rn
                    _lastDragged = draggable;
                    InitDrag();
                }
            }  
        }
    }
    void InitDrag(){

     _lastDragged.LastPosition = _lastDragged.transform.position;   
     UpdateDragStatus(true);

    }

    void Drag(){
     _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);

    }

    void Drop(){
      UpdateDragStatus(false);

    }

 void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.IsDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }

    public void OpenConfirmationWindow(string message)
    {
        deactivateSpawner1.SetActive(false);
        deactivateSpawner2.SetActive(false);
        deactivateSpawner3.SetActive(false);
        myConfirmationWindow.gameObject.SetActive(true);
        myConfirmationWindow.yesButton.onClick.AddListener(YesClicked);
        myConfirmationWindow.noButton.onClick.AddListener(NoClicked);
    }

    public void YesClicked()
    {
        myConfirmationWindow.gameObject.SetActive(false);
        Destroy(LastDraggable);
        deactivateSpawner1.SetActive(true);
        deactivateSpawner2.SetActive(true);
        deactivateSpawner3.SetActive(true);
        SanityValue.SpendSanity(2);

        
    }

     public void NoClicked()
    {
        myConfirmationWindow.gameObject.SetActive(false);
        deactivateSpawner1.SetActive(true);
        deactivateSpawner2.SetActive(true);
        deactivateSpawner3.SetActive(true);
        Destroy(_lastDragged.gameObject);

        
    }

}