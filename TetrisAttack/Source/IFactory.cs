using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAttack
{
    interface IFactory<T>
    {
        T Make();
    }
}
