using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
    /// <summary>
    /// Interface to implement common Methods to Register Classes
    /// Inherit this in all Registers
    /// </summary>
    interface IRegister
    {
         OutputMessage Save();
        OutputMessage Update();
        OutputMessage Delete();        
    }
}
