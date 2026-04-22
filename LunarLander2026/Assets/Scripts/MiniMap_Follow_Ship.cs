using UnityEngine;

public class MiniMap_Follow_Ship : MonoBehaviour
{
    //get refrence to object to follow
    public Transform Player;

    //minimap location is update after the player moves (this is done with late update)
    void LateUpdate()
    {
        Vector3 newposistion = Player.position;
        newposistion.y = transform.position.y;
        transform.position = newposistion; 
    }

}
