using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UIElements.Scripts
{
    public class UIElement : WindowDisplay
    {
        [SerializeField]
        private GameObject Root;
        public override void Hide()
        {
            base.Hide();
            Root?.SetActive(false);
        }

        public override void Show()
        {
            base.Show();
            Root?.SetActive(true);
        }

        private void Reset()
        {
            Root = gameObject;
        }
    }
}
