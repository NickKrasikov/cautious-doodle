using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerManager server = new ServerManager();

            SiteCollection sites = server.Sites;
            foreach (Site site in sites)
            {
                //Console.Out.WriteLine(site);
                //ApplicationDefaults defaults = site.ApplicationDefaults;

                ////get the name of the ApplicationPool under which the Site runs
                //string appPoolName = defaults.ApplicationPoolName;

                //ConfigurationAttributeCollection attributes = defaults.Attributes;
                //foreach (ConfigurationAttribute configAttribute in attributes)
                //{
                //    Console.Out.WriteLine(configAttribute);
                //    //put code here to work with each ConfigurationAttribute
                //}

                //ConfigurationAttributeCollection attributesCollection = site.Attributes;
                //foreach (ConfigurationAttribute attribute in attributesCollection)
                //{
                //    Console.Out.WriteLine(attribute);
                //    //put code here to work with each ConfigurationAttribute
                //}

                ////Get the Binding objects for this Site
                //BindingCollection bindings = site.Bindings;
                //foreach (Microsoft.Web.Administration.Binding binding in bindings)
                //{
                //    Console.Out.WriteLine(binding);
                //    //put code here to work with each Binding
                //}

                ////retrieve the State of the Site
                //ObjectState siteState = site.State;
                //Console.Out.WriteLine(siteState);

                //Get the list of all Applications for this Site
                ApplicationCollection applications = site.Applications;
                foreach (Application application in applications)
                {
                    VirtualDirectoryCollection vDirectories = application.VirtualDirectories;
                    foreach(VirtualDirectory vDirectory in vDirectories)
                    {
                        Console.Out.WriteLine("{0}\t{1}", vDirectory.Path, vDirectory.PhysicalPath);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
