using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] NotificationUnit unit;
    [SerializeField] Transform container;


    public void ReceiveOrder(string value)
    {
        //its just a string value

        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).GetComponent<NotificationUnit>().MoveFurtherBelow();
        }

        NotificationUnit newObject = Instantiate(unit, container.transform.position, Quaternion.identity);
        newObject.transform.parent = container;
        newObject.SetUp(value);
        newObject.transform.localScale = new Vector3(1, 1, 1);
    }

    int teste;
    [ContextMenu("Create Example")]
    public void CreateExample()
    {
        teste++;
        ReceiveOrder("this is an example " + teste);
    }

    public void CloseAllNotifications()
    {

    }
}
