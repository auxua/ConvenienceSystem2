namespace ConvenienceSystemServer
{
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;
    using Nancy.Security;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            
            //SSL: On Windows use this line to force SSL-Forwardings
            //SSLProxy.RewriteSchemeUsingForwardedHeaders(pipelines);
            
        }

    }


}