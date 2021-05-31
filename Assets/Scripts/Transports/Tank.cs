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

        [SerializeField] private TankViewController m_VisualController;

        [SerializeField] private Ammo m_AmmoMainWeapon;
        [SerializeField] private Ammo m_AmmoSecondaryWeapon;

        private List<Projectile> m_AmmosMainWeapon = new List<Projectile>();
        private List<Bullet> m_AmmosSecondaryWeapon = new List<Bullet>();

        private Transform m_AmmunitionMainSpawn;
        private Transform m_AmmunitionSecondarySpawn;

        private AccessoriesComplect m_AccessoriesComplect;
        

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
                    m_AmmosMainWeapon[0].BrokeCapsule(m_AccessoriesComplect.GetTower().MainWeaponPointOfShot.position);
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
                    m_AmmosSecondaryWeapon[0].BrokeCapsule(m_AccessoriesComplect.GetTower().SecondaryWeaponPointOfShot.position);
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
            m_VisualController.ViewTankAccessories(hull.gameObject, m_VisualController.transform).GetComponent<Hull>();
       }
        public void ChangeTower(Tower tower)
        {
            m_AccessoriesComplect.SetTower(tower);
            m_VisualController.ViewTankAccessories(tower.gameObject, m_AccessoriesComplect.GetHull().SpawnTower).GetComponent<Tower>();
        }
        public void ChangeUndercarriage(Undercarriage undercarriage)
        {
            m_AccessoriesComplect.SetUndercarriage(undercarriage);
            m_VisualController.ViewTankAccessories(undercarriage.gameObject, m_AccessoriesComplect.GetHull().SpawnUndercarriage).GetComponent<Undercarriage>();
        }
        public void ChangeDynamicArmor(DynamicArmor dynamicArmor)
        {
            List<Transform> generalList = new List<Transform>();
            generalList.AddRange(m_AccessoriesComplect.GetTower().SpawnDynamicArmor);
            generalList.AddRange(m_AccessoriesComplect.GetHull().SpawnDynamicArmor);
            m_AccessoriesComplect.SetDynamicArmors(m_VisualController.ViewDynamicArmor(generalList, dynamicArmor.gameObject));
        }

        #endregion
    }
}
