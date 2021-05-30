using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class AccessoriesComplect
    {
        private Hull m_Hull;
        private Tower m_Tower;
        private Engine m_Engine;
        private Undercarriage m_Undercarriage;

        public AccessoriesComplect(Hull hull, Tower tower, Engine engine, Undercarriage undercarriage)
        {
            SetHull(hull);
            SetTower(tower);
            SetEngine(engine);
            SetUndercarriage(undercarriage);
        }

        public Hull GetHull()
        {
            return m_Hull;
        }
        public Tower GetTower()
        {
            return m_Tower;
        }
        public Engine GetEngine()
        {
            return m_Engine;
        }
        public Undercarriage GetUndercarriage()
        {
            return m_Undercarriage;
        }

        public void SetHull(Hull hull)
        {
            m_Hull = hull ?? throw new ArgumentNullException(nameof(hull));
        }
        public void SetTower(Tower tower)
        {
            m_Tower = tower ?? throw new ArgumentNullException(nameof(tower));
        }
        public void SetEngine(Engine engine)
        {
            m_Engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }
        public void SetUndercarriage(Undercarriage undercarriage)
        {
            m_Undercarriage = undercarriage ?? throw new ArgumentNullException(nameof(undercarriage));
        }
    }
}
