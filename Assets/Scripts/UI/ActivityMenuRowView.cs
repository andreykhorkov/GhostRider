using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class ActivityMenuRowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_DateLbl;
        [SerializeField] private TextMeshProUGUI m_NameLbl;
        [SerializeField] private TextMeshProUGUI m_DistanceLbl;
        [SerializeField] private TextMeshProUGUI m_ElapsedLbl;
        [SerializeField] private TextMeshProUGUI m_LocationLbl;

        public TextMeshProUGUI DateLbl => m_DateLbl;
        public TextMeshProUGUI NameLbl => m_NameLbl;
        public TextMeshProUGUI DistanceLbl => m_DistanceLbl;
        public TextMeshProUGUI ElapsedLbl => m_ElapsedLbl;
        public TextMeshProUGUI LocationLbl => m_LocationLbl;
    }
}