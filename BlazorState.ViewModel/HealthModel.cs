using System;
using System.ComponentModel;
using System.Text;

namespace BlazorState.ViewModel
{
    public class HealthModel : IHealthModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int BmrWoman()
        {
            return (int)(655 + (4.35 * WeightPounds) + (4.7 * HeightInches)
                - (4.7 * AgeYears));
        }

        private int BmrMan()
        {
            return (int)(66 + (6.23 * WeightPounds) + (12.7 * HeightInches)
                - (6.8 * AgeYears));
        }

        private bool _isMale = true;
        private bool _isImperial = true;
        private double _heightInches = 60;
        private double _weightPounds = 130;
        private int _ageYears = 40;

        public bool IsMale
        {
            get
            {
                return _isMale;
            }
            set
            {
                if (value != _isMale)
                {
                    _isMale = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(IsMale)));
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(IsFemale)));
                }
            }
        }

        public bool IsFemale
        {
            get { return !IsMale; }
            set
            {
                IsMale = !value;
            }
        }

        public bool IsImperial
        {
            get
            {
                return _isImperial;
            }
            set
            {
                if (value != _isImperial)
                {
                    _isImperial = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(IsMetric)));
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(IsImperial)));
                }
            }
        }

        public bool IsMetric
        {
            get { return !IsImperial; }
            set { IsImperial = !value; }
        }

        public double HeightInches
        {
            get => _heightInches;
            set
            {
                if (value != _heightInches)
                {
                    _heightInches = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(HeightInches)));
                }
            }
        }

        public double WeightPounds
        {
            get => _weightPounds;
            set
            {
                if (value != _weightPounds)
                {
                    _weightPounds = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(WeightPounds)));
                }
            }
        }

        public int AgeYears
        {
            get => _ageYears;
            set
            {
                if (value != _ageYears)
                {
                    _ageYears = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(AgeYears)));
                }
            }
        }

        public void ToggleGender()
        {
            IsMale = !IsMale;
        }

        public void ToggleImperial()
        {
            IsImperial = !IsImperial;
        }

        public int Bmr
        {
            get
            {
                return IsMale ? BmrMan() : BmrWoman();
            }
        }

        public double Bmi
        {
            get
            {
                var bmi = (WeightPounds * 703) / (HeightInches * HeightInches);
                return Math.Round(bmi * 10.0) / 10.0;
            }
        }

        public string BmiTag
        {
            get
            {
                if (Bmi >= 30.0)
                {
                    return "Obese";
                }

                if (Bmi >= 25.0)
                {
                    return "Overweight";
                }

                if (Bmi < 18.5)
                {
                    return "Underweight";
                }

                return "Normal";
            }
        }

        public TargetHeartRate Thr
        {
            get
            {
                var max = 220.0 - AgeYears;
                var min = Math.Round(5.0 * max) / 10.0;
                var maxRate = Math.Round(8.5 * max) / 10.0;
                return new TargetHeartRate
                {
                    Low = (int)min,
                    High = (int)maxRate
                };
            }
        }

        public double KilogramsToPounds(double kilograms)
        {
            return kilograms * Globals.KilogramsPerPound;
        }

        public double PoundsToKilograms(double pounds)
        {
            return pounds / Globals.KilogramsPerPound;
        }

        public double InchesToCentimeters(double inches)
        {
            return inches * Globals.CentimetersPerInch;
        }

        public double CentimetersToInches(double centimeters)
        {
            return centimeters / Globals.CentimetersPerInch;
        }

        public string InchesToFeetDisplay(double inches)
        {
            var heightInches = inches;
            var sb = new StringBuilder();
            var ft = Math.Floor(heightInches / Globals.InchesPerFoot);
            if (ft > 0)
            {
                sb.Append($"{(int)ft} ft. ");
            }
            heightInches -= (ft * Globals.InchesPerFoot);
            if (heightInches >= 1)
            {
                sb.Append($"{(int)Math.Floor(heightInches)} in.");
            }
            return sb.ToString();
        }
    }
}
