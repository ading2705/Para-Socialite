using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragController : MonoBehaviour
{
    public Draggable LastDraggable => _lastDragged;
    // Keep exactly one active controller even if the scene has duplicates.
    private static DragController _instance;

    // TODO: Remove these offsets once the panel is anchored properly.
    // Right now, without offsets, the confirmation UI shifts whenever a button is clicked.

    [Header("Placement Confirmation UI")]
    [SerializeField] private GameObject _confirmPanel;   // Panel that contains confirm/cancel buttons
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Vector2 _confirmPanelOffset = new Vector2(0f, 84f);
    [SerializeField] private Vector2 _confirmButtonOffset = new Vector2(52f, 0f);
    [SerializeField] private Vector2 _cancelButtonOffset = new Vector2(-52f, 0f);
    [SerializeField, Min(0f)] private float _confirmPanelMargin = 20f;
    [SerializeField] private bool _pinConfirmPanelToBottomRight = true;
    [SerializeField] private Vector2 _bottomRightPadding = new Vector2(24f, 24f);

    [SerializeField] private PlayerHealth _playerHealth;


    private bool _isDragActive = false;
    private bool _isConfirmUIActive = false;

    private Vector2 _screenPosition;
    private Vector3 _worldPosition;

    private Draggable _lastDragged;
    private Draggable _pendingConfirmDraggable;

    private RectTransform _confirmPanelRect;
    private RectTransform _confirmButtonRect;
    private RectTransform _cancelButtonRect;
    private Canvas _confirmCanvas;

    void Awake()
    {
        // Destroy duplicates so drag input is handled by one controller only.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        CacheConfirmUIRefs();
        NormalizeConfirmButtonLayout();
        HideConfirmUI();
    }

    void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    void Update()
    {
        // While confirm/cancel is open, pause drag input.
        if (_isConfirmUIActive)
        {
            // Failsafe so the game can't get stuck if confirmation UI becomes unreachable.
            if (Input.GetKeyDown(KeyCode.Escape))
                OnCancelPressed();

            return;
        }

        // End drag on mouse/touch release.
        if (_isDragActive)
        {
            bool mouseUp = Input.GetMouseButtonUp(0);
            bool touchEnded = (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended);

            if (mouseUp || touchEnded)
            {
                Drop();
                return;
            }
        }

        // Read current pointer from mouse or first touch.
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return;

        _worldPosition = mainCamera.ScreenToWorldPoint(_screenPosition);
        _worldPosition.z = 0f;

        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            // Start drag when pointer is over a draggable object.
            Collider2D hitCollider = Physics2D.OverlapPoint(_worldPosition);
            if (hitCollider != null)
            {
                Draggable draggable = hitCollider.GetComponent<Draggable>();
                if (draggable != null && !draggable.IsPlacementLocked && !draggable.IsAwaitingConfirmation)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        // Store latest pre-drag position used by invalid-drop fallback logic.
        _lastDragged.LastPosition = _lastDragged.transform.position;
        UpdateDragStatus(true);
    }

    void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    void Drop()
    {
        UpdateDragStatus(false);
        // Draggable handles snapping and then calls back for confirm/cancel when needed.
    }

    void UpdateDragStatus(bool isDragging)
    {
        if (_lastDragged == null)
        {
            _isDragActive = false;
            return;
        }

        _isDragActive = _lastDragged.IsDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }

    
    // Called by Draggable when it finishes snapping onto a valid tile
    
    public void RequestPlacementConfirmation(Draggable draggable)
    {
        if (draggable == null) return;

        _pendingConfirmDraggable = draggable;

        // If UI refs are missing, resolve immediately so gameplay does not deadlock.
        if (_confirmPanel == null || _confirmButton == null || _cancelButton == null)
        {
            ResolvePlacementWithoutUI();
            return;
        }

        _isConfirmUIActive = true;

        ShowConfirmUIAt(draggable.transform.position);

        // Rebind listeners each time so repeated placements do not stack callbacks.
        _confirmButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();

        _confirmButton.onClick.AddListener(OnConfirmPressed);
        _cancelButton.onClick.AddListener(OnCancelPressed);
    }

    void OnConfirmPressed()
    {
        if (_pendingConfirmDraggable == null)
        {
            HideConfirmUI();
            return;
        }

        // Confirm spends health cost (if health system exists), then locks placement.
        int cost = 0;
        TurretBehavior turret = _pendingConfirmDraggable.GetComponent<TurretBehavior>();
        if (turret != null) cost = turret.HealthCost;

        // If no health script assigned, allow placement (or you can block it)
        if (_playerHealth != null)
        {
            if (!_playerHealth.SpendHealth(cost))
            {
                // Not enough HP -> cancel placement
                _pendingConfirmDraggable.CancelPlacement();
                HideConfirmUI();
                _pendingConfirmDraggable = null;
                _lastDragged = null;
                return;
            }
        }

        _pendingConfirmDraggable.ConfirmPlacement();
        HideConfirmUI();
        _pendingConfirmDraggable = null;
        _lastDragged = null;

    }

    void OnCancelPressed()
    {
        // Cancel always sends the draggable back via Draggable.CancelPlacement().
        if (_pendingConfirmDraggable != null)
        {
            _pendingConfirmDraggable.CancelPlacement();
        }

        HideConfirmUI();

        _pendingConfirmDraggable = null;
        _lastDragged = null;
    }

    void ShowConfirmUIAt(Vector3 worldPos)
    {
        if (_confirmPanel == null) return;

        _confirmPanel.SetActive(true);
        CacheConfirmUIRefs();
        // Normalize panel/button anchors so old scene offsets do not accumulate.
        NormalizeConfirmButtonLayout();

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return;

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, worldPos) + _confirmPanelOffset;

        if (_confirmPanelRect != null && _confirmCanvas != null)
        {
            RectTransform canvasRect = _confirmCanvas.transform as RectTransform;

            //Keep confirm UI fixed in bottom-right instead of near turret.
            if (_pinConfirmPanelToBottomRight && canvasRect != null)
            {
                Vector2 halfBounds = GetConfirmUIHalfExtents();
                Vector2 min = canvasRect.rect.min + halfBounds + new Vector2(_confirmPanelMargin, _confirmPanelMargin);
                Vector2 max = canvasRect.rect.max - halfBounds - new Vector2(_confirmPanelMargin, _confirmPanelMargin);
                Vector2 bottomRight = new Vector2(max.x - _bottomRightPadding.x, min.y + _bottomRightPadding.y);

                bottomRight.x = Mathf.Clamp(bottomRight.x, min.x, max.x);
                bottomRight.y = Mathf.Clamp(bottomRight.y, min.y, max.y);

                _confirmPanelRect.anchoredPosition = bottomRight;
                return;
            }

            Camera uiCamera = _confirmCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : (_confirmCanvas.worldCamera != null ? _confirmCanvas.worldCamera : mainCamera);

            if (canvasRect != null &&
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out Vector2 localPoint))
            {
                // Default mode: show near turret but clamp fully inside visible canvas area.
                Vector2 halfBounds = GetConfirmUIHalfExtents();
                Vector2 min = canvasRect.rect.min + halfBounds + new Vector2(_confirmPanelMargin, _confirmPanelMargin);
                Vector2 max = canvasRect.rect.max - halfBounds - new Vector2(_confirmPanelMargin, _confirmPanelMargin);

                localPoint.x = Mathf.Clamp(localPoint.x, min.x, max.x);
                localPoint.y = Mathf.Clamp(localPoint.y, min.y, max.y);

                _confirmPanelRect.anchoredPosition = localPoint;
                return;
            }
        }

        // Fallback when canvas-space conversion fails: clamp in raw screen pixels.
        Vector2 halfBoundsFallback = GetConfirmUIHalfExtents();
        screenPos.x = Mathf.Clamp(
            screenPos.x,
            halfBoundsFallback.x + _confirmPanelMargin,
            Screen.width - halfBoundsFallback.x - _confirmPanelMargin
        );
        screenPos.y = Mathf.Clamp(
            screenPos.y,
            halfBoundsFallback.y + _confirmPanelMargin,
            Screen.height - halfBoundsFallback.y - _confirmPanelMargin
        );
        _confirmPanel.transform.position = screenPos;
    }

    void HideConfirmUI()
    {
        _isConfirmUIActive = false;

        if (_confirmPanel != null)
            _confirmPanel.SetActive(false);

        // Confirm/cancel handlers own pending-drag cleanup.
    }

    void ResolvePlacementWithoutUI()
    {
        if (_pendingConfirmDraggable == null)
            return;

        // Headless fallback: apply same health and placement rules without opening UI.
        int cost = 0;
        TurretBehavior turret = _pendingConfirmDraggable.GetComponent<TurretBehavior>();
        if (turret != null) cost = turret.HealthCost;

        if (_playerHealth != null && !_playerHealth.SpendHealth(cost))
        {
            _pendingConfirmDraggable.CancelPlacement();
        }
        else
        {
            _pendingConfirmDraggable.ConfirmPlacement();
        }

        _isConfirmUIActive = false;
        _pendingConfirmDraggable = null;
        _lastDragged = null;
    }

    void CacheConfirmUIRefs()
    {
        // Cache rect/canvas references once per show so positioning math is fast and safe.
        if (_confirmPanel != null)
        {
            _confirmPanelRect = _confirmPanel.GetComponent<RectTransform>();
            _confirmCanvas = _confirmPanel.GetComponentInParent<Canvas>();
        }

        if (_confirmButton != null)
            _confirmButtonRect = _confirmButton.GetComponent<RectTransform>();

        if (_cancelButton != null)
            _cancelButtonRect = _cancelButton.GetComponent<RectTransform>();
    }

    void NormalizeConfirmButtonLayout()
    {
        // Enforce a stable local layout regardless of where scene editing left the rects.
        if (_confirmPanelRect != null)
        {
            _confirmPanelRect.anchorMin = _confirmPanelRect.anchorMax = new Vector2(0.5f, 0.5f);
            _confirmPanelRect.pivot = new Vector2(0.5f, 0.5f);
        }

        if (_confirmButtonRect != null)
        {
            _confirmButtonRect.anchorMin = _confirmButtonRect.anchorMax = new Vector2(0.5f, 0.5f);
            _confirmButtonRect.pivot = new Vector2(0.5f, 0.5f);
            _confirmButtonRect.anchoredPosition = _confirmButtonOffset;
        }

        if (_cancelButtonRect != null)
        {
            _cancelButtonRect.anchorMin = _cancelButtonRect.anchorMax = new Vector2(0.5f, 0.5f);
            _cancelButtonRect.pivot = new Vector2(0.5f, 0.5f);
            _cancelButtonRect.anchoredPosition = _cancelButtonOffset;
        }
    }

    Vector2 GetConfirmUIHalfExtents()
    {
        // Compute the actual panel footprint including button offsets for safe clamping.
        Vector2 halfExtents = _confirmPanelRect != null
            ? _confirmPanelRect.rect.size * 0.5f
            : new Vector2(80f, 50f);

        if (_confirmButtonRect != null)
        {
            Vector2 confirmHalf = _confirmButtonRect.rect.size * 0.5f;
            halfExtents.x = Mathf.Max(halfExtents.x, Mathf.Abs(_confirmButtonRect.anchoredPosition.x) + confirmHalf.x);
            halfExtents.y = Mathf.Max(halfExtents.y, Mathf.Abs(_confirmButtonRect.anchoredPosition.y) + confirmHalf.y);
        }

        if (_cancelButtonRect != null)
        {
            Vector2 cancelHalf = _cancelButtonRect.rect.size * 0.5f;
            halfExtents.x = Mathf.Max(halfExtents.x, Mathf.Abs(_cancelButtonRect.anchoredPosition.x) + cancelHalf.x);
            halfExtents.y = Mathf.Max(halfExtents.y, Mathf.Abs(_cancelButtonRect.anchoredPosition.y) + cancelHalf.y);
        }

        return halfExtents;
    }
}
