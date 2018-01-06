using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragObject : SaveableTransform
{
    public Rigidbody Rigidbody { get; private set; }

    private Transform parent;
    private int layer;
    private bool isCatched = false;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        parent = transform.parent;
        layer = gameObject.layer;
    }

    public void Drag(GameObject root)
    {
        Rigidbody.isKinematic = true;
        Rigidbody.transform.parent = root.transform;
        Rigidbody.transform.localPosition = Vector3.zero;
        SetLayer(LayerMask.NameToLayer("Drag"));
    }

    public void Drop()
    {
        Rigidbody.isKinematic = false;
        Rigidbody.transform.parent = parent;
        SetLayer(layer);
    }

    public void Throw(float force)
    {
        Vector3 direction = transform.parent.forward;
        Drop();
        Rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    public void Catch(GameObject root)
    {
        Rigidbody.isKinematic = true;
        transform.parent = root.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isCatched = true;
    }

    private void SetLayer(int layer)
    {
        transform.gameObject.layer = layer;
        foreach (Transform t in transform)
        {
            t.gameObject.layer = layer;
        }
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = base.Save();
        saveInfo.WriteProperty("isCathed", isCatched);
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        base.Load(saveInfo);
        isCatched = (bool)saveInfo.ReadProperty("isCathed");
        if (isCatched) GetComponent<Rigidbody>().isKinematic = true;
    }
}
