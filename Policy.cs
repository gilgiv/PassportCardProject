namespace passportcard
{

    public abstract class Policy
    {
        public PolicyType Type { get; set; }
        public ILogger? _logger;

        #region General Policy Prop
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        #endregion


        public abstract decimal Rating();
    }

    public class LifeInsurancePolicy : Policy
    {
        public bool IsSmoker { get; set; }
        public decimal Amount { get; set; }

        public LifeInsurancePolicy()
        {
            Type = PolicyType.Life;
        }

        public override decimal Rating()
        {
            decimal Rating;

            _logger.log("Rating LIFE policy...");
            _logger.log("Validating policy.");
            if (DateOfBirth == DateTime.MinValue)
            {
                _logger.log("Life policy must include Date of Birth.");
                return 0;
            }
            if (DateOfBirth < DateTime.Today.AddYears(-100))
            {
                _logger.log("Max eligible age for coverage is 100 years.");
                return 0;
            }
            if (Amount == 0)
            {
                _logger.log("Life policy must include an Amount.");
                return 0;
            }

            int age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < DateOfBirth.Day ||
                DateTime.Today.Month < DateOfBirth.Month)
            {
                age--;
            }
            decimal baseRate = Amount * age / 200;

            if (IsSmoker)
            {
                Rating = baseRate * 2;
                return Rating;
            }
            Rating = baseRate;
            return Rating;
        }
    }

    public class TravelPolicy : Policy
    {
        public string? Country { get; set; }
        public int Days { get; set; }

        public TravelPolicy()
        {
            Type = PolicyType.Travel;
        }

        public override decimal Rating()
        {
            decimal Rating;
            _logger.log("Rating TRAVEL policy...");
            _logger.log("Validating policy.");
            if (Days <= 0)
            {
                _logger.log("Travel policy must specify Days.");
                return 0;
            }
            if (Days > 180)
            {
                _logger.log("Travel policy cannot be more then 180 Days.");
                return 0;
            }
            if (string.IsNullOrEmpty(Country))
            {
                _logger.log("Travel policy must specify country.");
                return 0;
            }
            Rating = Days * 2.5m;
            if (Country == "Italy")
            {
                Rating *= 3;
            }
            return Rating;
        }
    }

    public class HealthInsurancePolicy : Policy
    {

        public string? Gender { get; set; }
        public decimal Deductible { get; set; }
        public HealthInsurancePolicy()
        {
            Type = PolicyType.Health;
        }

        public override decimal Rating()
        {
            decimal Rating;
            _logger.log("Rating HEALTH policy...");
            _logger.log("Validating policy.");
            if (string.IsNullOrEmpty(Gender))
            {
                _logger.log("Health policy must specify Gender");
                return 0;
            }
            if (Gender == "Male")
            {
                if (Deductible < 500)
                {
                    Rating = 1000m;
                }
                Rating = 900m;
            }
            else
            {
                if (Deductible < 800)
                {
                    Rating = 1100m;
                }
                Rating = 1000m;
            }

            return Rating;

        }
    }
}
