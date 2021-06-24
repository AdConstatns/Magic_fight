#pragma warning disable 1591

namespace Apex.Examples.AI.Tutorial
{
    using UnityEngine;

    /// <summary>
    /// A very simple example of a custom utility AI client component, which affords the user a choice between a load balanced and a threaded utility AI client.
    /// </summary>
    public class ExampleUtilityAIClientComponent : MonoBehaviour
    {
        [SerializeField]
        private AIClientType _clientType = AIClientType.LoadBalanced;

        private void OnEnable()
        {
            AIHandler.Start(this.gameObject, _clientType);
        }

        private void OnDisable()
        {
            AIHandler.Stop(this.gameObject);
        }
    }
}