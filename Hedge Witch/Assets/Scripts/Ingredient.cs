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
    public AudioClip droppedInCauldronSound;
    public AudioSource droppedSound;

    bool playSoundOnCollision;
    public void PlaySoundOnCollision(bool b)
    {
        playSoundOnCollision = b;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (playSoundOnCollision)
        {
            droppedSound.Play();
            playSoundOnCollision = false;
        }
    }

}
