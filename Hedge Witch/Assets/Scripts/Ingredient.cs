using UnityEngine;

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
}
