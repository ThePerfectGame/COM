public class DoorStorage : DoorLocationObject
{
    protected override void Start()
    {
        isLocked = GameController.Instance.Values.restoredGenerators < 2;
        base.Start();
    }
}
