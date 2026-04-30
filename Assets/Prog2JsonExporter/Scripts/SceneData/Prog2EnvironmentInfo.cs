using Prog2JsonExporter.Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Prog2JsonExporter.Scripts.SceneData
{
    public class Prog2EnvironmentInfo : MonoBehaviour
    {
        [SerializeField] private Transform camBoundsLeft;
        [SerializeField] private Transform camBoundsRight;
        [FormerlySerializedAs("playerSpawnPoint")] 
        [SerializeField] private Transform playerStartSpawn;
        [SerializeField] private Transform playerEndSpawn;
        public Prog2EnvironmentSceneInfo GetEnvironmentInfo()
        {
            Prog2EnvironmentSceneInfo scenneInfo = new Prog2EnvironmentSceneInfo();
            scenneInfo.cameraBoundsLeft = camBoundsLeft.position.x;
            scenneInfo.cameraBoundsRight = camBoundsRight.position.x;
            
            scenneInfo.startSpawnPoint = new Prog2Vector2();
            scenneInfo.startSpawnPoint.x = playerStartSpawn.position.x;
            scenneInfo.startSpawnPoint.y = playerStartSpawn.position.y;
            
            scenneInfo.endSpawnPoint = new Prog2Vector2();
            scenneInfo.endSpawnPoint.x = playerEndSpawn.position.x;
            scenneInfo.endSpawnPoint.y = playerEndSpawn.position.y;
            
            return scenneInfo;
        }
    }
}