using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Track
{
    public class FakeCompass : ICompass, IDisposable
    {
        private readonly Button m_StateButton;
        private readonly Transform m_NorthDirectionTf;

        public Vector3 NorthDirection { get; private set; }

        public FakeCompass(MainInstaller.Installables installables, Button button)
        {
            m_NorthDirectionTf = installables.m_NorthDirectionTf;
            m_StateButton = button;
            m_StateButton.onClick.AddListener(SetNorthDirection);
        }

        void IDisposable.Dispose()
        {
            m_StateButton.onClick.RemoveListener(SetNorthDirection);
        }

        private void SetNorthDirection()
        {
            var dir = Vector3.ProjectOnPlane(m_NorthDirectionTf.forward, Vector3.up);
            NorthDirection = dir.normalized;
        }
    }
}