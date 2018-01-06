using System.Collections.Generic;
using UnityEngine.Events;

public class InteractAction
{
    public string nameAction;
    public UnityAction unityAction;

    public InteractAction(string nameAction, UnityAction unityAction)
    {
        this.nameAction = nameAction;
        this.unityAction = unityAction;
    }
}

public abstract class ActionsObject : Saveable, IUse
{
    public string objectName = "...";

    public string Name { get { return Localization.Get(objectName); } }
    public bool IsActive { get { return true; } }

    public void Use()
    {
        UIController.Instance.ShowActionsScreen(this);
    }

    public abstract List<InteractAction> GetActions();
}
