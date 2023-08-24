using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PauseController))]
public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverOverlay;
    private PauseController _pauseController;
    private bool _gameOver = false;
    
    private void Awake()
    {
        _pauseController = GetComponent<PauseController>();
    }

    public void GameOver(GameObject player)
    {
        if (_gameOver) return;
        _gameOver = true;
        
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.Death();
        StartCoroutine(ParticlePause(pc.PlayerDeathTime()));
    }

    private IEnumerator ParticlePause(float time)
    {
        yield return new WaitForSeconds(time);
        _gameOverOverlay.SetActive(true);
    }
}