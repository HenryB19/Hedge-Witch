using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredient : MonoBehaviour
{

    public enum IngredientType
    {
        FrogLeg,
        BatWing,
        SnakeTongue,
        DogHeart,
        RoseThorn,
        OddMushroom,
        UnicornBlood,
        AngelTears,
        DemonFlesh,
    }

    public IngredientType type;

    public void OnSelectEntered() 
    {
        
    }

    public void OnSelectExited()
    {

    }

    public void OnHoverEntered()
    {

    }


    public void OnHoverExited()
    {

    }
}
