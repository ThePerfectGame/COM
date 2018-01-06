public class Safe : ContainerObject
{
    public bool isLocked = true;
    public string password = "1111";

    public override void Use()
    {
        if (isLocked == false)
        {
            base.Use();
        }
        else
        {
            UIController.Instance.ShowPasswordScreen(this);
        }
    }

    public void Unlock()
    {
        isLocked = false;
        base.Use();
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = base.Save();
        saveInfo.WriteProperty("isLocked", isLocked);
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        base.Load(saveInfo);
        isLocked = saveInfo.ReadProperty<bool>("isLocked");
    }
}
