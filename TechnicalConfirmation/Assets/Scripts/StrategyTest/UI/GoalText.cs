using UnityEngine;
using TMPro;

namespace StrategyTest.UI
{
    public class GoalText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private ExitTile exitTile;

        private void Start()
        {
            textMeshProUGUI.enabled = false;
        }

        private void OnEnable()
        {
            exitTile.OnPlayerEnter += OnPlayerEnter;
        }
        private void OnDisable() 
        {
            exitTile.OnPlayerEnter -= OnPlayerEnter;
        }

        private void OnPlayerEnter(bool isEntered)
        {
            if (isEntered)
            {
                textMeshProUGUI.enabled = true;
            }
        }
    }
}