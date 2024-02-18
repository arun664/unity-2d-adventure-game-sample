using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{

    public float currentHealth = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uidocument = GetComponent<UIDocument>();
        VisualElement healthBar = uidocument.rootVisualElement.Q<VisualElement>("HealthBar");
        healthBar.style.width = Length.Percent(currentHealth *  100.0f);
    }

}
