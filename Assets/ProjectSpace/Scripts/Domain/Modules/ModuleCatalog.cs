using ProjectSpace.Domain.Common;

namespace ProjectSpace.Domain.Modules
{

    public static class ModuleCatalog
    {
        public static readonly ModuleDefinition Core =
            new ModuleDefinition(
                ModuleType.Core,
                "Core",
                new GridSize(3, 3),
                500);

        public static readonly ModuleDefinition Cockpit =
            new ModuleDefinition(
                ModuleType.Cockpit,
                "Cockpit",
                new GridSize(3, 2),
                200);

        public static readonly ModuleDefinition Engine =
            new ModuleDefinition(
                ModuleType.Engine,
                "Engine",
                new GridSize(2, 3),
                150);

        public static readonly ModuleDefinition Turret =
            new ModuleDefinition(
                ModuleType.Turret,
                "Turret",
                new GridSize(2, 2),
                100);
    }
}