using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.LoadBalancing;

namespace ApiGateway.Configuration
{
    public static class YarpConfig
    {
        public static IReadOnlyList<RouteConfig> GetRouteConfigs()
        {
            return new List<RouteConfig>()
            {
                new RouteConfig()
                {
                    RouteId = "auth-route",
                    ClusterId = "product-cluster",
                    Match = new RouteMatch()
                    {
                        Path = "/auth-service/{**catch-all}",
                    }
                },
            };
        }

        public static IReadOnlyList<ClusterConfig> GetClusterConfigs() {
            return new List<ClusterConfig>()
            {
                new ClusterConfig()
                {
                    ClusterId = "auth-cluster",
                    Destinations = new Dictionary<string, DestinationConfig>()
                    {
                        {"auth-destination",new DestinationConfig(){Address = "http://localhost:5001"} },
                        {"auth-destination2",new DestinationConfig(){Address = "http://localhost:5002"} },
                        {"auth-destination3",new DestinationConfig(){Address = "http://localhost:5003"} },
                    },
                    LoadBalancingPolicy = LoadBalancingPolicies.RoundRobin
                }
            };
        }
    }
}
