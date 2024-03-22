using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDamage : MonoBehaviour
{
    public GameObject canvas;

    public void ShowDamageText(string damageAmount, Vector3 position, Color color)
    {
        var Canvas = Instantiate(canvas, position, Quaternion.identity);
        var damageText = Canvas.GetComponentInChildren<TextMeshProUGUI>();
        damageText.color = color;
        damageText.text = damageAmount; 
        Destroy(Canvas.gameObject, 2f);
    }
}
