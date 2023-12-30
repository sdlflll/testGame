using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRoomSprite : MonoBehaviour
{
    public int spriteRoomType;
    public int canvasRoomType;
    public Sprite[] canvasRoomsSprites = new Sprite[5];
    public Image canvasRoomImage;

    private void Awake()
    {
        canvasRoomImage = gameObject.GetComponent<Image>();
        canvasRoomImage.sprite = canvasRoomsSprites[canvasRoomType];
    }
}
