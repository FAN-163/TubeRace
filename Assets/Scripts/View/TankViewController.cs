using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    
    public class TankViewController : MonoBehaviour
    {
        private AccessoriesComplect m_AccessoriesComplect;

        /// <summary>
        /// Принимает параметры танка для создания объектов и возвращает созданные объекты
        /// </summary>
        /// <param name="tankParameters"></param>
        /// <returns></returns>
        public AccessoriesComplect ToCollectView(TankParameters tankParameters)
        {
            GameObject newHull = ViewTankAccessories(tankParameters.hull.gameObject, transform);
            newHull.transform.parent = transform;
            Hull hull = newHull.transform.GetComponent<Hull>();

            GameObject newTower = ViewTankAccessories(tankParameters.tower.gameObject, hull.SpawnTower);
            Tower tower = newTower.transform.GetComponent<Tower>();

            GameObject newEngine = ViewTankAccessories(tankParameters.engine.gameObject, hull.SpawnEngine);
            Engine engine = newEngine.transform.GetComponent<Engine>();

            GameObject newUndercarriage = ViewTankAccessories(tankParameters.undercarriage.gameObject, hull.SpawnUndercarriage);
            Undercarriage undercarriage = newUndercarriage.transform.GetComponent<Undercarriage>();

            List<Transform> generalList = new List<Transform>();
            generalList.AddRange(tower.SpawnDynamicArmor);
            generalList.AddRange(hull.SpawnDynamicArmor);

            List<GameObject> newDynamicArmors = ViewDynamicArmor(generalList, tankParameters.dynamicArmor.gameObject);
            
            m_AccessoriesComplect = new AccessoriesComplect(hull, tower, engine, undercarriage, newDynamicArmors);
            return m_AccessoriesComplect;
        }
        
        public List<GameObject> ViewDynamicArmor(List<Transform> transforms, GameObject dynamicArmor)
        {
            List<GameObject> listDynamicArmor = new List<GameObject>();
            foreach (Transform spawnDynamickArmor in transforms)
            {
                GameObject newDynamicArmor = Instantiate(dynamicArmor);
                newDynamicArmor.transform.parent = spawnDynamickArmor;
                newDynamicArmor.transform.position = spawnDynamickArmor.position;
                newDynamicArmor.transform.up = spawnDynamickArmor.up;
                listDynamicArmor.Add(newDynamicArmor);
            }

            return listDynamicArmor;
        }
        public GameObject ViewTankAccessories(GameObject accessories, Transform parent)
        {
            GameObject newAccessories = Instantiate(accessories);
            newAccessories.transform.parent = parent;
            newAccessories.transform.position = parent.position;
            return newAccessories;
        }

    }
}
