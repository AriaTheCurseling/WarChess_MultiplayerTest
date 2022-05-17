//using System.ServiceModel;
//using System.ServiceModel.Discovery;

public class Broadcast
{
    //static void HostService<T>()
    //{
    //    using ServiceHost serviceHost = new ServiceHost(typeof(T));

    //    // Add ServiceDiscoveryBehavior
    //    serviceHost.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

    //    // Add a UdpDiscoveryEndpoint
    //    serviceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());
    //}

    //static EndpointAddress FindService<T>()
    //{
    //    // Create DiscoveryClient
    //    DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

    //    // Find ICalculatorService endpoints
    //    FindResponse findResponse = discoveryClient.Find(new FindCriteria(typeof(T)));

    //    if (findResponse.Endpoints.Count > 0)
    //        return findResponse.Endpoints[0].Address;
        
    //    return null;
    //}
}
