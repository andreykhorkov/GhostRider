using DefaultNamespace.EventArgs;
using MiscTools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class ActivityMenuRow
    {
        private readonly long m_ActivityId;
        private readonly Button m_Button;
        private readonly Dispatcher m_Dispatcher;

        public ActivityMenuRow(ActivityMenuRowView view, ActivityAttributes rowAttributes, Button button,
            Dispatcher dispatcher)
        {
            view.DateLbl.text = rowAttributes.Date.ToString("dd/MM/yyyy");
            view.DistanceLbl.text = MetersToKilometers(rowAttributes.Distance).ToString("F2");
            view.ElapsedLbl.text = SecondsToHoursMin(rowAttributes.Elapsed);
            view.LocationLbl.text = rowAttributes.Location;
            view.NameLbl.text = rowAttributes.Name;
            m_ActivityId = rowAttributes.Id;
            m_Button = button;
            m_Dispatcher = dispatcher;
            m_Button.onClick.AddListener(OnElementClicked);
        }

        ~ActivityMenuRow()
        {
            m_Button.onClick.RemoveListener(OnElementClicked);
        }

        private void OnElementClicked()
        {
            m_Dispatcher.Send(EventId.ActivityLoadClicked, new LoadActivityEventArgs(m_ActivityId));
            Debug.Log(m_ActivityId);
        }

        private static float MetersToKilometers(double meters)
        {
            return (float)meters / 1000;
        }

        private static string SecondsToHoursMin(int sec)
        {
            var hours = sec / 3600;
            var min = sec % 3600 / 60;
            return $"{hours:00}:{min:00}";
        }

        public class Factory : PlaceholderFactory<ActivityAttributes, ActivityMenuRow>
        {
        }
    }
}