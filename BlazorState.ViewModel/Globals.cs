using System;

namespace BlazorState.ViewModel
{
    public static class Globals
    {
        public const double KilogramsPerPound = 2.205;
        public const double CentimetersPerInch = 2.54;
        public const double InchesPerFoot = 12;
        public const int MinAge = 13;
        public const int MaxAge = 120;
        public const int MinWeightPounds = 20;
        public const int MaxWeightPounds = 400;
        public static readonly int MinWeightKilograms =
            (int)Math.Floor(MinWeightPounds / KilogramsPerPound);
        public static readonly int MaxWeightKilograms =
            (int)Math.Ceiling(MaxWeightPounds / KilogramsPerPound);
        public const int MinHeightInches = 2 * (int)InchesPerFoot;
        public const int MaxHeightInches = 9 * (int)InchesPerFoot;
        public static readonly int MinHeightCentimeters =
            (int)Math.Floor(MinHeightInches * CentimetersPerInch);
        public static readonly int MaxHeightCentimeters =
            (int)Math.Ceiling(MaxHeightInches * CentimetersPerInch);
    }
}
