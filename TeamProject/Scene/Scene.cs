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
        public Scene()
        {
            options = new List<string>();
        }

        public abstract void Update();
        public abstract void Render();

        public virtual void SetupScene()
        {
            // Empty
        }
    }
}
