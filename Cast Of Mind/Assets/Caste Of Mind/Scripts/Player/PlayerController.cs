using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public static int MaxHealth = 100;

    public Camera mainCamera;
    public GameObject dragPoint;
    public float throwForce = 10f;

    public bool RadiationProtection { get; set; }
    public bool InRadiationZone { get; set; }
    public int Health { get; set; }

    public IUse InteractiveObject { get; private set; }
    public DragObject DraggableObject { get; private set; }
    public bool IsHoverDragObject { get; private set; }
    public float HitTime { get; private set; }

    void Start()
    {
        HitTime = Time.time - 5;
        if (Health == 0) Health = MaxHealth;
    }

    void Update()
    {
        if (GameController.Instance.GameState != GameStates.Game) return;

        // Search for interactive objects
        InteractiveObject = null;
        DragObject dragObject = null;
        IsHoverDragObject = false;
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 2.0f))
        {
            InteractiveObject = hit.collider.gameObject.GetComponent(typeof(IUse)) as IUse;
            if (InteractiveObject != null && InteractiveObject.IsActive == false) InteractiveObject = null;

            dragObject = hit.collider.GetComponent<DragObject>();
            if (dragObject != null && dragObject.Rigidbody.isKinematic) dragObject = null;
            IsHoverDragObject = dragObject != null;
        }
        if ((InteractiveObject != null) && (InputController.Use))
        {
            InteractiveObject.Use();
        }
        if (InteractiveObject == null && dragObject != null && DraggableObject == null && InputController.Drag)
        {
            dragObject.Drag(dragPoint);
            DraggableObject = dragObject;
        }
        if (DraggableObject != null && InputController.Dragging == false)
        {
            DraggableObject.Drop();
            DraggableObject = null;
        }
        if (DraggableObject != null && InputController.Throw)
        {
            DraggableObject.Throw(throwForce);
            DraggableObject = null;
        }
    }

    public void DropDragObject()
    {
        if (DraggableObject != null) DraggableObject.Drop();
        DraggableObject = null;
    }

    public void MoveToPoint(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void RestoreHealth()
    {
        Health = MaxHealth;
    }

    public void Hit(int damage)
    {
        if (Health <= 0) return;
        Health -= damage;
        Health = Mathf.Max(0, Health);
        SoundsController.Play("Hit");
        HitTime = Time.time;
        if (Health == 0) Death();
    }

    private void Death()
    {
        GameController.Instance.GameState = GameStates.Death;
    }
}
