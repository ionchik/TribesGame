using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    [SerializeField] private Button _sceneSwapper;
    [SerializeField] private string _sceneName;

    private void OnEnable()
    {
        _sceneSwapper.onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        _sceneSwapper.onClick.RemoveListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
