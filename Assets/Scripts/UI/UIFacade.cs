using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class UIFacade
    {
        private Button m_StateButton;

        public UIFacade(Button stateButton)
        {
            m_StateButton = stateButton;

            m_StateButton.onClick.AddListener(OnStateBtnClick);
        }

        ~UIFacade()
        {
            m_StateButton.onClick.RemoveListener(OnStateBtnClick);
        }

        private void OnStateBtnClick()
        {
            UnityEngine.Debug.Log("StateBtn Click");
        }
    }
}