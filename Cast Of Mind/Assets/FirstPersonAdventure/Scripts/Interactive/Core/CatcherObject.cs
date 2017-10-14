using UnityEngine;
using UnityEngine.Events;

public class CatcherObject : Saveable
{
    public DragObject[] catchObjects;
    public GameObject catchPoint;
    public float catchRadius = 1;

    public UnityEvent onCatch;

    protected bool isCaught = false;

    void Update()
    {
        if (isCaught == false)
        {
            foreach (DragObject catchObject in catchObjects)
            {
                if (Vector3.Distance(catchObject.transform.position, catchPoint.transform.position) < catchRadius)
                {
                    if (PlayerController.Instance.DraggableObject == catchObject) PlayerController.Instance.DropDragObject();
                    catchObject.Catch(catchPoint);
                    isCaught = true;
                    onCatch.Invoke();
                }
            }
        }
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.WriteProperty("isCaught", isCaught);
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        isCaught = saveInfo.ReadProperty<bool>("isCaught");
    }
}
