using UnityEngine;

public class InputController : Singleton<InputController>
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }
    public static float RotationHorizontal { get; private set; }
    public static float RotationVertical { get; private set; }

    public static bool Escape { get; private set; }
    public static bool Inventory { get; private set; }
    public static bool Notebook { get; private set; }
    public static bool Jump { get; private set; }
    public static bool Drag { get; private set; }
    public static bool Dragging { get; private set; }
    public static bool Throw { get; private set; }

    private static bool use;
    public static bool Use
    {
        get
        {
            if (use == true)
            {
                use = false;
                return true;
            }
            return use;
        }
    }

    void Update()
    {
        Horizontal = 0;
        Vertical = 0;
        if (Input.GetKey(KeyCode.W)) Vertical = 1;
        if (Input.GetKey(KeyCode.S)) Vertical = -1;
        if (Input.GetKey(KeyCode.A)) Horizontal = -1;
        if (Input.GetKey(KeyCode.D)) Horizontal = 1;
        RotationHorizontal = Input.GetAxis("Mouse X") * OptionsController.Instance.Config.mouseSence;
        RotationVertical = -Input.GetAxis("Mouse Y") * OptionsController.Instance.Config.mouseSence;
        if (OptionsController.Instance.Config.inverseMouseY) RotationVertical *= -1;

        use = Input.GetKeyDown(KeyCode.E);
        Escape = Input.GetKeyDown(KeyCode.Escape);
        Inventory = Input.GetKeyDown(KeyCode.Tab);
        Notebook = Input.GetKeyDown(KeyCode.J);
        Jump = Input.GetKeyDown(KeyCode.Space);
        Drag = Input.GetKeyDown(KeyCode.Mouse0);
        Dragging = Input.GetKey(KeyCode.Mouse0);
        Throw = Input.GetMouseButtonDown(1);
    }
}
