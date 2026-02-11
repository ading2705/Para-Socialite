using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public Vector3 LastPosition;
    // Original spawn line position used when cancel (X/No) is pressed.
    public Vector3 InitialPosition { get; private set; }

    // state for confirm/cancel flow
    public bool IsPlacementLocked { get; private set; }
    public bool IsAwaitingConfirmation { get; private set; }

    private Collider2D _collider;
    private DragController _dragController;
    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestination;

    // Track whether latest trigger was a valid tile so we can ask for confirmation once.
    private bool _pendingValidDrop;
    // While returning to spawn line, ignore tile triggers to avoid re-snapping mid-return.
    private bool _ignoreDropTriggers;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragController = FindObjectOfType<DragController>();
        // Capture initial turret line as the authoritative cancel destination.
        InitialPosition = transform.position;
        LastPosition = InitialPosition;
    }

    void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            // While actively dragging, pointer movement controls transform directly.
            if (IsDragging)
                return;

            // After release, ease to snap/return destination.
            transform.position = Vector3.Lerp(
                transform.position,
                _movementDestination.Value,
                _movementTime * Time.fixedDeltaTime
            );

            // Arrived at destination (distance check avoids float equality issues).
            if (Vector3.Distance(transform.position, _movementDestination.Value) < 0.01f)
            {
                transform.position = _movementDestination.Value;
                gameObject.layer = Layer.Default;
                _movementDestination = null;
                // Re-enable trigger processing after return movement ends.
                _ignoreDropTriggers = false;

                // Valid tile drop: request Yes/No once.
                if (_pendingValidDrop && !IsPlacementLocked && !IsAwaitingConfirmation)
                {
                    IsAwaitingConfirmation = true;
                    _pendingValidDrop = false;

                    if (_dragController != null)
                        _dragController.RequestPlacementConfirmation(this);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Draggable collidedDraggable = other.GetComponent<Draggable>();

        // Push overlapping draggable objects apart while this object is the active drag target.
        if (collidedDraggable != null &&
            _dragController != null &&
            _dragController.LastDraggable != null &&
            _dragController.LastDraggable.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }

        // Locked turrets no longer participate in placement snapping.
        if (IsPlacementLocked)
            return;

        // During cancel-return to spawn line, ignore tile triggers to prevent interruption.
        if (_ignoreDropTriggers)
            return;

        if (other.CompareTag("Drop Valid"))
        {
            // Valid tile: move to tile center, then ask for confirmation.
            _movementDestination = other.transform.position;
            _pendingValidDrop = true;
        }
        else if (other.CompareTag("Drop Invalid"))
        {
            // Invalid tile: bounce back to last pre-drag position.
            _movementDestination = LastPosition;
            _pendingValidDrop = false;
        }
    }

    // Called by Yes/confirm button.
    public void ConfirmPlacement()
    {
        IsAwaitingConfirmation = false;
        IsPlacementLocked = true;

        _ignoreDropTriggers = false;
        _pendingValidDrop = false;
        _movementDestination = null;
    }

    // Called by No/X/cancel button.
    public void CancelPlacement()
    {
        IsAwaitingConfirmation = false;
        IsPlacementLocked = false;

        // Return to original spawn line location, not last dragged tile.
        _ignoreDropTriggers = true;
        _pendingValidDrop = false;
        _movementDestination = InitialPosition;
    }
}
