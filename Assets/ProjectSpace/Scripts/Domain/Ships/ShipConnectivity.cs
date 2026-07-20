using System;
using System.Collections.Generic;
using ProjectSpace.Domain.Modules;

namespace ProjectSpace.Domain.Ships
{
    public static class ShipConnectivity
    {
        public static IReadOnlyCollection<ModuleInstance> GetConnectedModules(Ship ship)
        {
            if (ship.Core == null)
            {
                return Array.Empty<ModuleInstance>();
            }

            Queue<ModuleInstance> queue = new();
            HashSet<ModuleInstance> connected = new();

            queue.Enqueue(ship.Core);
            connected.Add(ship.Core);

            while (queue.Count > 0)
            {
                ModuleInstance current = queue.Dequeue();

                IReadOnlyCollection<ModuleInstance> neighbors =
                    ship.GetNeighbors(current);

                foreach (ModuleInstance neighbor in neighbors)
                {
                    if (connected.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return connected;
        }

    }

}