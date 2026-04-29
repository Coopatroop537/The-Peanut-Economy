using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour
{
    public Sprite[] textures; // Array for textures
    private int[] imageSymbols; // Assuming you have an array for the symbols

    public void UpdateReelDisplay()
    {
        // Swap textures logic
        for (int i = 0; i < imageSymbols.Length; i++)
        {
            int symbolIndex = imageSymbols[i] % 4; // Change % 10 to % 4
            // Assuming you have a method to set the image sprite:
            SetImageSprite(i, textures[symbolIndex]); // Set the sprite from the textures array
        }
    }

    // Assuming you have a method to set an image sprite
    private void SetImageSprite(int index, Sprite sprite)
    {
        // Implementation to set the sprite (not shown)
    }
}