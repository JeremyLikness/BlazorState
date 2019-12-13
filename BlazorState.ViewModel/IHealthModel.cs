using System.ComponentModel;

namespace BlazorState.ViewModel
{
    public interface IHealthModel: INotifyPropertyChanged
    {
        int AgeYears { get; set; }
        double Bmi { get; }
        string BmiTag { get; }
        int Bmr { get; }
        double HeightInches { get; set; }
        bool IsFemale { get; set; }
        bool IsImperial { get; set; }
        bool IsMale { get; set; }
        bool IsMetric { get; set; }
        TargetHeartRate Thr { get; }
        double WeightPounds { get; set; }

        double CentimetersToInches(double centimeters);
        double InchesToCentimeters(double inches);
        string InchesToFeetDisplay(double inches);
        double KilogramsToPounds(double kilograms);
        double PoundsToKilograms(double pounds);
        void ToggleGender();
        void ToggleImperial();
    }
}