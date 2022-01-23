using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BathroomToiletDropdown : MonoBehaviour
{
    public Dropdown mDropdown;
    // Start is called before the first frame update
    void Start()
    {

        mDropdown.onValueChanged.AddListener(delegate
        {
            onValueChanged(mDropdown);
        });
    }

    void Destroy()
    {
        mDropdown.onValueChanged.RemoveAllListeners();
    }

    void showDropdown()
    {
        mDropdown.Show();
    }

    void hideDropdown()
    {
        mDropdown.Hide();
    }

    void onValueChanged(Dropdown target)
    {
        Debug.Log("selected: " + target.value);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
