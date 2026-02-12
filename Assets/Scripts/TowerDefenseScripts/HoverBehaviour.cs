using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject turretBox;
    [SerializeField] private TMP_Text titleLabel;
    [SerializeField] private TMP_Text descriptionLabel;
    public string turretTitle;
    public string turretDescription;

    void OnMouseOver()
    {
        titleLabel.text = turretTitle;
        descriptionLabel.text = turretDescription;
        turretBox.SetActive(true);
    }

    void OnMouseExit()
    {
        turretBox.SetActive(false);
    }
}
