using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Form
{
    public GameObject notificationObject;
    public GameObject radiationPanel;
    public Text useHintText;
    public Text dragHintText;
    public Text throwHintText;
    public Text healthText;
    public Text notificationText;

    private Queue<string> notifications = new Queue<string>();
    private float showNotificationTime;

    void Start()
    {
        notificationObject.gameObject.SetActive(false);
    } 

    public override void Show()
    {
        base.Show();
        radiationPanel.SetActive(false);
    }

    void Update()
    {
        //if (IsShown == false) return;
        healthText.text = Localization.Get("UI.Game.Health") + " " + PlayerController.Instance.Health;
        radiationPanel.SetActive(PlayerController.Instance.InRadiationZone);

        useHintText.gameObject.SetActive(PlayerController.Instance.InteractiveObject != null);
        dragHintText.gameObject.SetActive(PlayerController.Instance.IsHoverDragObject);
        throwHintText.gameObject.SetActive(PlayerController.Instance.DraggableObject != null);

        if (Time.time - showNotificationTime > 3)
        {
            if (notifications.Count > 0)
            {
                notificationObject.SetActive(true);
                notificationText.text = notifications.Dequeue();
                showNotificationTime = Time.time;
            }
            else
            {
                notificationObject.SetActive(false);
            }
        }
    }

    public void AddNotification(string notification)
    {
        notifications.Enqueue(notification);
    }
}
