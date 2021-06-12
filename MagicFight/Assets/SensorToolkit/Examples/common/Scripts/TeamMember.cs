using UnityEngine;
using System.Collections;

namespace SensorToolkit.Example
{
    public enum Teams { Yellow, Magenta, Green, Red, Cyan, None };

    public class TeamMember : MonoBehaviour
    {
        public Teams StartTeam;
        public Material DefaultMaterial;
        public Material YellowMaterial;
        public Material MagentaMaterial;
        public Material GreenMaterial;
        public Material RedMaterial;
        public Material CyanMaterial;
        //public MeshRenderer[] TeamRenderers;
        public SkinnedMeshRenderer[] TeamSkinRenders;

        private Teams team;
        public Teams Team
        {
            get { return initialised ? team : StartTeam; }
            set
            {
                team = value;
                Material mat = DefaultMaterial;
                switch (team) {
                    case Teams.Yellow:
                        mat = YellowMaterial;
                        break;
                    case Teams.Magenta:
                        mat = MagentaMaterial;
                        break;
                    case Teams.Green:
                        mat = GreenMaterial;
                        break;
                    case Teams.Red:
                        mat = RedMaterial;
                        break;
                    case Teams.Cyan:
                        mat = CyanMaterial;
                        break;
                    case Teams.None:
                        break;
                    default:
                        break;
                }
                //var mat = team == Teams.Yellow ? YellowMaterial : MagentaMaterial;
                 
                for (int i = 0; i < TeamSkinRenders.Length; i++)
                {
                    //var renderer = TeamRenderers[i];
                    //renderer.sharedMaterial = mat;

                    var skinrenderer = TeamSkinRenders[i];
                    skinrenderer.sharedMaterial = mat;
                }
            }
        }

        bool initialised = false;

        void Start()
        {
            Team = StartTeam;
            initialised = true;
        }
    }
}