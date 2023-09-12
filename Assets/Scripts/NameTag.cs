using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    PhotonView view;

    [SerializeField]
    private Text nameText;

    private Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        SetTextColors();
        nameText.text = view.Owner.NickName;
    }

    private void SetTextColors()
    {
        if(this.gameObject.name == "Player Red(Clone)")
        {
            textColor =  new Color32(196, 36, 48, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Orange(Clone)")
        {
            textColor =  new Color32(237, 118, 20, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Yellow(Clone)")
        {
            textColor =  new Color32(255, 200, 37, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Green(Clone)")
        {
            textColor =  new Color32(30, 111, 80, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Blue(Clone)")
        {
            textColor =  new Color32(0, 57, 109, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Purple(Clone)")
        {
            textColor =  new Color32(98, 36, 97, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Pink(Clone)")
        {
            textColor =  new Color32(243, 137, 245, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Light Blue(Clone)")
        {
            textColor =  new Color32(148, 253, 255, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Black(Clone)")
        {
            textColor =  new Color32(19, 19, 19, 255);
            nameText.color = textColor;
        }
        if(this.gameObject.name == "Player Gray(Clone)")
        {
            textColor =  new Color32(180, 180, 180, 255);
            nameText.color = textColor;
        }
    }
}
