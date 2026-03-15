using UnityEngine;
using UnityEngine.UI;

namespace TitleScene.UI
{
    public class No21Images : MonoBehaviour
    {
        [Header("‰æ‘œØ‚è‘Ö‚¦‚·‚éImage")]
        [SerializeField] private Image no21Image = null;

        [Header("Ø‚è‘Ö‚¦‚é‰æ‘œ")]
        [SerializeField] private Sprite[] sprites = null;

        /// <summary>
        /// ‰æ‘œ‚ğØ‚è‘Ö‚¦‚éƒƒ\ƒbƒh
        /// </summary>
        public void ImageChange(int imageNumber)
        {
            no21Image.sprite = sprites[imageNumber];
        }
    }
}

