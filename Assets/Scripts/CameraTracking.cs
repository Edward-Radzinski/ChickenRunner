using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private Vector3 newPosition;
    private float height;

    private void Start()
    {
        offset = transform.position - player.position; //высчитываем расстояние от камеры до игрока чтобы на этом же расстоянии от игрока держаться когда он сьебется вдаль
    }
    private void FixedUpdate()
    {  
        if (player == null) return;         //если игрок помер то камера перестает за ним следить иначе ошибки выдает после смерти
        height = player.position.y + 3.83f; //это высота камеры от позиции игрока + некторое значение
        newPosition = new Vector3(player.position.x, height, offset.z + player.position.z);//сама слежка координаты передаются как XYZ
        transform.position = newPosition;   //присвоение этой слежки
    }
}
