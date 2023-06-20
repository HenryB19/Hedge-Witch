using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum IngredientType
    {
        FrogLeg,//0
        BatWing,//1
        SnakeTongue,//2
        DogHeart,//3
        RoseThorn,//4
        OddMushroom,//5
        UnicornBlood,//6
        AngelTears,//7
        DemonFlesh,//8
    }

    public IngredientType type;
}
