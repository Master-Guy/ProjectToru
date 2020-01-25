using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Options
{
    public class LootDesk : Option
    {
        public GameObject Key = null;

        private Desk Vault;

        public void Start()
        {
            Vault = GetComponentInParent<Desk>();
        }

        public override string Activate(Character c)
        {
            return null;
        }
    }
}
