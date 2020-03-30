using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace com.Avinash.AR_Sharing
{
    public class LoadTestScene : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        }
    }
}

