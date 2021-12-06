using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacketReceiver : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        MiniMapController.RegisterMapObject(this.gameObject, image);
    }
    void OnDestroy()
    {
        MiniMapController.RemoveMapObject(this.gameObject);
    }
    public void Destruction()
    {
        FindObjectOfType<AudioManager>().PlaySound("Unload");
        Destroy(this.gameObject);
    }

    void Update()
    {

    }
}
