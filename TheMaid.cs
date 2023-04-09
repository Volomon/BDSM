using EFT;
using UnityEngine;
using Comfort.Common;
using System.Threading.Tasks;
using BDSM;

namespace BDSM
{
    public class TheMaid : MonoBehaviour
    {
        float timer { get; set; }

        void Update()
        {
            if (!Ready())
            {
                timer = 0f;
                return;
            }

            if (Plugin.EnableClean.Value)
            {
                timer += Time.deltaTime;
            }
            if (timer >= Plugin.TimeToClean.Value)
            {
                QueueCleanup();
                timer = 0f;
            }
        }

        async void QueueCleanup()
        {
            await Task.Delay(10000);
            foreach (BotOwner bot in FindObjectsOfType<BotOwner>())
            {
                if (!bot.HealthController.IsAlive && Vector3.Distance(player.Transform.position, bot.Transform.position) >= Plugin.DistToClean.Value)
                {
                    bot.gameObject.SetActive(false);
                }
            }
        }

        public bool Ready() => gameWorld != null && gameWorld.AllPlayers != null && gameWorld.AllPlayers.Count > 0 && !(player is HideoutPlayer);

        Player player
        { get => gameWorld.AllPlayers[0]; }

        GameWorld gameWorld
        { get => Singleton<GameWorld>.Instance; }
    }
}