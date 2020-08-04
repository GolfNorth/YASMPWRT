using System;

namespace YASMPWRT.Data
{
    public sealed class LevelsData
    {
        private static readonly Lazy<LevelsData> _instance = new Lazy<LevelsData>(() => new LevelsData());

        public static LevelsData Instance => _instance.Value;
        public string[] Levels { get; private set; }

        public LevelsData()
        {
            Levels = new[]
            {
                "Levels/Level-Test",
                "Levels/Level-1",
                "Levels/Level-2",
                "Levels/Level-3",
                "Levels/Level-9",
                "Levels/Level-4",
                "Levels/Level-7",
                "Levels/Level-6",
                "Levels/Level-8",
                "Levels/Level-5",
                "Levels/Level-Ending"
            };
        }
    }
}