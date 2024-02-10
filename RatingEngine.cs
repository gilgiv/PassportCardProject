using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace passportcard
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        private ILogger _logger;
        public decimal Rating { get; set; }

        public RatingEngine(ILogger logger)
        {
            _logger = logger;
        }

        public void Rate()
        {
            // log start rating
            _logger.log("Starting rate.");
            _logger.log("Loading policy.");

            // load policy - open file policy.json
            string policyJson = File.ReadAllText("policy.json");
            PolicyType? policyType = GetPolicyType(policyJson);
            Policy? policy = GetPolicyByPolicyType(policyJson, policyType);

            if (policy != null)
            {
                policy._logger = _logger;
                Rating = policy.Rating();
            }

            _logger.log("Rating completed.");
        }

        private Policy? GetPolicyByPolicyType(string policyJson, PolicyType? policyType)
        {
            Policy? policy = null;
            switch (policyType)
            {
                case PolicyType.Health:
                    policy = JsonConvert.DeserializeObject<HealthInsurancePolicy>(policyJson, new StringEnumConverter());
                    break;

                case PolicyType.Travel:
                    policy = JsonConvert.DeserializeObject<TravelPolicy>(policyJson, new StringEnumConverter());
                    break;

                case PolicyType.Life:
                    policy = JsonConvert.DeserializeObject<LifeInsurancePolicy>(policyJson, new StringEnumConverter());
                    break;

                default:
                    _logger.log("Unknown policy type");
                    break;
            }

            return policy;
        }

        private PolicyType? GetPolicyType(string policyJson)
        {
            JObject parsedObject = JObject.Parse(policyJson);
            PolicyType? policyType = (PolicyType)Enum.Parse(typeof(PolicyType), parsedObject["type"].ToString());
            return policyType;
        }
    }
}
