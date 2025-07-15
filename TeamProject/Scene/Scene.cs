using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal abstract class Scene
    {
        protected List<string> options;
        protected int optionsLen = 0;

        public Scene()
        {
            options = new List<string>();
        }
        protected abstract void SceneControl();
        public abstract void Render();

        public virtual void SetupScene()
        {
            // Empty
        }
    }
}
