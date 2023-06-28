using UnityEngine;

public class SpellBook : MonoBehaviour
{
    public Material material;

    int currentPage = 0;

    private void Start()
    {
        material.SetFloat("_page", 0);
    }

    public void TurnPage(bool direction)
    {
        if (direction)
        {
            if (currentPage == 2) currentPage = 2;
            else currentPage++;
        }
        else
        {
            if (currentPage == 0) currentPage = 0;
            else currentPage--;
        }
        material.SetFloat("_page", currentPage);
    }
}
