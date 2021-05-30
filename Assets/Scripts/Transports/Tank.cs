using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    [System.Serializable]
    public class TankParameters
    {
        #region Action

        [Header("Action")]

        [Header("Поставить галочку и повысить скорость")]
        public bool StartTheEngine;


        [Header("Пробел-заряжает")]

        [Header("ставим галочку Атака основным \"A\"")]
        public bool AtackMainWeapon;

        [Header("ставим галочку Атака дополнительным \"S\"")]
        public bool AtackSecondaryWeapon;


        #endregion

        #region Parameters

        [Header("Parameters")]
       
        [Range(0.0f, 10.0f)]
        public float maxSpeed;
       
        #endregion

        #region Accessories

        [Header("Accessories")]

        public Tower tower;
        public Hull hull;
        public Undercarriage undercarriage;
        public Engine engine;
        public DynamicArmor dynamicArmor;

        #endregion

        public Transform AmmunitionMainSpawn;
        public Transform AmmunitionSecondarySpawn;

    }
    public class Tank : MonoBehaviour
    {
        [SerializeField] private TankParameters m_TankParameters;

        [SerializeField] private ViewController m_VisualController;

        [SerializeField] private Ammo m_AmmoMainWeapon;
        [SerializeField] private Ammo m_AmmoSecondaryWeapon;

        private List<Projectile> m_AmmosMainWeapon = new List<Projectile>();
        private List<Bullet> m_AmmosSecondaryWeapon = new List<Bullet>();

        private Transform m_AmmunitionMainSpawn;
        private Transform m_AmmunitionSecondarySpawn;

        private AccessoriesComplect m_AccessoriesComplect;
        private Hull m_Hull;
        private Tower m_Tower;
        private Engine m_Engine;
        private Undercarriage m_Undercarriage;
        private DynamicArmor m_DynamicArmorModel;

        #region  Unity events

        /// <summary>
        /// сборка составных частей танка
        /// </summary>
        private void Awake()
        {
            m_AccessoriesComplect = m_VisualController.ToCollectView(m_TankParameters);
        }

        /// <summary>
        /// получение компонентов  танка
        /// </summary>
        private void Start()
        {
            
            m_AmmunitionMainSpawn = m_TankParameters.AmmunitionMainSpawn;
            m_AmmunitionSecondarySpawn = m_TankParameters.AmmunitionSecondarySpawn;
            m_Tower = m_AccessoriesComplect.GetTower();
            m_Hull = m_AccessoriesComplect.GetHull();
            m_Undercarriage = m_AccessoriesComplect.GetUndercarriage();
            m_Engine = m_AccessoriesComplect.GetEngine();

            m_DynamicArmorModel = m_TankParameters.dynamicArmor.GetComponent<DynamicArmor>();
        }


        private void Update()
        {
            Move();
            if(Input.GetKeyDown(KeyCode.A))
            {
                AtackMainWeapon();
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                AtackSecondaryWeapon();
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ChargeUp();
            }
            
        }
       
        #endregion

        #region MovingAround


        /// <summary>
        /// Движение вперед
        /// </summary>
        private void Move()
        {
            if (m_TankParameters.StartTheEngine)
            {
                transform.position += new Vector3(0f, 0f, m_TankParameters.maxSpeed * Time.deltaTime);
            }
        }

        #endregion

        #region Atack

        /// <summary>
        /// Атака основным оружием при поставленной галочке в инспекторе
        /// </summary>
        private void AtackMainWeapon()
        {
            if (m_TankParameters.AtackMainWeapon)
            {
                if(m_AmmosMainWeapon.Count > 0)
                {
                    m_AmmosMainWeapon[0].BrokeCapsule(m_Tower.MainWeaponPointOfShot.position);
                    m_AmmosMainWeapon.Remove(m_AmmosMainWeapon[0]);
                }
            }
        }

        /// <summary>
        /// Атака доп оружием при поставленной галочке в инспекторе
        /// </summary>
        private void AtackSecondaryWeapon()
        {
            if (m_TankParameters.AtackSecondaryWeapon)
            {
                if (m_AmmosSecondaryWeapon.Count > 0)
                {
                    m_AmmosSecondaryWeapon[0].BrokeCapsule(m_Tower.SecondaryWeaponPointOfShot.position);
                    m_AmmosSecondaryWeapon.Remove(m_AmmosSecondaryWeapon[0]);
                }
            }
        }

        /// <summary>
        /// Перезарядка 
        /// </summary>
        public void ChargeUp()
        {
            for (int i = 0; i < 30; i++)
            {
                GameObject newMainAmmo = Instantiate(m_AmmoMainWeapon).gameObject;
                newMainAmmo.transform.position = m_AmmunitionMainSpawn.position; 
                newMainAmmo.transform.parent = m_AmmunitionMainSpawn;
               

                GameObject newSecondaryAmmo = Instantiate(m_AmmoSecondaryWeapon).gameObject;
                newSecondaryAmmo.transform.position = m_AmmunitionSecondarySpawn.position;
                newSecondaryAmmo.transform.parent = m_AmmunitionSecondarySpawn;
               
                                
                m_AmmosMainWeapon.Add(newMainAmmo.GetComponent<Projectile>());
                m_AmmosSecondaryWeapon.Add(newSecondaryAmmo.GetComponent<Bullet>());

            }
        }

        #endregion

        #region ChangeAccessories
        /// <summary>
        /// Замена составных частей танка
        /// </summary>
        

        public void ChangeHull(Hull hull)
        {
            m_AccessoriesComplect.SetHull(hull);
            m_Hull = hull;
            m_VisualController.ViewTankAccessories(hull.gameObject, m_VisualController.transform).GetComponent<Hull>();
       }
        public void ChangeTower(Tower tower)
        {
            m_AccessoriesComplect.SetTower(tower);
            m_Tower = tower;
            m_VisualController.ViewTankAccessories(tower.gameObject, m_Hull.SpawnTower).GetComponent<Tower>();
        }
        public void ChangeUndercarriage(Undercarriage undercarriage)
        {
            m_AccessoriesComplect.SetUndercarriage(undercarriage);
            m_Undercarriage = undercarriage;
            m_VisualController.ViewTankAccessories(undercarriage.gameObject, m_Hull.SpawnUndercarriage).GetComponent<Undercarriage>();
        }
        public void ChangeDynamicArmor(DynamicArmor dynamicArmor)
        {
            m_DynamicArmorModel = dynamicArmor;
            List<Transform> generalList = new List<Transform>();
            generalList.AddRange(m_Tower.SpawnDynamicArmor);
            generalList.AddRange(m_Hull.SpawnDynamicArmor);
            m_VisualController.ViewDynamicArmor(generalList, dynamicArmor.gameObject);
        }

        #endregion
    }
}
