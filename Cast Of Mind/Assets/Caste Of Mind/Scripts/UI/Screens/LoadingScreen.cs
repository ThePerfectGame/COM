using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : Form
{
    public Image loadingImage;

    void Update()
    {
        loadingImage.transform.Rotate(new Vector3(0, 0, 50) * Time.deltaTime);
    }
}
