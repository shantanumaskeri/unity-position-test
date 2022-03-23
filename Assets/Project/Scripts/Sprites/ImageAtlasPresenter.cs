using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Project.Scripts.Sprites
{
    [UsedImplicitly]
    public class ImageAtlasPresenter
    {
        /// <summary>
        /// Static variables 
        /// </summary>
        private static SpriteAtlas _atlas;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="atlas"></param>
        public ImageAtlasPresenter(SpriteAtlas atlas)
        {
            _atlas = atlas;
        }
        
        /// <summary>
        /// Load the sprite into sprite atlas
        /// </summary>
        /// <param name="image"></param>
        /// <param name="spriteRenderer"></param>
        /// <param name="label"></param>
        public static void LoadAtlas(Image image, SpriteRenderer spriteRenderer, string label)
        {
            // Check if the game object has an image component and assign the texture to it
            if (!ReferenceEquals(image, null)) 
                image.sprite = _atlas.GetSprite(label);
            
            // Check if the game object has a sprite renderer component and assign the texture to it
            if (!ReferenceEquals(spriteRenderer, null)) 
                spriteRenderer.sprite = _atlas.GetSprite(label);
        }
    }
}
