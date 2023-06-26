using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellBook : MonoBehaviour
{
    public Material material;

    int currentPage = 0;

    public void TurnPage(bool direction)
    {
        if (direction)
        {
            currentPage++;
        }
        else
        {
            currentPage--;
        }
        if (currentPage == 3) currentPage = 0;
        else if (currentPage == -1) currentPage = 2;
        material.SetFloat("_page", currentPage);
    }
}
