using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Speedmarket
{
    public class MainMenu : MonoBehaviour
    {
        Stack<GameObject> _stack;

        public void LoadLevel(int index)
        {
            SceneManager.LoadSceneAsync(index);
        }

        public void Push(GameObject gObj)
        {
            GameObject current = _stack.Peek();
            current.SetActive(false);

            _stack.Push(gObj);
            gObj.SetActive(true);
        }

        public void Pop()
        {
            GameObject popped = _stack.Pop();
            popped.SetActive(false);

            GameObject current = _stack.Peek();
            current.SetActive(true);
        }

        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        void Start()
        {
            int capacity = 4;
            _stack = new Stack<GameObject>(capacity);
            _stack.Push(gameObject);
        }
    }
}
