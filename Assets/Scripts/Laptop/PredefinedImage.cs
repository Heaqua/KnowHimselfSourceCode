

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using Random = System.Random;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;




public class PredefinedImage : MonoBehaviour
{
    public FileStream questionMarkFile;
        
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
           
        #region ToSavePredefinedImage

        [Serializable]
        public class SavePredefinedImage
        {
            public static SerializedVector2[] questionMark;
            public static SerializedVector2[] toilet;
        }
    
        [Serializable]public class SerializedVector2
        {
            public float _x;
            public float _y;
     
            public Vector2 Vector2
            {
                get{
                    return new Vector2(_x, _y);
                }
                set
                {
                    _x = value.x;
                    _y = value.y;
                }
            }

            public SerializedVector2(Vector2 _vector2)
            {
                _x = _vector2.x;
                _y = _vector2.y;
            }
        }


        #endregion

        #region Save&Load
        public static List<SerializedVector2> _questionMark = new List<SerializedVector2>();
        public static List<SerializedVector2> _toilet = new List<SerializedVector2>();
        public static void SaveQuestionMark()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.dataPath + "/Scripts/Laptop/QuestionMark.dat");
            SavePredefinedImage.questionMark = _questionMark.ToArray() ;
            bf.Serialize(file, SavePredefinedImage.questionMark);
            file.Close();
        }
        public static void LoadQuestionMark()
        {
            Debug.Log("function called");
            if (File.Exists(Application.dataPath + "/Scripts/Laptop/QuestionMark.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Debug.Log("File exists");
                FileStream file = File.Open(Application.dataPath + "/Scripts/Laptop/QuestionMark.dat", FileMode.Open);
                SavePredefinedImage.questionMark = (SerializedVector2[]) bf.Deserialize(file);
            }
        }
        
        public static void SaveToilet()
        {
            Debug.Log("Save Toilet");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.dataPath + "/Scripts/Laptop/Toilet.dat");
            SavePredefinedImage.toilet = _toilet.ToArray() ;
            bf.Serialize(file, SavePredefinedImage.toilet);
            file.Close();
        }
        public static void LoadToilet()
        {
            if (File.Exists(Application.dataPath + "/Scripts/Laptop/Toilet.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.dataPath + "/Scripts/Laptop/Toilet.dat", FileMode.Open);
                SavePredefinedImage.toilet = (SerializedVector2[]) bf.Deserialize(file);
            }
        }
        #endregion

    }


