using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Data model
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)]
        public float mass;
        
        [Range(0.0f, 10.0f)]
        public float thrust;

        [Range(0.0f, 100.0f)]
        public float agility;
        public float maxSpeed;

        public bool afteburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller
    /// </summary>
    public class Bike : MonoBehaviour
    {
        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParameters m_BikeParametersInitial;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController m_VisualController;

        private BikeParameters m_EffectiveOarameters;

        //methods that changes model and view

        //каждый метод это небольшое действие сущности
        //это действие должно быть максимально эффективно
        //следует избегать длинных методов, тело метода
        //повторяющиеся действие это кандидат на вынос в отдельный метод;

        private void DoSomething(Vector3 a, string b)
        {
            Debug.Log($"a = {a} | b = {b}");

            m_VisualController.SetupBikeView(m_BikeParametersInitial);
        }

        #region Unity events
        
        //задача по кнопке пробел спавнить префаб
        //действие = метод
        //определяем параметры
        //1 параметр
        //уровень доступность =нужен ли метод за пределами класса? private/public
        // типо возвращаемого значения? = будет ли метод нам что то вычислять?
        // если нет = void иначе тип который мы считаем

        private GameObject CreateNewPrefabInstance(GameObject sourcePrefab)
        {
            return Instantiate(sourcePrefab);
        }
       
        [SerializeField] private GameObject m_Prefab;

        private void Update()
        {
          if(Input.GetKeyDown(KeyCode.Space))
            {
                CreateNewPrefabInstance(m_Prefab);
            }
        }

        #endregion
    }
}

